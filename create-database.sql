CREATE DATABASE WakeCommerce
GO

USE WakeCommerce
GO

CREATE TABLE dbo.Products(
	Id uniqueidentifier NOT NULL,
	Name varchar(255) NOT NULL,
	Stock INT NOT NULL,
	Price DECIMAL(10,2) NOT NULL,
    CreatedAt DATETIME NOT NULL,
	CONSTRAINT PK_Products PRIMARY KEY (Id)
)


INSERT INTO dbo.Products (Id, Name, Stock, Price, CreatedAt) VALUES
    (NEWID(), 'Playstation 5', 10, 3499.99, GETDATE()),
    (NEWID(), 'Elden Ring', 20, 179.99, GETDATE()),
    (NEWID(), 'Baldurs Gate 3', 15, 179.99, GETDATE()),
    (NEWID(), 'Bloodborne', 30, 99.90, GETDATE()),
    (NEWID(), 'Dark Souls 3', 25, 189.98, GETDATE());