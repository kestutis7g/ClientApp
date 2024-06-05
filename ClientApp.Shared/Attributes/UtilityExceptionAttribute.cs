namespace ClientApp;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ClientAppExceptionAttribute : Attribute
{
    public int StatusCode { get; }
    public string? ErrorCode { get; }
    public string? Message { get; set; }

    public ClientAppExceptionAttribute(int statusCode, string errorCode)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    public ClientAppExceptionAttribute(int statusCode, string errorCode, string message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Message = message;
    }

    public ClientAppExceptionAttribute(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ClientAppExceptionAttribute()
    {
    }
}
