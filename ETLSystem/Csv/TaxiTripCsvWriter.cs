using CsvHelper;
using CsvHelper.Configuration;
using ETLSystem.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ETLSystem.Csv
{
    public class TaxiTripCsvWriter
    {
        public CsvConfiguration Configuration { get; }
        public string WritePath { get; }

        public TaxiTripCsvWriter(string writePath)
        {
            Configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            WritePath = writePath;
        }

        public async Task WriteAllAsync(IEnumerable<TaxiTrip> taxiTrips)
        {
            using (var writer = new StreamWriter(WritePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                await csv.WriteRecordsAsync(taxiTrips);
            }

        }
    }
}
