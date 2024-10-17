CREATE DATABASE EuroBuld;

USE EuroBuld;

CREATE TABLE Role (
    ID_Role INT PRIMARY KEY IDENTITY(1,1),
    roll_name VARCHAR(20),
  salary VARCHAR(20)
);

CREATE TABLE Users (
    ID_Users INT PRIMARY KEY IDENTITY(1,1),
  ID_Company INT,
    Login VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,
    Email VARCHAR(40),
    First_name VARCHAR(20),
    Last_name VARCHAR(20),
    Patronymic VARCHAR(20),
    Passport_details VARCHAR(20),
    Date_birth DATE
);

CREATE TABLE Staff (
    ID_Staff INT PRIMARY KEY IDENTITY(1,1),
    ID_Users INT,
    ID_Role INT,
  Login VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,
    Email VARCHAR(40),
    First_name VARCHAR(20),
    Last_name VARCHAR(20),
    Patronymic VARCHAR(20),
    Passport_details VARCHAR(20),
    Date_birth DATE,
    Date_employment DATE,
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

CREATE TABLE Construction_Status (
    ID_Construction_Status INT PRIMARY KEY IDENTITY(1,1),
    Status_Description VARCHAR(50) NOT NULL 
);


CREATE TABLE Processed_customer_orders (
    ID_Processed_customer_orders INT PRIMARY KEY IDENTITY(1,1),
    ID_Customer_orders INT,
    ID_Staff INT,
  ID_Construction_Status INT,
    Project_Name VARCHAR(20),
    Date_Start DATE,
    Date_Ending DATE,
  Final_sum VARCHAR(20),
    FOREIGN KEY (ID_Customer_orders) REFERENCES Customer_orders(ID_Customers_orders),
    FOREIGN KEY (ID_Staff) REFERENCES Staff(ID_Staff),
  FOREIGN KEY (ID_Construction_Status) REFERENCES Construction_Status(ID_Construction_Status),
);


ALTER AUTHORIZATION ON DATABASE::EuroBuld TO sa;