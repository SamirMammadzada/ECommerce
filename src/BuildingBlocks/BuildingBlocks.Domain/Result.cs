namespace BuildingBlocks.Domain;

public class Result
{
    public bool IsSuccessful { get; protected set; }
    public string? Error { get; protected set; }

    protected Result(bool isSuccesful, string? error)
    {
        IsSuccessful = isSuccesful;
        Error = error;
    }

    public static Result Failure(string error) 
    {
        return new Result(false, error);
    }

    public static Result Success()
    {
        return new Result(true, null);
    }
}

public class Result<T> : Result
{
    public T? Value { get; protected set; }
    
    protected Result(bool isSuccessful , T? value, string? error) : base(isSuccessful , error)
    {
        Value = value;
    }

    public static new Result<T> Failure(string error)
    {
        return new Result<T>(false, default, error);
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null);
    }

}
