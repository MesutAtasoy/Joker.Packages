namespace Joker.Shared.Models.Enums
{
    public enum EventBaseState
    {
        Stateless = 0,
        Success = 200,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
        Cancelled = 406,
        Conflict = 409,
        ServerError = 500,
        RecordIsDeleted = 800,
        RecordIsAdded = 801,
        RecordIsUpdated = 802,
    }
}
