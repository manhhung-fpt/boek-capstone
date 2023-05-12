using Boek.Repository.Interfaces;
using Boek.Core.Entities;
using Boek.Core.Data;
using Boek.Core.Constants;

namespace Boek.Repository.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<Schedule> GetSchedules(int? CampaignId, List<byte?> status)
        {
            var result = dbSet.Where(s => s.CampaignOrganization.CampaignId.Equals(CampaignId));
            if (result.Any() && status.Any())
                result = result.Where(s => s.Status.HasValue && status.Contains(s.Status));
            return result.Any() ? result.ToList() : null;
        }

        public List<Province> GetProvincesFromSchedules(List<Schedule> schedules, List<Province> provinces = null)
        {
            var addresses = schedules.Where(s => !string.IsNullOrEmpty(s.Address))
            .Select(s => s.Address.Split(",").Last().ToLower().Trim()).GroupBy(a => a);
            if (addresses.Any())
            {
                var keys = addresses.Select(a => a.Key);
                var result = ProvincesList.PROVINCES.Where(p => keys.Contains(p.NameWithType.ToLower().Trim()));
                if (result.Any())
                {
                    if (provinces != null)
                    {
                        if (provinces.Any())
                        {
                            result = result.Except(provinces);
                            if (result.Any())
                                provinces.AddRange(result);
                            return provinces;
                        }
                    }
                    return result.ToList();
                }
            }
            return null;
        }
    }
}