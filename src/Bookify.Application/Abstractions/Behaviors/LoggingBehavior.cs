using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //using reflection to get the type of the current request
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation("Executing command {Command}", name);

            var result = await next(); // run the request handler delegate which is the commandhandler

            _logger.LogInformation("Command {Command} processed successfully", name);
            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Command {Command}  processing failed", name);
            throw;
        }
    }
}
