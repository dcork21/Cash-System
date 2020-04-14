USE [master]
GO
  CREATE DATABASE CashSystem;

GO
  USE [CashSystem] CREATE TABLE [Identity] (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    FirstName VARCHAR (32),
    LastName VARCHAR (32),
    PostAddress VARCHAR (32),
    PostCode VARCHAR (8),
    Mobile VARCHAR (32),
    Email VARCHAR (32)
  );

CREATE TABLE [User] (
  UserId INT PRIMARY KEY IDENTITY (1, 1),
  IdentityId INT,
  UserName VARCHAR (32),
  PasswordHash VARCHAR (64),
  AccountId INT
);

CREATE TABLE [Account] (
  AccountId INT PRIMARY KEY IDENTITY (1, 1),
  UserId INT,
  BankId INT,
  AccountNumber VARCHAR (16),
  Balance FLOAT
);

CREATE TABLE [Withdrawal] (
  WithdrawlId INT PRIMARY KEY IDENTITY (1, 1),
  AccountId INT,
  Amount FLOAT,
  Expires DATETIME
);

CREATE TABLE [Bank] (
  BankId INT PRIMARY KEY IDENTITY (1, 1),
  SortCode VARCHAR (8),
  Latitude FLOAT,
  Longitude FLOAT
);

CREATE TABLE [Atm] (
  AtmId INT PRIMARY KEY IDENTITY (1, 1),
  BankId INT,
  Latitude FLOAT,
  Longitude FLOAT
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
  CONSTRAINT [FK_Atm_Bank] FOREIGN KEY (AtmId) REFERENCES [Bank] (BankId) ON DELETE NO ACTION ON UPDATE NO ACTION;