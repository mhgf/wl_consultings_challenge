namespace WlChallenge.Api.Shared.Response;

public class Response
{
    public IEnumerable<string> Errors { get; }


    protected Response(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public static Response<T> Ok<T>(T data) => new(data, []);
    public static Response Fail(IEnumerable<string> errors) => new(errors);
}

public class Response<T> : Response
{
    public T? Data { get; }

    protected internal Response(T data, IEnumerable<string> errors) : base(errors)
    {
        Data = data;
    }
}