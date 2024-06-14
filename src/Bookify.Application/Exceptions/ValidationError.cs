namespace Bookify.Application;

public sealed record ValidationError(string PropertyName, string ErrorMessage);
