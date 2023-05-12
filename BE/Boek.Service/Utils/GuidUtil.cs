using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boek.Service.Utils
{
    public static class GuidUtil
    {
        public static bool HasValue(this Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
                return false;
            return true;
        }
    }
}
