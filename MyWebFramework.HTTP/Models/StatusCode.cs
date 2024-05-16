namespace MyWebFramework.HTTP.Models
{
    public enum StatusCode
    {
        OK = 200,
        Created = 201,
        MovedPermanetly = 301,
        Found = 302,
        TemporaryRedirect = 307,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
    }
}