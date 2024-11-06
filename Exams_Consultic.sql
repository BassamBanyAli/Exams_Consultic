create database Exams_Consultic

create table Currency (
CurrencyCode NVARCHAR(3) PRIMARY KEY,
CurrencyName NVARCHAR(15) NOT NULL
)

create table Vendor(
VendorId NVARCHAR(20) PRIMARY KEY,
VendorName NVARCHAR(160)
)

CREATE TABLE PurchaseOrderHeader (
PurchId NVARCHAR(20) PRIMARY KEY,
Vendor NVARCHAR(20),
CurrencyCode NVARCHAR(3),
Date DATE, 
FOREIGN KEY (Vendor) REFERENCES Vendor(vendorId),
FOREIGN KEY (CurrencyCode) REFERENCES Currency(CurrencyCode)
)

CREATE TABLE PurchaseOrderLines (
PurchId NVARCHAR(20),
ItemId NVARCHAR(30),
Qty DECIMAL(18, 2),
UnitPrice DECIMAL(18, 2),
Amount AS (Qty * UnitPrice) PERSISTED,
FOREIGN KEY (PurchId) REFERENCES PurchaseOrderHeader(PurchId)
)