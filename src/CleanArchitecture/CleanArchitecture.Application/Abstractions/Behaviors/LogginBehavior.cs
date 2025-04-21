using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Abstractions.Messging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaviors
{
    public class LogginBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> _logger;

        public LogginBehaviors(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;
            try {
                _logger.LogInformation("Executing Application Request: {Name} {@Request}", name, request);
                var result =  await next();
                _logger.LogInformation("Application Request: {Name} {@Request} Completed", name, request);
                return result;

            }catch (Exception ex) {
                _logger.LogError(ex, "Application Request: Unhandled Exception for request {Name} {@Request}", name, request);
                throw;
            }           
        }
    }
}