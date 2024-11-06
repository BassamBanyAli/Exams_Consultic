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


INSERT INTO Currency (CurrencyCode, CurrencyName)
VALUES
('USD', 'United States'),
('EUR', 'Euro'),
('GBP', 'Pound')




INSERT INTO Vendor (VendorId, VendorName)
VALUES
('V001', 'Acme Corporation'),
('V002', 'Tech Supplies Ltd.'),
('V003', 'Global Enterprises'),
('V004', 'Retail Group'),
('V005', 'Office Depot');


INSERT INTO PurchaseOrderHeader (PurchId, Vendor, CurrencyCode, Date)
VALUES
('PO001', 'V001', 'USD', '2024-11-01'),
('PO002', 'V002', 'EUR', '2024-11-03'),
('PO003', 'V003', 'GBP', '2024-11-05');




INSERT INTO PurchaseOrderLines (PurchId, ItemId, Qty, UnitPrice)
VALUES
('PO001', 'ITEM001', 10, 15.50),
('PO001', 'ITEM002', 20, 25.00),
('PO002', 'ITEM003', 5, 100.00),
('PO002', 'ITEM004', 15, 50.00),
('PO003', 'ITEM005', 30, 45.75),
('PO003', 'ITEM006', 10, 60.00);



