using FoodTracker.Data.DTOs;

namespace FoodTracker.Service.FunctionalServices.Abstraction;

public interface IRegisterService
{
    Task<Response> Register(User user);
}