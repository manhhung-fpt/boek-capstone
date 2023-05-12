using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class IssuerRepository : GenericRepository<Issuer>, IIssuerRepository
    {
        public IssuerRepository(BoekCapstoneContext context) : base(context)
        {
        }
    }
}
