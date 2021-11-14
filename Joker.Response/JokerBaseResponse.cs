namespace Joker.Response;

public class JokerBaseResponse : JokerBaseResponse<object>
{
}
    
public class JokerBaseResponse<T>
{
    public JokerBaseResponse()
    {
    }

    public JokerBaseResponse(T payload) 
        : this()
    {
        Payload = payload;
    }
        
    public JokerBaseResponse(T payload, int statusCode)
        : this()
    {
        Payload = payload;
        StatusCode = statusCode;
    }
        
        
    public JokerBaseResponse(T payload, int statusCode, string message)
        : this()
    {
        Payload = payload;
        StatusCode = statusCode;
        Message = message;
    }
        
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Payload { get; set; }
}