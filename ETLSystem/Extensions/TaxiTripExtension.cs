using ETLSystem.Models;

namespace ETLSystem.Extensions
{
    public static class TaxiTripExtension
    {
        public static IEnumerable<TaxiTrip> ConvertStoreAndFwdFlag(this IEnumerable<TaxiTrip> taxiTrips)
        {
            return taxiTrips.Select(t =>
            {
                t.StoreAndFwdFlag = t.StoreAndFwdFlag == "N" ? "No" : t.StoreAndFwdFlag == "Y" ? "Yes" : t.StoreAndFwdFlag;
                return t;
            });
        }

        public static IEnumerable<TaxiTrip> RemoveWhitespaceFromTextFields(this IEnumerable<TaxiTrip> taxiTrips)
        {
            return taxiTrips.Select(t =>
            {
                var type = t.GetType();
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string)property.GetValue(t);
                        if (value != null)
                        {
                            property.SetValue(t, value.Trim());
                        }
                    }
                }

                return t;
            });
        }

        public static IEnumerable<TaxiTrip> ConvertToUtcTimeZone(this IEnumerable<TaxiTrip> taxiTrips)
        {
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var utcTimeZone = TimeZoneInfo.Utc;

            return taxiTrips.Select(t =>
            {
                t.PickupDateTime = TimeZoneInfo.ConvertTimeToUtc(t.PickupDateTime, estTimeZone);
                t.DropoffDateTime = TimeZoneInfo.ConvertTimeToUtc(t.DropoffDateTime, estTimeZone);
                return t;
            });
        }
    }
}
