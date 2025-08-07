public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Error { get; set; }
    public T Data { get; set; }

    public static Result<T> Success(T data) => new Result<T> { IsSuccess = true, Data = data };
    public static Result<T> Fail(string error) => new Result<T> { IsSuccess = false, Error = error };
}
