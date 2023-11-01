using CsvHelper.Configuration;
using CsvHelper;
using ETLSystem.Models;
using System.Globalization;
using ETLSystem.Csv;
using ETLSystem.Extensions;

namespace ETLSystem.DataHandlers
{
    public class TaxiTripDataHandler
    {
        public TaxiTripCsvWriter CsvWriter { get; }

        public TaxiTripDataHandler(TaxiTripCsvWriter csvWriter)
        {
            CsvWriter = csvWriter;
        }

        public async Task<IEnumerable<TaxiTrip>> HandleAsync(IEnumerable<TaxiTrip> taxiTripes)
        {
            await WriteDuplcatesToFile(taxiTripes);
            var unique = RemoveDuplicates(taxiTripes);
            return unique
                .ConvertStoreAndFwdFlag()
                .ConvertToUtcTimeZone()
                .RemoveWhitespaceFromTextFields();
        }

        private async Task WriteDuplcatesToFile(IEnumerable<TaxiTrip> taxiTrips)
        {
            var grouped = taxiTrips
                .GroupBy(t => new { t.PickupDateTime, t.DropoffDateTime, t.PassengerCount })
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1));

            await CsvWriter.WriteAllAsync(grouped);
        }

        private IEnumerable<TaxiTrip> RemoveDuplicates(IEnumerable<TaxiTrip> taxiTrips)
        {
            var distinctTrips = taxiTrips
                .GroupBy(t => new { t.PickupDateTime, t.DropoffDateTime, t.PassengerCount })
                .Select(group => group.First());

            return distinctTrips;
        }
    }
}
