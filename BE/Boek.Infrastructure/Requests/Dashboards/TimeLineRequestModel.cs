using Boek.Core.Enums;

namespace Boek.Infrastructure.Requests.Dashboards
{
    public class TimeLineRequestModel
    {
        public byte? Type { get; set; } = (byte)TimeLineType.Day;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TimeLength { get; set; }
        public byte? SeasonType { get; set; }
        public int? Year { get; set; } = DateTime.Now.Year;

        #region Methods
        public bool IsEmptyTimeDetail()
            => !this.StartDate.HasValue && !this.EndDate.HasValue && !this.TimeLength.HasValue && !this.SeasonType.HasValue;
        public bool IsNotEmptyStartAndEndTimeDetail()
            => this.StartDate.HasValue && this.EndDate.HasValue;
        public bool IsNotEmptyStartTimeDetail()
            => this.StartDate.HasValue && this.TimeLength.HasValue;
        public bool IsNotEmptyEndTimeDetail()
            => this.StartDate.HasValue && this.TimeLength.HasValue;
        public bool IsValidDateTime(bool? IsStartDate = null)
        {
            DateTime temp;
            //Both
            if (!IsStartDate.HasValue)
                return DateTime.TryParse(this.StartDate.ToString(), out temp) &&
            DateTime.TryParse(this.EndDate.ToString(), out temp);
            //Start date
            else if ((bool)IsStartDate)
                return DateTime.TryParse(this.StartDate.ToString(), out temp);
            //End date
            return DateTime.TryParse(this.EndDate.ToString(), out temp);
        }

        public bool IsRightOrderDate()
            => DateTime.Compare((DateTime)this.StartDate, (DateTime)this.EndDate) < 0;
        public bool IsValidTimeLength()
            => this.TimeLength > 0;
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
                ((DateTime)this.StartDate).AddYears(((int)interval));
                ((DateTime)this.EndDate).AddYears(((int)interval));
            }
        }
        #endregion
    }
}