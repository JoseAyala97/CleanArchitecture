using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("Se presentaron uno o mas errores de validacion.")
        {
            //se inicializa la propiedad
            Errors = new Dictionary<string, string[]>();
        }
        //logica para agregar todas las validaciones
        //evaluacionde  la propiedad y el mensaje de error
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            //se inicializa la propiedad desde failuries
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failurieGroup => failurieGroup.Key, failurieGroup => failurieGroup.ToArray());
        }
        //propiedad de tipo IDictionary, para guardar todos los errores
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
