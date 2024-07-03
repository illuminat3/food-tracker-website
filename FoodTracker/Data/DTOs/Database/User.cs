using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Data.DTOs
{
    public class User
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string? Username { get; set; }
        [Required] public string? Email { get; set; }
        [Required] public string? HashedPassword { get; set; }
        [Required] public bool IsAdmin { get; set; } = false;

        public bool IsMatch(User user)
        {
            return user.Id == Id && user.Username == Username && user.Email == Email && user.HashedPassword == HashedPassword;
        }
    }
}
