namespace FoodTracker.Data.DTOs.Transfer;

public class Error (bool isActive, string? message = null)
{
    public bool IsActive { get; set; } = isActive;
    public string? Message { get; set; } = message;
}