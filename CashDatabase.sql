USE [master]
GO
DROP DATABASE CashSystem
GO
  CREATE DATABASE CashSystem;
GO
  USE [CashSystem] 



CREATE TABLE [Account] (
  AccountId INT PRIMARY KEY IDENTITY (1, 1),
  UserId INT,
  BankId INT,
  SortCode VARCHAR (8),
  AccountNumber VARCHAR (16),
  Balance FLOAT
);

CREATE TABLE [Atm] (
  AtmId INT PRIMARY KEY IDENTITY (1, 1),
  BankId INT,
  Latitude FLOAT,
  Longitude FLOAT
);
CREATE TABLE [Bank] (
  BankId INT PRIMARY KEY IDENTITY (1, 1),
  BankName VARCHAR (16),
  SortCode VARCHAR (8),
  Latitude FLOAT,
  Longitude FLOAT
);

CREATE TABLE [Identity] (
    Id INT PRIMARY KEY IDENTITY (1, 1),
	UserName VARCHAR (32),
	PasswordHash VARCHAR (128),
    FirstName VARCHAR (32),
    LastName VARCHAR (32),
    PostAddress VARCHAR (128),
    PostCode VARCHAR (8),
    Mobile VARCHAR (16),
    Email VARCHAR (32)
  );

  CREATE TABLE [User] (
  UserId INT PRIMARY KEY IDENTITY (1, 1),
  IdentityId INT,
  SessionToken VARCHAR (32),
  SessionExpiry DATETIME2
);

CREATE TABLE [Withdrawal] (
  WithdrawalId INT PRIMARY KEY IDENTITY (1, 1),
  AccountId INT,
  VerificationHash VARCHAR (128),
  Amount FLOAT,
  ExpiryDate DATETIME2
);


ALTER TABLE
  [User]
ADD
  CONSTRAINT [FK_User_Identity] FOREIGN KEY (IdentityId) REFERENCES [Identity] (Id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE
  [Account]
ADD
  CONSTRAINT [FK_Account_User] FOREIGN KEY (UserId) REFERENCES [User] (UserId) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE
  [Account]
ADD
  CONSTRAINT [FK_Account_Bank] FOREIGN KEY (BankId) REFERENCES [Bank] (BankId) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE
  [Withdrawal]
ADD
  CONSTRAINT [FK_Withdrawal_Account] FOREIGN KEY (AccountId) REFERENCES [Account] (AccountId) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE
  [Atm]
ADD
  CONSTRAINT [FK_Atm_Bank] FOREIGN KEY (BankId) REFERENCES [Bank] (BankId) ON DELETE NO ACTION ON UPDATE NO ACTION;


INSERT INTO Bank(BankName, SortCode, Latitude, Longitude)
VALUES ('Barclays Bank', '20-13-34', 51.4575779,-2.610413);
INSERT INTO Atm(BankId, Latitude, Longitude)
VALUES (1, 51.4575779,-2.610413);
INSERT INTO Atm(BankId, Latitude, Longitude)
VALUES (1, 51.4575779,-2.610413);

INSERT INTO Bank(BankName, SortCode, Latitude, Longitude)
VALUES ('NatWest', '20-13-34', 51.4569763,-2.6005509);
INSERT INTO Atm(BankId, Latitude, Longitude)
VALUES (2, 51.4569763,-2.6005509);
INSERT INTO Atm(BankId, Latitude, Longitude)
VALUES (2, 51.4569763,-2.6005509);

INSERT INTO Bank(BankName, SortCode, Latitude, Longitude)
VALUES ('HSBC', '20-13-34', 51.4572271,-2.6005509);
INSERT INTO Atm(BankId, Latitude, Longitude)
VALUES (3, 51.462752,-2.5958812);
INSERT INTO Atm(BankId, Latitude, Longitude)
VALUES (3, 51.462752,-2.5958812);

