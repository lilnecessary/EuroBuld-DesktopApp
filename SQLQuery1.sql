CREATE DATABASE Stroy1

USE Stroy1


CREATE TABLE Должности (
    ДолжностьID INT PRIMARY KEY IDENTITY,
    Название NVARCHAR(100) NOT NULL,
    Оклад DECIMAL(18, 2) NOT NULL,
    Описание NVARCHAR(MAX),
    ТребуемыйОпыт INT,
    Обязанности NVARCHAR(MAX)
);


CREATE TABLE РолиСотрудников (
    РольID INT PRIMARY KEY IDENTITY,
    Название NVARCHAR(50) NOT NULL,
    ДолжностьID INT NOT NULL,
    FOREIGN KEY (ДолжностьID) REFERENCES Должности(ДолжностьID)
);


CREATE TABLE Сотрудники (
    СотрудникID INT PRIMARY KEY IDENTITY,
    Имя NVARCHAR(100) NOT NULL,
	Фамилия NVARCHAR(100) NOT NULL,
	Отчество NVARCHAR(100) NOT NULL,
    Телефон NVARCHAR(20),
    Email NVARCHAR(100) UNIQUE,
    ДолжностьID INT NOT NULL,
    FOREIGN KEY (ДолжностьID) REFERENCES Должности(ДолжностьID)
);


CREATE TABLE Клиенты (
    КлиентID INT PRIMARY KEY IDENTITY,
    Имя NVARCHAR(100) NOT NULL,
	Фамилия NVARCHAR(100) NOT NULL,
	Отчество NVARCHAR(100) NOT NULL,
    Телефон NVARCHAR(20),
    Email NVARCHAR(100) UNIQUE, 
    Адрес NVARCHAR(255)
);


CREATE TABLE Пользователи (
    ПользовательID INT PRIMARY KEY IDENTITY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Пароль NVARCHAR(255) NOT NULL,
    КлиентID INT NULL,
    СотрудникID INT NULL,
    CHECK ((КлиентID IS NOT NULL AND СотрудникID IS NULL) OR (КлиентID IS NULL AND СотрудникID IS NOT NULL)),
    FOREIGN KEY (КлиентID) REFERENCES Клиенты(КлиентID),
    FOREIGN KEY (СотрудникID) REFERENCES Сотрудники(СотрудникID)
);


CREATE TABLE Проекты (
    ПроектID INT PRIMARY KEY IDENTITY,
    КлиентID INT NOT NULL,
    Название NVARCHAR(255) NOT NULL,
    Описание NVARCHAR(MAX),
    ДатаНачала DATE NOT NULL,
    ДатаОкончания DATE,
    Статус NVARCHAR(50),
    FOREIGN KEY (КлиентID) REFERENCES Клиенты(КлиентID)
);


CREATE TABLE Услуги (
    УслугаID INT PRIMARY KEY IDENTITY,
    Название NVARCHAR(255) NOT NULL,
    Описание NVARCHAR(MAX)
);


CREATE TABLE ПроектныеУслуги (
    ПроектУслугаID INT PRIMARY KEY IDENTITY,
    ПроектID INT NOT NULL,
    УслугаID INT NOT NULL,
    Стоимость DECIMAL(18, 2),
    Количество INT,
    FOREIGN KEY (ПроектID) REFERENCES Проекты(ПроектID),
    FOREIGN KEY (УслугаID) REFERENCES Услуги(УслугаID)
);


CREATE TABLE НазначенияСотрудников (
    НазначениеID INT PRIMARY KEY IDENTITY,
    ПроектID INT NOT NULL,
    СотрудникID INT NOT NULL,
    РольID INT NOT NULL,
    ДатаНазначения DATE NOT NULL,
    FOREIGN KEY (ПроектID) REFERENCES Проекты(ПроектID),
    FOREIGN KEY (СотрудникID) REFERENCES Сотрудники(СотрудникID),
    FOREIGN KEY (РольID) REFERENCES РолиСотрудников(РольID)
);

ALTER AUTHORIZATION ON DATABASE :: Stroy1 to sa;
