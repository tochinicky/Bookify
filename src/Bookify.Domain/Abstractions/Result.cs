using System.Diagnostics.CodeAnalysis;

namespace Bookify.Domain;
/// <summary>This pattern provides a robust way to handle and propagate the results of operations, explicitly managing success and failure states without relying on exceptions. The use of Error (presumably an enum or class) allows detailed error information to be attached to failure results. This approach improves code readability and maintainability by clearly indicating success and failure states and their associated data. </summary>
public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; } //Indicates if the result represents a successful operation.
    public bool IsFailure => !IsSuccess; //Indicates if the result represents a failed operation (inverse of IsSuccess).
    public Error Error { get; } //Holds the error information if the operation failed.
    //Creates a successful result.
    public static Result Success() => new(true, Error.None);
    //Creates a failed result with an error.
    public static Result Failure(Error error) => new(false, error);

    //Creates a successful result with a value.
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    //Creates a failed result with an error and a default value.
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    // <summary>Creates a result based on whether the provided value is null.</>
    public static Result<TValue> Create<TValue>(TValue? value) => value is not null
    ? Success(value) : Failure<TValue>(Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("The value of a failure result cannot be accessed");
    public static implicit operator Result<TValue>(TValue? value) => Create(value); // Allows implicit conversion from TValue to Result<TValue>, creating a result based on whether the value is null.
}
