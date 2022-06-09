IF EXISTS(SELECT 1 FROM sys.objects WHERE name='Wb_Waybill' AND type='U')
	DROP TABLE Wb_Waybill

CREATE TABLE Wb_Waybill(
ID bigint NOT NULL,
CoID int NOT NULL,

Consignor nvarchar(50) NOT NULL, --发货方
ConsignorAddr nvarchar(100) NOT NULL,
ConsignorContact varchar(11) NOT NULL,

Consignee nvarchar(50) NOT NULL, --收货方
ConsigneeAddr nvarchar(100) NOT NULL,
ConsigneeContact varchar(11) NOT NULL,

PlateNo nvarchar(20) NOT NULL, --承运车辆
Driver nvarchar(50) NOT NULL,
DriverContact varchar(11) NOT NULL,

Amount decimal(9,2) NOT NULL,
ActualAmount decimal(9,2) NOT NULL,
Damage decimal(9,2) NOT NULL,
Deduction decimal(9,2) NOT NULL,

DeliveryTime datetime NULL,
ReceiptTime datetime NULL,

CONSTRAINT [PK_Waybil] PRIMARY KEY CLUSTERED(ID)
)
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name='Wb_WaybillCargo' AND type='U')
	DROP TABLE Wb_WaybillCargo

CREATE TABLE Wb_WaybillCargo(
ID bigint IDENTITY(1,1) NOT NULL,
CoID int NOT NULL,
WbID bigint NOT NULL,
Name nvarchar(50) NOT NULL,
Specification varchar(50) NULL,
Package varchar(20) NOT NULL,
PackageWeight decimal(9,2) NOT NULL,
PackageNumber int NOT NULL,
PackageDimension varchar(64) NOT NULL,
Price decimal(9,2) NOT NULL,
Number decimal(9,2) NOT NULL,
Unit varchar(20) NOT NULL,
Damage decimal(9,2) NOT NULL,
CONSTRAINT [PK_WaybillCargo] PRIMARY KEY CLUSTERED(ID)
)
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE name='Wb_Contractor' AND type='U')
	DROP TABLE Wb_Contractor

CREATE TABLE Wb_Contractor(
ID int IDENTITY(1,1) NOT NULL,
CoID int NOT NULL,
Name nvarchar(50) NOT NULL,
Addr nvarchar(100) NOT NULL,
Contact varchar(11) NOT NULL,
IsConsignor bit NOT NULL,
IsConsignee bit NOT NULL,
CONSTRAINT [PK_Contractor] PRIMARY KEY CLUSTERED(ID)
)
GO