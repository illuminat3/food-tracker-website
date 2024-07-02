using FoodTracker.Data.DTOs;
using FoodTracker.Data.DTOs.Transfer;

namespace FoodTracker.Service.FunctionalServices.Abstraction;

public interface IRegisterService
{
    Task<Response> Register(User user);
}