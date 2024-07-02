using FoodTracker.Data.DTOs;

namespace FoodTracker.Service.FunctionalServices.Abstraction;

public interface ILoginService
{
    Task<User> Login(User user);
}