using ETLSystem.Csv;
using ETLSystem.DataHandlers;
using ETLSystem.Repositories;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace ETLSystem
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var csvPath = configuration.GetRequiredSection("CsvPath").Value!;
            var duplicatePath = configuration.GetRequiredSection("DuplicatePath").Value!;
            var connectionString = configuration.GetConnectionString("ConnectionString")!;

            var csv = new TaxiTripCsvReader(csvPath);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var readed = csv.ReadAll();

            var handler = new TaxiTripDataHandler(new TaxiTripCsvWriter(duplicatePath));
            var handled = (await handler.HandleAsync(readed)).ToList();

            var repository = new TaxiTripRepository(connectionString);
            repository.BulkInsert(handled);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}