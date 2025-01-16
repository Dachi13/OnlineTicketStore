namespace BuildingBlocks.Library;

public class Result<TValue>
{
    public readonly TValue Value;
    public readonly bool IsSuccess;
    public readonly Error Error;

    private Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = Error.None;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default!;
        Error = error;
    }

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<Error, TResult> failure)
        => IsSuccess
            ? success(Value)
            : failure(Error);
}