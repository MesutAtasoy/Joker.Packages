using System.Text;
using DotNetCore.CAP.Messages;
using DotNetCore.CAP.Serialization;
using Joker.Exceptions;
using Joker.Extensions;
using Newtonsoft.Json;

namespace Joker.CAP.Serializer;

public class CAPJsonSerializer : ISerializer
{
    private readonly JsonSerializerSettings _serializerSettings;

    public CAPJsonSerializer()
    {
        _serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new ReadOnlyContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };
    }

    public Task<TransportMessage> SerializeAsync(Message message)
    {
        Check.NotNull(message, nameof(message));

        if (message.Value == null)
        {
            return Task.FromResult(new TransportMessage(message.Headers, null));
        }

        string jsonBody = JsonConvert.SerializeObject(message.Value, message.Value.GetType(), _serializerSettings);
        var transportMessage = new TransportMessage(message.Headers, Encoding.UTF8.GetBytes(jsonBody));

        return Task.FromResult(transportMessage);
    }

    public Task<Message> DeserializeAsync(TransportMessage transportMessage, Type valueType)
    {
        if (valueType == null || transportMessage.Body == null)
        {
            return Task.FromResult(new Message(transportMessage.Headers, null));
        }

        string serializeBody = Encoding.UTF8.GetString(transportMessage.Body);
        object deserializeObj = JsonConvert.DeserializeObject(serializeBody, valueType, _serializerSettings);

        return Task.FromResult(new Message(transportMessage.Headers, deserializeObj));
    }

    public string Serialize(Message message)
    {
        return JsonConvert.SerializeObject(message, _serializerSettings);
    }

    public Message Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Message>(json, _serializerSettings);
    }

    public object Deserialize(object value, Type valueType)
    {
        if (!IsJsonType(value))
        {
            throw new NotSupportedException("Type is not of type json");
        }

        return Deserialize(value.ToString());
    }

    public bool IsJsonType(object jsonObject)
    {
        if (jsonObject is string jsonString)
            return jsonString.IsValidJson();

        return false;
    }
}