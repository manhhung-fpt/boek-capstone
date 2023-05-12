using Boek.Core.Constants;
using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        public List<Schedule> GetSchedules(int? CampaignId, List<byte?> status);
        public List<Province> GetProvincesFromSchedules(List<Schedule> schedules, List<Province> provinces = null);
    }
}