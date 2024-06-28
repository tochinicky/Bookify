using Bookify.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Bookify.Application;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse> >_logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //using reflection to get the type of the current request
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation("Executing Request {Request}", name);

            var result = await next(); // run the request handler delegate which is the commandhandler

            if (result.IsSuccess)
            {
                _logger.LogInformation("Request {Request} processed successfully", name);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    _logger.LogError("Request {Request} processed with error", name);
                }
            }
            
            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Request {Request}  processing failed", name);
            throw;
        }
    }
}
