using System.Net;

namespace Joker.Exceptions;

public class NotFoundException : StatusCodeException
{
    public  NotFoundException()
    {
            
    }
        
    public NotFoundException(string message) 
        :base(message)
    {
        StatusCode = (int) HttpStatusCode.NotFound;
    }
}