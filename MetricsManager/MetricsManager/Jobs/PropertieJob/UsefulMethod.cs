using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs.PropertieJob
{
    public static class UsefulMethod
    {
        public static DateTimeOffset CheckDateTime(DateTimeOffset time)
        {
            var minDateTime = new DateTimeOffset(1970, 01, 01, 00, 0, 0,TimeSpan.Zero) ;
            if (time > minDateTime)
            {
                return time;
            }
            else
            {
                return minDateTime;
            }
        }
    }
}
