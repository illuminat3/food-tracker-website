namespace FoodTracker.Data.DTOs;

public class Response
{
    public bool isSuccess { get; set; }
    public string? message { get; set; }
    
    public Response(bool isSuccess, string? message)
    {
        this.isSuccess = isSuccess;
        this.message = message;
    }

    public Response(bool isSuccess)
    {
        this.isSuccess = isSuccess;
    }
}