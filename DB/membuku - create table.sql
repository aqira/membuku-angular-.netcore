CREATE DATABASE Membuku;
GO
USE Membuku;
GO

CREATE TABLE Users(
    Username VARCHAR(50) PRIMARY KEY,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender VARCHAR(1) CHECK(Gender IN ('F', 'M')),
    JoinDate DATE NOT NULL DEFAULT GETDATE(),
    LastActiveDate DATETIME
);

CREATE TABLE Authors(
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Books(
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL, 
    Cover VARCHAR(255),
    ReleaseDate DATE NOT NULL,
    AuthorId INT REFERENCES Authors(Id)
);

CREATE TABLE Reviews(
    Username VARCHAR(50) REFERENCES Users(Username) NOT NULL,
    BookId INT REFERENCES Books(Id) NOT NULL,
    AddedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ReadStatus VARCHAR(20) NOT NULL CHECK(ReadStatus IN ('READ', 'CURRENTLY-READING', 'TO-READ')),
    Description VARCHAR(MAX),
    ReadDate DATE,
    Rating INT CHECK(Rating BETWEEN 1 AND 5),
    ReviewDate DATETIME ,
    PRIMARY KEY(Username, BookId),
);

CREATE TABLE HighlightedBooks(
    BookId INT PRIMARY KEY REFERENCES Books(Id),
    OrderNumber INT NOT NULL,
    AddedDate DATETIME NOT NULL DEFAULT GETDATE()
);
