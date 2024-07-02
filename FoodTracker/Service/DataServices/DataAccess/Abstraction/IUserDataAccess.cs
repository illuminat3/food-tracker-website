using FoodTracker.Data.DTOs;

namespace FoodTracker.Service.DataServices.DataAccess.Abstraction;

public interface IUserDataAccess
{
    Task<bool> Insert(User user);
    
    Task<bool> Update(User user);
    
    Task<bool> Delete(User user);
    
    Task<User> Get(string userId);
    
    Task<IEnumerable<User>> GetAll();
    
    Task<User> GetByUsername(string username);
    
    Task<User> GetByEmail(string email);
}