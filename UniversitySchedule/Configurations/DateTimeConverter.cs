using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversitySchedule.API.Configurations
{
    public class DateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //var t = long.Parse((string)reader.Value);
            var dt = DateTime.MinValue;
            try
            {
                double.TryParse(reader.Value.ToString(), out double value);
                double t;
                if (value == 0)
                {
                    t = DateTime.Parse(reader.Value.ToString()).Ticks;//(long)reader.Value;
                }
                else
                {
                    t = value;
                }
                dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(t);
            }
            catch
            {
                // ignored
            }

            return dt;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            var ndt = (DateTime?)value;

            if (ndt == null)
            {
                writer.WriteValue("null");
                return;
            }

            var dt = ndt.Value;

            if (dt.Equals(DateTime.MinValue))
            {
                writer.WriteValue(-1);
                return;
            }

            var ml = ((DateTime)value).DateTimeToMilliseconds();

            writer.WriteValue(ml);
        }
    }

    public static class Extensions
    {
        public static long DateTimeToMilliseconds(this DateTime dateTime)
        {

            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var ml = (long)(dateTime - start).TotalMilliseconds;

            return ml;
        }
    }
}
