namespace Joker.Shared.Models.Enums
{
    public enum ResponseMessageType
    {
        Stateless = 0,
        Success = 200,
        NoContent = 204,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        Cancelled = 406,
        Conflict = 409,
        ServerError = 500,
        RecordIsDeleted = 800,
        RecordIsAdded = 801,
        RecordIsUpdated = 802,
    }
}
