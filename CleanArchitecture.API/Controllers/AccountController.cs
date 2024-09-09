using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AccountController(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            return Ok(await _authServices.Login(request));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {
            return Ok(await _authServices.Register(request));
        }

    }
}
