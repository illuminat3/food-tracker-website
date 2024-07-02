using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;

namespace FoodTracker.Service.DataServices;

public class UserService : IUserService
{
    private User? _user;
    
    public Task<User?> GetCurrentUser()
    {
        return Task.FromResult(_user);
    }

    public Task<bool> SetCurrentUser(User user)
    {
        try
        {
            _user = user;
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}