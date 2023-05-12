using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool CheckDuplicatedEmail(string email);
    }
}
