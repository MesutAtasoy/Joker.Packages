namespace Joker.Exceptions;

public class JokerException : StatusCodeException
{
    public JokerException()
    {
            
    }
        
    public JokerException(string message) 
        :base(message)
    {
        StatusCode = 400;
    }
        
    public JokerException(string message, int statusCode) 
        :base(message)
    {
        StatusCode = statusCode;
    }
}