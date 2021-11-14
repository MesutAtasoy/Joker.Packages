using System.Runtime.Serialization;

namespace Joker.ElasticSearch.Exceptions;

[Serializable]
public class ElasticSearchException : Exception
{
    public ElasticSearchException()
    {
    }

    public ElasticSearchException(SerializationInfo serializationInfo, StreamingContext context) : base(
        serializationInfo, context)
    {
    }

    public ElasticSearchException(string message) : base(message)
    {
    }

    public ElasticSearchException(string message, Exception innerException) : base(message, innerException)
    {
    }
}