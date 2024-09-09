using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviours
{
    //trabaja monitoreando las validaciones
    public class UnHandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnHandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application requesat: Sucedio una excepcion para el request {Name} {@Request}", requestName, request);
                throw;
            }
            
        }
    }
}
