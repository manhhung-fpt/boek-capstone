using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class LevelRepository : GenericRepository<Level>, ILevelRepository
    {
        public LevelRepository(BoekCapstoneContext context) :
            base(context)
        {
        }

        public Level CheckDuplicatedLevelConditionalPoint(int? conditionalPoint) =>
            dbSet.SingleOrDefault(u =>
                    u.ConditionalPoint.Equals(conditionalPoint));

        public Level CheckDuplicatedLevelName(string name) =>
            dbSet.SingleOrDefault(u =>
                    u.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }
}
