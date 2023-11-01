using CsvHelper;
using CsvHelper.Configuration;
using ETLSystem.Models;
using System.Globalization;

namespace ETLSystem.Csv
{
    public class TaxiTripCsvReader
    {
        public CsvConfiguration Configuration { get; }
        public string ReadPath { get; }

        public TaxiTripCsvReader(string readPath)
        {
            Configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            ReadPath = readPath;
        }

        public IEnumerable<TaxiTrip> ReadAll()
        {
            using (var reader = new StreamReader(ReadPath))
            using (var csv = new CsvReader(reader, Configuration))
            {
                var records = csv.GetRecords<TaxiTrip>();
                return records.ToList();
            }
        }
    }
}
