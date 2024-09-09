using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;

namespace CleanArchitecture.Application.Behaviours
{
    //Debe ser generico porque uno representara el request y el otro el response
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        //IValidator de fluentValidation
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        //captura el objeto request, para validar las propiedades
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()) 
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failuries = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failuries.Count != 0)
                {
                    throw new ValidationException(failuries);
                }
            }
            return await next();
        }
    }
}
