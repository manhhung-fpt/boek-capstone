using Boek.Core.Enums;
using Boek.Core.Extensions;

namespace Boek.Infrastructure.ViewModels.Dashboards
{
    public class TimeLineViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte? Type { get; set; } = (byte)TimeLineType.Day;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TimeLength { get; set; }
        public byte? SeasonType { get; set; }
        public int? Year { get; set; } = DateTime.Now.Year;

        #region Methods
        public bool IsEmptyTimeDetail()
            => !this.StartDate.HasValue && !this.EndDate.HasValue && !this.TimeLength.HasValue;
        public void ConvertDecimalToIntTimeLength()
        {
            if (this.Type.Equals((byte)TimeLineType.Day))
                this.TimeLength = ((int?)this.TimeLength);
            else
                this.TimeLength = (int)Math.Ceiling(((double)this.TimeLength));
        }

        public void ChangeYear()
        {
            if (this.Year != DateTime.Now.Year)
            {
                var interval = this.Year - DateTime.Now.Year;
                this.StartDate = ((DateTime)this.StartDate).AddYears(((int)interval));
                this.EndDate = ((DateTime)this.EndDate).AddYears(((int)interval));
            }
        }

        public void SetTitle(string Subject)
        {
            var TimeLineTypeName = StatusExtension<TimeLineType>.GetStatus(this.Type);
            this.Title = !String.IsNullOrEmpty(Subject) ? $"{Subject}_" : "";
            this.Title += $"{this.Year}_{TimeLineTypeName}";
            if (this.SeasonType.HasValue)
            {
                var SeasonTypeName = StatusExtension<SeasonTimeLine>.GetStatus(this.SeasonType);
                this.Title += $"_{SeasonTypeName}";
                Title = Title.Trim();
            }
        }
        #endregion
    }
}