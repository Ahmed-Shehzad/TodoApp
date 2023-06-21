using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Foundation.API.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Handling {Name}", typeof(TRequest).Name);
            try
            {
                var response = await next();
                _logger.LogDebug("Handled {Name}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error in {Name}: {E}", typeof(TRequest).Name, e);
                throw;
            }
        }
    }
}