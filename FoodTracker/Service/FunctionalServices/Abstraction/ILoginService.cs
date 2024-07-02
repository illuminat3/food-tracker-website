using FoodTracker.Data.DTOs;
using FoodTracker.Data.DTOs.Transfer;

namespace FoodTracker.Service.FunctionalServices.Abstraction;

public interface ILoginService
{
    Task<Response> Login(User user);
}