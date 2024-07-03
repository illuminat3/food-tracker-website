using System.Security.Cryptography;
using System.Text;
using FoodTracker.Service.FunctionalServices.Abstraction;

namespace FoodTracker.Service.FunctionalServices;

public class HashService : IHashService
{
    public string HashString(string input)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}