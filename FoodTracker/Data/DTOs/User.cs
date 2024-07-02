namespace FoodTracker.Data.DTOs;

public class User
{
    public string Id { get; set; } = new Guid().ToString();
    public string Username { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
}