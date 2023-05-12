using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ILevelRepository : IGenericRepository<Level>
    {
        Level CheckDuplicatedLevelName(string name);
        Level CheckDuplicatedLevelConditionalPoint(int? conditionalPoint);
    }
}
