namespace FoodTracker.Data.DTOs.Transfer;

public class Response(bool isSuccess, string? message = null)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public string? Message { get; set; } = message;
}