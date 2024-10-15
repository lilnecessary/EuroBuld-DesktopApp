CREATE DATABASE EuroBuld;

USE EuroBuld;

CREATE TABLE Role (
    ID_Role INT PRIMARY KEY IDENTITY(1,1),
    Job_title VARCHAR(20),
    Wages VARCHAR(20)
);

CREATE TABLE Companies (
    ID_Company INT PRIMARY KEY IDENTITY(1,1),
    Company_Name VARCHAR(50) NOT NULL,
    Address VARCHAR(100),
    Phone_Number VARCHAR(20),
    Email VARCHAR(40)
);

CREATE TABLE Users (
    ID_Users INT PRIMARY KEY IDENTITY(1,1),
    Login VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,
    Email VARCHAR(40),
    First_name VARCHAR(20),
    Last_name VARCHAR(20),
    Patronymic VARCHAR(20),
    Passport_details VARCHAR(20),
    Date_birth DATE,
    ID_Company INT,
    FOREIGN KEY (ID_Company) REFERENCES Companies(ID_Company) -- Связь с таблицей Companies
);

CREATE TABLE Staff (
    ID_Staff INT PRIMARY KEY IDENTITY(1,1),
    ID_Users INT,
    ID_Role INT,
    Date_employment DATE,
    FOREIGN KEY (ID_Users) REFERENCES Users(ID_Users),
    FOREIGN KEY (ID_Role) REFERENCES Role(ID_Role)
);

CREATE TABLE Service (
    ID_Service INT PRIMARY KEY IDENTITY(1,1),
    Item_Name VARCHAR(20) NOT NULL,
    Item_Description VARCHAR(50) NOT NULL,
    Price VARCHAR(10) NOT NULL,
    Image VARBINARY(MAX)
);

CREATE TABLE Customer_orders (
    ID_Customers_orders INT PRIMARY KEY IDENTITY(1,1),
    ID_Service INT,
    ID_Users INT,
    Order_Date DATE,
    Quantity VARCHAR(20),
    FOREIGN KEY (ID_Service) REFERENCES Service(ID_Service),
    FOREIGN KEY (ID_Users) REFERENCES Users(ID_Users)
);

CREATE TABLE Processed_customer_orders (
    ID_Processed_customer_orders INT PRIMARY KEY IDENTITY(1,1),
    ID_Customer_orders INT,
    ID_Staff INT,
    Project_Name VARCHAR(20),
    Construction_Status VARCHAR(20),
    Date_Start DATE,
    Date_Ending DATE,
    FOREIGN KEY (ID_Customer_orders) REFERENCES Customer_orders(ID_Customers_orders),
    FOREIGN KEY (ID_Staff) REFERENCES Staff(ID_Staff)
);

CREATE TABLE Requests (
    ID_Request INT PRIMARY KEY IDENTITY(1,1),
    ID_Users INT NOT NULL,
    ID_Staff INT NOT NULL,
    Request_Date DATETIME DEFAULT GETDATE(),
    Request_Content VARCHAR(255) NOT NULL,
    Status VARCHAR(20),
    FOREIGN KEY (ID_Users) REFERENCES Users(ID_Users),
    FOREIGN KEY (ID_Staff) REFERENCES Staff(ID_Staff)
);

ALTER AUTHORIZATION ON DATABASE::EuroBuld TO sa;
