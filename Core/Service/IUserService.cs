using Omu.ProDinner.Core.Model;

namespace Omu.ProDinner.Core.Service
{
    public interface IUserService : ICrudService<User>
    {
        bool IsUnique(string login);
        
        void ChangePassword(int id, string password);
        
        user Get(string login, string password);
    }
}
