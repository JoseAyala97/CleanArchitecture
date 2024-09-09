namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; } 

        public CodeErrorResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            //en caso de que sea nulo, el metodo privado lo manejara
            ErrorMessage = errorMessage ?? GetDefaultMessageStatusCode(statusCode);
        }
        //logica del statusCode
        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request enviado tiene errores",
                401 => "No tienes autorizacion para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Error en el servidor",
                //error por defecto
                _ => string.Empty,
            };
        }
    }
}
