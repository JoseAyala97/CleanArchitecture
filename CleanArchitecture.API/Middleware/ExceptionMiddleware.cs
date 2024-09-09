using CleanArchitecture.API.Errors;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        //pipeline que pasara a la siguiente fase en caso de que no haya exception
        private readonly RequestDelegate _next;
        //logger
        private readonly ILogger<ExceptionMiddleware> _logger;
        //para conocer si se esta debug o en produccion
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //que procese el request
                await _next(context);
            }catch (Exception ex)
            {
                //si hay error que imprima el detalle del error
                _logger.LogError(ex, ex.Message);
                //mensaje que se le mostrara
                context.Response.ContentType = "application/json";
                //error por default
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //mensaje que se mostrara dependiendo si esta en desarrollo o en produccion
                var response = _environment.IsDevelopment()
                    //ex.Message para mostrar el mensaje 
                    //ex.StackTrace para mostrar detalles
                    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError);
                
                //Para enviar el texto en formato json
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                
                var json = JsonSerializer.Serialize(response, options);
                //el que ya envia el mensaje al cliente
                await context.Response.WriteAsync(json);
            }
        }
    }
}
