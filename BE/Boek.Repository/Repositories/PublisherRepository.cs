using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using System.Xml.Linq;

namespace Boek.Repository.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public Publisher CheckDuplicatedPublisherEmail(string email) => dbSet.SingleOrDefault(u =>
            u.Email.ToLower().Trim().Equals(email.ToLower().Trim()));

        public Publisher CheckDuplicatedPublisherName(string name) => dbSet.SingleOrDefault(u =>
            u.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }
}
