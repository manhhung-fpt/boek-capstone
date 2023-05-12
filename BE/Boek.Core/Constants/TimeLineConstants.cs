namespace Boek.Core.Constants
{
    public class TimeLineConstants
    {
        #region Season

        #region start month
        public static DateTime STRING_START_MONTH = new DateTime(DateTime.Now.Year, 1, 1);
        public static DateTime SUMMER_START_MONTH = new DateTime(DateTime.Now.Year, 4, 1);
        public static DateTime FALL_START_MONTH = new DateTime(DateTime.Now.Year, 7, 1);
        public static DateTime WINTER_START_MONTH = new DateTime(DateTime.Now.Year, 10, 1);
        #endregion

        #region end month
        public static DateTime STRING_END_MONTH = new DateTime(DateTime.Now.Year, 3, 31, 23, 59, 59);
        public static DateTime SUMMER_END_MONTH = new DateTime(DateTime.Now.Year, 6, 30, 23, 59, 59);
        public static DateTime FALL_END_MONTH = new DateTime(DateTime.Now.Year, 9, 30, 23, 59, 59);
        public static DateTime WINTER_END_MONTH = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
        #endregion

        #endregion
    }
}