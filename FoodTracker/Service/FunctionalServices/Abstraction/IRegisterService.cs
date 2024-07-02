using FoodTracker.Data.DTOs;

namespace FoodTracker.Service.FunctionalServices.Abstraction;

public interface IRegisterService
{
    Task<bool> Register(User user);
}