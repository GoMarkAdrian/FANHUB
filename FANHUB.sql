CREATE DATABASE FANHUB

USE FANHUB

CREATE TABLE Users(
	UserID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name VARCHAR(50) NULL,
	Username VARCHAR(50) NULL UNIQUE,
	Mobile VARCHAR(50) NULL,
	Email VARCHAR(50) NULL UNIQUE,
	Address VARCHAR(MAX) NULL,
	PostCode VARCHAR(50) NULL,
	Password VARCHAR(50) NULL,
	ImageURL VARCHAR(MAX) NULL,
	CreatedDate DATETIME NULL,
)

CREATE TABLE Contact(
	ContactID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name VARCHAR(50) NULL,
	Email VARCHAR(50) NULL,
	Subject VARCHAR(100) NULL,
	Message VARCHAR(MAX) NULL,
	CreatedDate DATETIME NULL,
)

CREATE TABLE Categories(
	CategoryID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name VARCHAR(50) NULL,
	ImageURL VARCHAR(MAX) NULL,
	IsActive BIT NULL, 
	CreatedDate DATETIME NULL,
)

CREATE TABLE Products(
	ProductID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name VARCHAR(50) NULL,
	Description VARCHAR(MAX) NULL,
	Price DECIMAL(18,2) NULL,
	Quantity INT NULL,
	ImageURL VARCHAR(MAX) NULL,
	CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID) ON DELETE CASCADE NULL, --FK
	IsActive BIT NULL,
	CreatedDate DATETIME NULL,
)

CREATE TABLE Carts(
	CartID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ProductID INT FOREIGN KEY REFERENCES Products(ProductID) ON DELETE CASCADE NULL, --FK
	Quantity INT NULL,
	UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE NULL,  --FK
)

CREATE TABLE Payment(
	PaymentID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name VARCHAR(50) NULL,
	CardNo VARCHAR(50) NULL,
	ExpiryDate VARCHAR(50) NULL,
	CvvNo INT NULL,
	Address VARCHAR(MAX) NULL,
	PaymentMode VARCHAR(50)NULL,
)

CREATE TABLE Orders(
	OderID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	OrderNo VARCHAR(MAX) NULL,
	ProductID INT FOREIGN KEY REFERENCES Products(ProductID) ON DELETE CASCADE NULL, --FK
	Quantity INT NULL,
	UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE NULL,  --FK
	Status VARCHAR(50),
	PaymentID INT FOREIGN KEY REFERENCES Payment(PaymentID) ON DELETE CASCADE NULL, --FK
	OrderDate DATETIME NULL,
)
