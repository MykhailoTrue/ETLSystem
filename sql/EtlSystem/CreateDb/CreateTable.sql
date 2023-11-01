USE EtlSystemDb;

IF OBJECT_ID(N'TaxiTrips', N'U') IS NOT NULL
	DROP TABLE TaxiTrips;
CREATE TABLE TaxiTrips (
    Id INT IDENTITY(1,1),
    PickupDateTime DATETIME,
    DropoffDateTime DATETIME,
    PassengerCount INT,
    TripDistance FLOAT,
    StoreAndFwdFlag NVARCHAR(10),
    PickUpLocationId INT,
    DropOffLocationId INT,
    FareAmount FLOAT,
    TipAmount FLOAT,
	
	CONSTRAINT PK_TaxiTrips_Id PRIMARY KEY (Id),
);

CREATE INDEX IX_PickupDateTime ON TaxiTrips (PickupDateTime);
CREATE INDEX IX_TripDistance ON TaxiTrips (TripDistance);
CREATE INDEX IX_DropoffDateTime ON TaxiTrips (DropoffDateTime);
CREATE INDEX IX_PULocationId ON TaxiTrips (PickUpLocationId);
CREATE INDEX IX_TipAmount ON TaxiTrips (TipAmount);