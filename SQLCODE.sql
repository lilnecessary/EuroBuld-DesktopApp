CREATE DATABASE EuroBuld;

USE EuroBuld;


CREATE TABLE Users (
    ID_Users INT PRIMARY KEY IDENTITY(1,1),
	Email VARCHAR(40) NOT NULL,
    Password VARCHAR(20) NOT NULL,
	Number_Phone VARCHAR(20),
	Address VARCHAR(20),
    First_name VARCHAR(20),
    Last_name VARCHAR(20),
    Patronymic VARCHAR(20),
    Passport_details VARCHAR(20),
);

CREATE TABLE Role (
  ID_Role INT PRIMARY KEY IDENTITY(1,1),
  roll_name VARCHAR(20),
  salary VARCHAR(20)
);

CREATE TABLE Staff (
    ID_Staff INT PRIMARY KEY IDENTITY(1,1),
	ID_Role INT NOT NULL,
	Email VARCHAR(40) NOT NULL,
    Password VARCHAR(20) NOT NULL,
	Image VARBINARY(MAX),
    First_name VARCHAR(20) NOT NULL,
    Last_name VARCHAR(20) NOT NULL,
    Patronymic VARCHAR(20) NOT NULL,
    Passport_details VARCHAR(20) NOT NULL,
    Date_birth DATE NOT NULL,
    Date_employment DATE NOT NULL,
    FOREIGN KEY (ID_Role) REFERENCES Role(ID_Role)
);

CREATE TABLE Service (
    ID_Service INT PRIMARY KEY IDENTITY(1,1),
    Item_Name VARCHAR(20) NOT NULL,
    Item_Description VARCHAR(50) NOT NULL,
    Price NVARCHAR(10) NOT NULL,
    Image VARBINARY(MAX) NOT NULL
);

CREATE TABLE Customer_orders (
    ID_Customers_orders INT PRIMARY KEY IDENTITY(1,1),
    ID_Service INT,
    ID_Users INT,
    Order_Date DATE,
	Status VARCHAR(20),
    FOREIGN KEY (ID_Service) REFERENCES Service(ID_Service),
    FOREIGN KEY (ID_Users) REFERENCES Users(ID_Users)
);

CREATE TABLE Foremen (
    ID_Foreman INT PRIMARY KEY IDENTITY(1,1),
    First_Name VARCHAR(50),
    Last_Name VARCHAR(50),
    Patronymic VARCHAR(50),
    Number_of_Workers INT,
	Number_phone VARCHAR(20),
);

CREATE TABLE Processed_customer_orders (
    ID_Processed_customer_orders INT PRIMARY KEY IDENTITY(1,1),
    ID_Customer_orders INT,
    ID_Staff INT,
    ID_Foreman INT, 
    Status VARCHAR(20),
    Date_Start DATE,
    Date_Ending DATE,
    Final_sum NVARCHAR(20),
    FOREIGN KEY (ID_Customer_orders) REFERENCES Customer_orders(ID_Customers_orders),
    FOREIGN KEY (ID_Staff) REFERENCES Staff(ID_Staff),
    FOREIGN KEY (ID_Foreman) REFERENCES Foremen(ID_Foreman)
);

CREATE TABLE Requests (
    ID_Request INT PRIMARY KEY IDENTITY(1,1),
    ID_Service INT,
    Request_Date DATE DEFAULT GETDATE(),
    First_name VARCHAR(20),
    Last_name VARCHAR(20),
    Email VARCHAR(40),
    Additional_Info VARCHAR(200),
    Status VARCHAR(20) DEFAULT 'Отправлен',
    FOREIGN KEY (ID_Service) REFERENCES Service(ID_Service)
);

INSERT INTO Role (roll_name, salary)
VALUES
('Admin', '50000'),
('Manager', '30000');

INSERT INTO Staff (ID_Role, Email, Password, Image, First_name, Last_name, Patronymic, Passport_details, Date_birth, Date_employment)
VALUES
(1, '12', '12', NULL, 'Admin', 'User', 'Adminov', '1234 567890', '1980-01-01', '2022-01-01'),
(2, '13', '13', NULL, 'Alex', 'Petrov', 'Semenov', '2345 678901', '1990-05-12', '2023-06-15');

INSERT INTO Foremen (First_Name, Last_Name, Patronymic, Number_of_Workers, Number_phone)
VALUES
('Ivan', 'Ivanov', 'Petrovich', 5, '89037765432'),
('Sergey', 'Sergeev', 'Nikolaevich', 8, '89038876543');

