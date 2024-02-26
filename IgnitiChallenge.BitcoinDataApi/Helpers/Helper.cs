using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Helpers
{
    public static class Helper
    {
        private const int daysAgo = -31;

        public static string LocalTimeFormattedString()
        {
            return DateTime.UtcNow.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
        }

        public static long GetHistoricalDataStartDateTimestamp()
        {
            DateTime date31DaysAgo = DateTime.UtcNow.AddDays(daysAgo);
            DateTimeOffset dto = new DateTimeOffset(date31DaysAgo);

            return dto.ToUnixTimeMilliseconds();
        }

        public static long GetTimeStampFromDatetime(string date)
        {
            DateTime dateTime = DateTime.Parse(date).AddHours(12);
            DateTimeOffset dto = new DateTimeOffset(dateTime);

            return dto.ToUnixTimeMilliseconds();
        }
    }
}
