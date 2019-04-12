using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySchedule.DI.Managers
{
    public static class RequestManager
    {
        public static int GetUserTimeCorrection(HttpRequest request)
        {
            var correction = request.Headers["TimeZone"].ToString();

            if (string.IsNullOrEmpty(correction))
            {
                return 4;
            }

            else
            {
                try
                {
                    return Convert.ToInt32(correction);
                }
                catch (Exception)
                {
                    return 4;
                }
            }
        }

        public static string GetLanguage(HttpRequest request)
        {
            var language = request.Headers["lang"].ToString();

            return language;
        }
    }
}
