using FoodTracker.Data.DTOs;

namespace FoodTracker.Service.FunctionalServices.Abstraction;

public interface ILoginService
{
    Task<Response> Login(User user);
}