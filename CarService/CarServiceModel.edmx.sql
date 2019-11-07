
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/11/2019 23:09:57
-- Generated from EDMX file: C:\Users\dinosaph\Documents\dotnet\Projects\CarService\CarService\CarServiceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AUTO];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientAuto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Masini] DROP CONSTRAINT [FK_ClientAuto];
GO
IF OBJECT_ID(N'[dbo].[FK_DetaliuComandaMecanic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mecanici] DROP CONSTRAINT [FK_DetaliuComandaMecanic];
GO
IF OBJECT_ID(N'[dbo].[FK_DetaliuComandaMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Materiale] DROP CONSTRAINT [FK_DetaliuComandaMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_ImagineDetaliuComanda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Imagini] DROP CONSTRAINT [FK_ImagineDetaliuComanda];
GO
IF OBJECT_ID(N'[dbo].[FK_AutoComanda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comenzi] DROP CONSTRAINT [FK_AutoComanda];
GO
IF OBJECT_ID(N'[dbo].[FK_ComandaDetaliuComanda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetaliiComenzi] DROP CONSTRAINT [FK_ComandaDetaliuComanda];
GO
IF OBJECT_ID(N'[dbo].[FK_AutoSasiu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Masini] DROP CONSTRAINT [FK_AutoSasiu];
GO
IF OBJECT_ID(N'[dbo].[FK_DetaliuComandaOperatie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Operatii] DROP CONSTRAINT [FK_DetaliuComandaOperatie];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Masini]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Masini];
GO
IF OBJECT_ID(N'[dbo].[Sasiuri]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sasiuri];
GO
IF OBJECT_ID(N'[dbo].[Mecanici]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mecanici];
GO
IF OBJECT_ID(N'[dbo].[Materiale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Materiale];
GO
IF OBJECT_ID(N'[dbo].[Imagini]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Imagini];
GO
IF OBJECT_ID(N'[dbo].[Operatii]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Operatii];
GO
IF OBJECT_ID(N'[dbo].[Comenzi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comenzi];
GO
IF OBJECT_ID(N'[dbo].[DetaliiComenzi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetaliiComenzi];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nume] nvarchar(15)  NOT NULL,
    [Prenume] nvarchar(15)  NOT NULL,
    [Adresa] nvarchar(50)  NOT NULL,
    [Localitate] nvarchar(10)  NOT NULL,
    [Judet] nvarchar(10)  NOT NULL,
    [Telefon] int  NOT NULL,
    [Email] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Masini'
CREATE TABLE [dbo].[Masini] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NumarAuto] nvarchar(10)  NOT NULL,
    [SerieSasiu] nvarchar(25)  NOT NULL,
    [Client_Id] int  NOT NULL,
    [Sasiu_Id] int  NOT NULL
);
GO

-- Creating table 'Sasiuri'
CREATE TABLE [dbo].[Sasiuri] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodSasiu] nvarchar(2)  NOT NULL,
    [Denumire] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'Mecanici'
CREATE TABLE [dbo].[Mecanici] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nume] nvarchar(15)  NOT NULL,
    [Prenume] nvarchar(15)  NOT NULL,
    [DetaliuComandaId] int  NOT NULL
);
GO

-- Creating table 'Materiale'
CREATE TABLE [dbo].[Materiale] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Denumire] nvarchar(50)  NOT NULL,
    [Cantitate] decimal(10,2)  NOT NULL,
    [Pret] decimal(10,2)  NOT NULL,
    [DataAprovizionare] datetime  NOT NULL,
    [DetaliuComandaId] int  NOT NULL
);
GO

-- Creating table 'Imagini'
CREATE TABLE [dbo].[Imagini] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titlu] nvarchar(15)  NOT NULL,
    [Descriere] nvarchar(256)  NOT NULL,
    [Data] datetime  NOT NULL,
    [Foto] varbinary(max)  NOT NULL,
    [DetaliuComanda_Id] int  NOT NULL
);
GO

-- Creating table 'Operatii'
CREATE TABLE [dbo].[Operatii] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Denumire] nvarchar(256)  NOT NULL,
    [TimpExecutie] decimal(6,2)  NOT NULL,
    [DetaliuComanda_Id] int  NOT NULL
);
GO

-- Creating table 'Comenzi'
CREATE TABLE [dbo].[Comenzi] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StareComanda] nvarchar(max)  NOT NULL,
    [DataSystem] datetime  NOT NULL,
    [DataProgramare] datetime  NOT NULL,
    [DataFinalizare] datetime  NOT NULL,
    [KmBord] int  NOT NULL,
    [Descriere] nvarchar(1024)  NOT NULL,
    [ValoarePiese] decimal(10,2)  NOT NULL,
    [ClientId] int  NOT NULL,
    [Auto_Id] int  NOT NULL
);
GO

-- Creating table 'DetaliiComenzi'
CREATE TABLE [dbo].[DetaliiComenzi] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ComandaId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Masini'
ALTER TABLE [dbo].[Masini]
ADD CONSTRAINT [PK_Masini]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sasiuri'
ALTER TABLE [dbo].[Sasiuri]
ADD CONSTRAINT [PK_Sasiuri]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mecanici'
ALTER TABLE [dbo].[Mecanici]
ADD CONSTRAINT [PK_Mecanici]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Materiale'
ALTER TABLE [dbo].[Materiale]
ADD CONSTRAINT [PK_Materiale]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Imagini'
ALTER TABLE [dbo].[Imagini]
ADD CONSTRAINT [PK_Imagini]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Operatii'
ALTER TABLE [dbo].[Operatii]
ADD CONSTRAINT [PK_Operatii]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comenzi'
ALTER TABLE [dbo].[Comenzi]
ADD CONSTRAINT [PK_Comenzi]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetaliiComenzi'
ALTER TABLE [dbo].[DetaliiComenzi]
ADD CONSTRAINT [PK_DetaliiComenzi]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Client_Id] in table 'Masini'
ALTER TABLE [dbo].[Masini]
ADD CONSTRAINT [FK_ClientAuto]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientAuto'
CREATE INDEX [IX_FK_ClientAuto]
ON [dbo].[Masini]
    ([Client_Id]);
GO

-- Creating foreign key on [DetaliuComandaId] in table 'Mecanici'
ALTER TABLE [dbo].[Mecanici]
ADD CONSTRAINT [FK_DetaliuComandaMecanic]
    FOREIGN KEY ([DetaliuComandaId])
    REFERENCES [dbo].[DetaliiComenzi]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetaliuComandaMecanic'
CREATE INDEX [IX_FK_DetaliuComandaMecanic]
ON [dbo].[Mecanici]
    ([DetaliuComandaId]);
GO

-- Creating foreign key on [DetaliuComandaId] in table 'Materiale'
ALTER TABLE [dbo].[Materiale]
ADD CONSTRAINT [FK_DetaliuComandaMaterial]
    FOREIGN KEY ([DetaliuComandaId])
    REFERENCES [dbo].[DetaliiComenzi]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetaliuComandaMaterial'
CREATE INDEX [IX_FK_DetaliuComandaMaterial]
ON [dbo].[Materiale]
    ([DetaliuComandaId]);
GO

-- Creating foreign key on [DetaliuComanda_Id] in table 'Imagini'
ALTER TABLE [dbo].[Imagini]
ADD CONSTRAINT [FK_ImagineDetaliuComanda]
    FOREIGN KEY ([DetaliuComanda_Id])
    REFERENCES [dbo].[DetaliiComenzi]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImagineDetaliuComanda'
CREATE INDEX [IX_FK_ImagineDetaliuComanda]
ON [dbo].[Imagini]
    ([DetaliuComanda_Id]);
GO

-- Creating foreign key on [Auto_Id] in table 'Comenzi'
ALTER TABLE [dbo].[Comenzi]
ADD CONSTRAINT [FK_AutoComanda]
    FOREIGN KEY ([Auto_Id])
    REFERENCES [dbo].[Masini]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutoComanda'
CREATE INDEX [IX_FK_AutoComanda]
ON [dbo].[Comenzi]
    ([Auto_Id]);
GO

-- Creating foreign key on [ComandaId] in table 'DetaliiComenzi'
ALTER TABLE [dbo].[DetaliiComenzi]
ADD CONSTRAINT [FK_ComandaDetaliuComanda]
    FOREIGN KEY ([ComandaId])
    REFERENCES [dbo].[Comenzi]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComandaDetaliuComanda'
CREATE INDEX [IX_FK_ComandaDetaliuComanda]
ON [dbo].[DetaliiComenzi]
    ([ComandaId]);
GO

-- Creating foreign key on [Sasiu_Id] in table 'Masini'
ALTER TABLE [dbo].[Masini]
ADD CONSTRAINT [FK_AutoSasiu]
    FOREIGN KEY ([Sasiu_Id])
    REFERENCES [dbo].[Sasiuri]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutoSasiu'
CREATE INDEX [IX_FK_AutoSasiu]
ON [dbo].[Masini]
    ([Sasiu_Id]);
GO

-- Creating foreign key on [DetaliuComanda_Id] in table 'Operatii'
ALTER TABLE [dbo].[Operatii]
ADD CONSTRAINT [FK_DetaliuComandaOperatie]
    FOREIGN KEY ([DetaliuComanda_Id])
    REFERENCES [dbo].[DetaliiComenzi]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetaliuComandaOperatie'
CREATE INDEX [IX_FK_DetaliuComandaOperatie]
ON [dbo].[Operatii]
    ([DetaliuComanda_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------