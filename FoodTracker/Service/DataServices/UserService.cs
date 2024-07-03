using Blazored.LocalStorage;
using FoodTracker.Data.DTOs;
using FoodTracker.Service.DataServices.Abstraction;
using System.Text.Json;
using FoodTracker.Service.DataServices.DataAccess.Abstraction;

namespace FoodTracker.Service.DataServices
{
    public class UserService : IUserService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IUserDataAccess _userDataAccess;
        private const string UserKey = "foodTrackerCurrentUser";
        private User? _user;
        
        public UserService(ILocalStorageService localStorage, IUserDataAccess userDataAccess)
        {
            _localStorage = localStorage;
            _userDataAccess = userDataAccess;
        }
        
        public async Task<User?> GetCurrentUser()
        {
            try
            {
                if (_user != null)
                {
                    return _user;
                }
                
                var userJson = await _localStorage.GetItemAsStringAsync(UserKey);
                
                if (string.IsNullOrWhiteSpace(userJson))
                {
                    return null;
                }
                
                var user = JsonSerializer.Deserialize<User>(userJson);
                
                if (user == null)
                {
                    return null;
                }
                
                var tempUser = await _userDataAccess.Get(user.Id);
                
                if (tempUser == null)
                {
                    return null;
                }
                
                if (user.IsMatch(tempUser))
                {
                    _user = tempUser;
                }

                return _user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SetCurrentUser(User? user)
        {
            try
            {
                if (user == null)
                {
                    await _localStorage.RemoveItemAsync(UserKey);
                }
                else
                {
                    var userJson = JsonSerializer.Serialize(user);
                    await _localStorage.SetItemAsStringAsync(UserKey, userJson);
                }
                
                _user = user;
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}