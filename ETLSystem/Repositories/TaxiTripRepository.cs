using Dapper;
using ETLSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLSystem.Repositories
{
    public class TaxiTripRepository
    {
        private readonly string _connectionString;

        public TaxiTripRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void BulkInsert(IEnumerable<TaxiTrip> taxiTrips)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    var query = @"INSERT INTO TaxiTrips 
                        (PickupDateTime, DropoffDateTime, PassengerCount, TripDistance, StoreAndFwdFlag, PickUpLocationId, DropOffLocationId, FareAmount, TipAmount) 
                        VALUES
                        (@PickupDateTime, @DropoffDateTime, @PassengerCount, @TripDistance, @StoreAndFwdFlag, @PickUpLocationId, @DropOffLocationId, @FareAmount, @TipAmount)";
                    dbConnection.Execute(
                        query, 
                        param: taxiTrips,
                        transaction: transaction);

                    transaction.Commit();
                }
            }
        }
    }
}
