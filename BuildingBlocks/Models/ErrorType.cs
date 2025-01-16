namespace BuildingBlocks.Models;

public enum ErrorType
{
    None,
    Conflict,
    NotFound,
    Validation,
    DatabaseError,
    InternalServerError
}