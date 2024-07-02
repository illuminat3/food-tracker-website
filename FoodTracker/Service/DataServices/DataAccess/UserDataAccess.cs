using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.DataAccess.Abstraction;

namespace FoodTracker.Service.DataServices.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    public Task<bool> Insert(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }
}