namespace Bookify.Application;

public sealed class ValidationException : Exception
{
    public IEnumerable<ValidationError> Error { get; }
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Error = errors;
    }
}
