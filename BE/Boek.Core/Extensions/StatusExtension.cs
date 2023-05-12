using Boek.Core.Constants;

namespace Boek.Core.Extensions
{
    public static class StatusExtension<T> where T : Enum
    {
        public static string GetStringStatus(decimal? index, bool ShowErrorMessage = true, int language = MessageConstants.LANGUAGE_VN)
        {
            try
            {
                var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(t => ((int)Enum.Parse(typeof(T), t.ToString(), ignoreCase: true), t))
                .ToList();
                var result = list.SingleOrDefault(l => l.Item1.Equals((int)index)).t.ToEnumMemberAttrValue();
                result = GetOtherStringStatus(result, language);
                return result;
            }
            catch (Exception ex)
            {
                if (ShowErrorMessage)
                    throw new Exception(ex.Message);
                return null;
            }
        }
        public static string GetStringStatus(byte? index, bool ShowErrorMessage = true, int language = MessageConstants.LANGUAGE_VN)
        {
            try
            {
                var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(t => ((int)Enum.Parse(typeof(T), t.ToString(), ignoreCase: true), t))
                .ToList();
                var result = list.SingleOrDefault(l => l.Item1.Equals((int)index)).t.ToEnumMemberAttrValue();
                result = GetOtherStringStatus(result, language);
                return result;
            }
            catch (Exception ex)
            {
                if (ShowErrorMessage)
                    throw new Exception(ex.Message);
                return null;
            }
        }
        public static string GetStringStatus(bool? index, bool ShowErrorMessage = true, int language = MessageConstants.LANGUAGE_VN)
        {
            try
            {
                var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(t => ((int)Enum.Parse(typeof(T), t.ToString(), ignoreCase: true), t))
                .ToList();
                var result = (bool)index ? list[0].t.ToEnumMemberAttrValue() : list[1].t.ToEnumMemberAttrValue();
                result = GetOtherStringStatus(result, language);
                return result;
            }
            catch (Exception ex)
            {
                if (ShowErrorMessage)
                    throw new Exception(ex.Message);
                return null;
            }
        }

        private static string GetOtherStringStatus(string result, int language = MessageConstants.LANGUAGE_VN)
        {
            if (!string.IsNullOrEmpty(result) && result.Contains(";"))
            {
                if (language == MessageConstants.LANGUAGE_VN)
                {
                    var stringArrays = result.Split(";");
                    result = stringArrays.Any() ? stringArrays[language].Trim() : null;
                }
                else
                {
                    var stringArrays = result.Split(";");
                    result = stringArrays.Count() >= 2 ? stringArrays[language].Trim() : null;
                }
            }
            return result;
        }

        public static T GetEnumItemStatus(byte? index)
        {
            try
            {
                var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(t => ((int)Enum.Parse(typeof(T), t.ToString(), ignoreCase: true), t))
                .ToList();
                return list.SingleOrDefault(l => l.Item1.Equals((int)index)).t;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static T GetEnumItemStatus(bool? index)
        {
            try
            {
                var list = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(t => ((int)Enum.Parse(typeof(T), t.ToString(), ignoreCase: true), t))
                .ToList();
                return (bool)index ? list[0].t : list[1].t;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetStatus(decimal? Status, bool ShowErrorMessage = true, int language = MessageConstants.LANGUAGE_VN) => GetStringStatus(Status, ShowErrorMessage, language);
        public static string GetStatus(byte? Status, bool ShowErrorMessage = true, int language = MessageConstants.LANGUAGE_VN) => GetStringStatus(Status, ShowErrorMessage, language);
        public static string GetStatus(bool? Status, bool ShowErrorMessage = true, int language = MessageConstants.LANGUAGE_VN) => GetStringStatus(Status, ShowErrorMessage, language);
        public static T GetEnumStatus(byte? Status) => GetEnumItemStatus(Status);
        public static T GetEnumStatus(bool? Status) => GetEnumItemStatus(Status);
    }
}
