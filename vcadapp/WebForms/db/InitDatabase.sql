-- Criação do banco de dados
IF DB_ID('VcadAppDB') IS NULL
    CREATE DATABASE VcadAppDB;
GO

USE VcadAppDB;
GO

-- Tabela de cadastro de pessoas
IF OBJECT_ID('dbo.Person', 'U') IS NOT NULL
    DROP TABLE dbo.Person;
GO

CREATE TABLE dbo.Person
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    BirthDate DATE NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    MaritalStatus NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Procedure: Inserir Pessoa
IF OBJECT_ID('dbo.usp_InsertPerson', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_InsertPerson;
GO

CREATE PROCEDURE dbo.usp_InsertPerson
    @Name NVARCHAR(200),
    @BirthDate DATE,
    @Email NVARCHAR(200),
    @MaritalStatus NVARCHAR(50),
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Person (Name, BirthDate, Email, MaritalStatus)
    VALUES (@Name, @BirthDate, @Email, @MaritalStatus);

    SET @NewId = SCOPE_IDENTITY();
END;
GO

-- Procedure: Obter todas as pessoas
IF OBJECT_ID('dbo.usp_GetPersons', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetPersons;
GO

CREATE PROCEDURE dbo.usp_GetPersons
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id,
           Name,
           BirthDate,
           Email,
           MaritalStatus,
           CreatedAt
    FROM dbo.Person
    ORDER BY CreatedAt DESC;
END;
GO

-- Procedure: Atualizar pessoa
IF OBJECT_ID('dbo.usp_UpdatePerson', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_UpdatePerson;
GO

CREATE PROCEDURE dbo.usp_UpdatePerson
    @Id INT,
    @Name NVARCHAR(200),
    @BirthDate DATE,
    @Email NVARCHAR(200),
    @MaritalStatus NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Person
    SET Name = @Name,
        BirthDate = @BirthDate,
        Email = @Email,
        MaritalStatus = @MaritalStatus
    WHERE Id = @Id;
END;
GO

-- Procedure: Excluir pessoa
IF OBJECT_ID('dbo.usp_DeletePerson', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_DeletePerson;
GO

CREATE PROCEDURE dbo.usp_DeletePerson
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Person WHERE Id = @Id;
END;
GO