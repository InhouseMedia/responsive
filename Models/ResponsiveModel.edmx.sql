
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/08/2014 12:34:13
-- Generated from EDMX file: C:\Users\rklank65\Documents\Solutions\Responsive\Models\ResponsiveModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-Responsive-20140904142246];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Article_Content]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_Content] DROP CONSTRAINT [FK_Article_Content];
GO
IF OBJECT_ID(N'[dbo].[FK_Navigation_ChangeLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_ChangeLogs] DROP CONSTRAINT [FK_Navigation_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Navigation_PublishLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_PublishLogs] DROP CONSTRAINT [FK_Navigation_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_ChangeLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_ChangeLogs] DROP CONSTRAINT [FK_Article_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_PublishLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_PublishLogs] DROP CONSTRAINT [FK_Article_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_Metadata]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_Metadata] DROP CONSTRAINT [FK_Article_Metadata];
GO
IF OBJECT_ID(N'[dbo].[FK_NavigationNavigation_Content]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_Content] DROP CONSTRAINT [FK_NavigationNavigation_Content];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Article]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article];
GO
IF OBJECT_ID(N'[dbo].[Article_Content]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_Content];
GO
IF OBJECT_ID(N'[dbo].[Navigation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation];
GO
IF OBJECT_ID(N'[dbo].[Navigation_ChangeLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[Navigation_PublishLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[Article_ChangeLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[Article_PublishLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[Article_Metadata]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_Metadata];
GO
IF OBJECT_ID(N'[dbo].[Navigation_Content]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation_Content];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Article'
CREATE TABLE [dbo].[Article] (
    [Article_Id] int IDENTITY(1,1) NOT NULL  ,
    [Active] tinyint  NOT NULL  ,
    [Created_By] int  NOT NULL  ,
    [Creation_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Article_Content'
CREATE TABLE [dbo].[Article_Content] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Title] varchar(250)  NULL  ,
    [Text] varchar(max)  NULL  ,
    [Controller] nvarchar(max)  NULL  ,
    [Action] nvarchar(max)  NULL  ,
    [Active] tinyint  NOT NULL  ,
    [Created_By] int  NOT NULL  ,
    [Creation_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Navigation'
CREATE TABLE [dbo].[Navigation] (
    [Navigation_Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NOT NULL  ,
    [Parent_Id] int  NULL  ,
    [Level] int  NOT NULL  ,
    [Priority] float  NOT NULL DEFAULT 0.5 ,
    [Active] tinyint  NOT NULL  ,
    [Created_By] int  NOT NULL  ,
    [Creation_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Navigation_ChangeLogs'
CREATE TABLE [dbo].[Navigation_ChangeLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Navigation_Id] int  NULL  ,
    [Changed_By] int  NOT NULL  ,
    [Changed_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Navigation_PublishLogs'
CREATE TABLE [dbo].[Navigation_PublishLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Navigation_Id] int  NULL  ,
    [Published_By] int  NOT NULL  ,
    [Published_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Article_ChangeLogs'
CREATE TABLE [dbo].[Article_ChangeLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Changed_By] int  NOT NULL  ,
    [Changed_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Article_PublishLogs'
CREATE TABLE [dbo].[Article_PublishLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Published_By] int  NOT NULL  ,
    [Published_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Article_Metadata'
CREATE TABLE [dbo].[Article_Metadata] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Meta_Title] varchar(250)  NULL  ,
    [Meta_Keywords] varchar(250)  NULL  ,
    [Meta_Description] varchar(max)  NULL  
);
GO

-- Creating table 'Navigation_Content'
CREATE TABLE [dbo].[Navigation_Content] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Navigation_Id] int  NULL  ,
    [Title] nvarchar(max)  NOT NULL  ,
    [Url] nvarchar(max)  NULL  ,
    [On_Click] nvarchar(max)  NOT NULL  
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Article_Id] in table 'Article'
ALTER TABLE [dbo].[Article]
ADD CONSTRAINT [PK_Article]
    PRIMARY KEY CLUSTERED ([Article_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_Content'
ALTER TABLE [dbo].[Article_Content]
ADD CONSTRAINT [PK_Article_Content]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Navigation_Id] in table 'Navigation'
ALTER TABLE [dbo].[Navigation]
ADD CONSTRAINT [PK_Navigation]
    PRIMARY KEY CLUSTERED ([Navigation_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Navigation_ChangeLogs'
ALTER TABLE [dbo].[Navigation_ChangeLogs]
ADD CONSTRAINT [PK_Navigation_ChangeLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Navigation_PublishLogs'
ALTER TABLE [dbo].[Navigation_PublishLogs]
ADD CONSTRAINT [PK_Navigation_PublishLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_ChangeLogs'
ALTER TABLE [dbo].[Article_ChangeLogs]
ADD CONSTRAINT [PK_Article_ChangeLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_PublishLogs'
ALTER TABLE [dbo].[Article_PublishLogs]
ADD CONSTRAINT [PK_Article_PublishLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_Metadata'
ALTER TABLE [dbo].[Article_Metadata]
ADD CONSTRAINT [PK_Article_Metadata]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Navigation_Content'
ALTER TABLE [dbo].[Navigation_Content]
ADD CONSTRAINT [PK_Navigation_Content]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Article_Id] in table 'Article_Content'
ALTER TABLE [dbo].[Article_Content]
ADD CONSTRAINT [FK_Article_Content]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[Article]
        ([Article_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_Content'
CREATE INDEX [IX_FK_Article_Content]
ON [dbo].[Article_Content]
    ([Article_Id]);
GO

-- Creating foreign key on [Navigation_Id] in table 'Navigation_ChangeLogs'
ALTER TABLE [dbo].[Navigation_ChangeLogs]
ADD CONSTRAINT [FK_Navigation_ChangeLogs]
    FOREIGN KEY ([Navigation_Id])
    REFERENCES [dbo].[Navigation]
        ([Navigation_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Navigation_ChangeLogs'
CREATE INDEX [IX_FK_Navigation_ChangeLogs]
ON [dbo].[Navigation_ChangeLogs]
    ([Navigation_Id]);
GO

-- Creating foreign key on [Navigation_Id] in table 'Navigation_PublishLogs'
ALTER TABLE [dbo].[Navigation_PublishLogs]
ADD CONSTRAINT [FK_Navigation_PublishLogs]
    FOREIGN KEY ([Navigation_Id])
    REFERENCES [dbo].[Navigation]
        ([Navigation_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Navigation_PublishLogs'
CREATE INDEX [IX_FK_Navigation_PublishLogs]
ON [dbo].[Navigation_PublishLogs]
    ([Navigation_Id]);
GO

-- Creating foreign key on [Article_Id] in table 'Article_ChangeLogs'
ALTER TABLE [dbo].[Article_ChangeLogs]
ADD CONSTRAINT [FK_Article_ChangeLogs]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[Article]
        ([Article_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_ChangeLogs'
CREATE INDEX [IX_FK_Article_ChangeLogs]
ON [dbo].[Article_ChangeLogs]
    ([Article_Id]);
GO

-- Creating foreign key on [Article_Id] in table 'Article_PublishLogs'
ALTER TABLE [dbo].[Article_PublishLogs]
ADD CONSTRAINT [FK_Article_PublishLogs]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[Article]
        ([Article_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_PublishLogs'
CREATE INDEX [IX_FK_Article_PublishLogs]
ON [dbo].[Article_PublishLogs]
    ([Article_Id]);
GO

-- Creating foreign key on [Article_Id] in table 'Article_Metadata'
ALTER TABLE [dbo].[Article_Metadata]
ADD CONSTRAINT [FK_Article_Metadata]
    FOREIGN KEY ([Article_Id])
    REFERENCES [dbo].[Article]
        ([Article_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_Metadata'
CREATE INDEX [IX_FK_Article_Metadata]
ON [dbo].[Article_Metadata]
    ([Article_Id]);
GO

-- Creating foreign key on [Navigation_Id] in table 'Navigation_Content'
ALTER TABLE [dbo].[Navigation_Content]
ADD CONSTRAINT [FK_NavigationNavigation_Content]
    FOREIGN KEY ([Navigation_Id])
    REFERENCES [dbo].[Navigation]
        ([Navigation_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NavigationNavigation_Content'
CREATE INDEX [IX_FK_NavigationNavigation_Content]
ON [dbo].[Navigation_Content]
    ([Navigation_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------