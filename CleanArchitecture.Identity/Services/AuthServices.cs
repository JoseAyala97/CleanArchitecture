using CleanArchitecture.Application.Constans;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthServices : IAuthServices
    {
        //ya se encuentra en la libreria de identity, no se debe crear por aparte
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CleanArchitectureIdentityDbContext _context;

        public AuthServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings, RoleManager<IdentityRole> roleManager, CleanArchitectureIdentityDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())

                try
                {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new Exception($"El usuario con email {request.Email} no existe");
                }

                var resultado = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

                if (!resultado.Succeeded)
                {
                    throw new Exception($"Las credenciales son incorrectas");
                }

                var token = await GenerateToken(user);
                var authResponse = new AuthResponse
                {
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Email = user.Email,
                    Username = user.UserName,
                };

                return authResponse;
            }
            catch
            {
                    // Revertir la transacción si ocurre un error
                    await transaction.RollbackAsync();
                    throw;
                }
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingUser = await _userManager.FindByNameAsync(request.UserName);
                    if (existingUser != null)
                        throw new Exception($"El usuario ya fue tomado por otra cuenta");

                    var existingEmail = await _userManager.FindByEmailAsync(request.Email);
                    if (existingEmail != null)
                        throw new Exception($"El email ya se encuentra registrado");

                    var user = new ApplicationUser
                    {
                        Email = request.Email,
                        Name = request.Name,
                        LastName = request.LastName,
                        UserName = request.UserName,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new Exception($"Error al crear el usuario: {errors}");
                    }

                    // Asignar el rol seleccionado
                    if (!string.IsNullOrEmpty(request.Role))
                    {
                        if (await _roleManager.RoleExistsAsync(request.Role))
                        {
                            await _userManager.AddToRoleAsync(user, request.Role);
                        }
                        else
                        {
                            throw new Exception($"El rol '{request.Role}' no existe.");
                        }
                    }

                    var token = await GenerateToken(user);

                    // Confirmar la transacción si todo ha salido bien
                    await transaction.CommitAsync();

                    return new RegistrationResponse
                    {
                        Email = user.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        UserId = user.Id,
                        Username = user.UserName
                    };
                }
                catch
                {
                    // Revertir la transacción si ocurre un error
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //data dentro del token se le denomina claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            //para parsear la data como objeto de tipo claim para poder ponerla dentro del token
            var roleClaims = new List<Claim>();

            foreach(var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                //un claim, seria un dato en un diccionario de datos, agregando al cabecera JwtRegisteredClaimNames.Sub, como buena practica
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                //un claim, seria un dato en un diccionario de datos, agregando al cabecera JwtRegisteredClaimNames.Email, como buena practica
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //usando constantes en vez de las referencias directas que estan en la libreria
                new Claim(CustomClaimTypes.Uid, user.Id)
                //Union para agregar los otros datos al token
            }.Union(userClaims).Union(roleClaims);
            //_jwtSettings sera lo que nos permitira leer el dato, que se ubicara en appSetting
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            //algoritmo de seguridad
            var sigingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: sigingCredentials
                );
            
            return jwtSecurityToken;
        }
    }
}
