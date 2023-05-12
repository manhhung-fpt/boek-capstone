using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Publisher CheckDuplicatedPublisherName(string name);
        Publisher CheckDuplicatedPublisherEmail(string email);
    }
}
