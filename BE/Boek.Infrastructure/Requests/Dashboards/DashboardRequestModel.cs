namespace Boek.Infrastructure.Requests.Dashboards
{
    public class DashboardRequestModel
    {
        public List<TimeLineRequestModel> TimeLine { get; set; }
        /// <summary>
        /// Size Subject is a subject's size (Ex. School is subject, and Students are its data)
        /// </summary>
        public int? SizeSubject { get; set; } = 1;
        /// <summary>
        /// Size Data is a data's size inside a subject (Ex. School is subject, and Students are its data)
        /// </summary>
        public int? SizeData { get; set; } = 3;
        public bool? IsDescendingData { get; set; }
        public bool? IsDescendingTimeLine { get; set; }
        public bool? SeparateDay { get; set; }

        public void SetDefaultSizeData(int Number = 3)
        {
            if (!SizeData.HasValue || SizeData <= 0)
                SizeData = Number;
        }
        public void SetDefaultSizeSubject(int Number = 1)
        {
            if (!SizeSubject.HasValue || SizeSubject <= 0)
                SizeSubject = Number;
        }

        public void SetDescendingData(bool Sort = true)
        {
            if (!IsDescendingData.HasValue)
                IsDescendingData = Sort;
        }
        public void SetDescendingTimeLine(bool Sort = false)
        {
            if (!IsDescendingTimeLine.HasValue)
                IsDescendingData = Sort;
        }
        public void SetSeparateDay(bool value = false)
        {
            if (!SeparateDay.HasValue)
                SeparateDay = value;
        }
    }
}