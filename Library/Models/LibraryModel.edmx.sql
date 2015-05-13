
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/11/2015 22:28:23
-- Generated from EDMX file: C:\Users\rklank65\Documents\Solutions\Responsive\Library\Models\LibraryModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Library];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Article_ChangeLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_ChangeLogs] DROP CONSTRAINT [FK_Article_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_Content]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_Content] DROP CONSTRAINT [FK_Article_Content];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_Metadata]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_Metadata] DROP CONSTRAINT [FK_Article_Metadata];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_PublishLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_PublishLogs] DROP CONSTRAINT [FK_Article_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Navigation_ChangeLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_ChangeLogs] DROP CONSTRAINT [FK_Navigation_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Navigation_PublishLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_PublishLogs] DROP CONSTRAINT [FK_Navigation_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_NavigationNavigation_Content]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_Content] DROP CONSTRAINT [FK_NavigationNavigation_Content];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUsersArticle_PublishLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_PublishLogs] DROP CONSTRAINT [FK_AspNetUsersArticle_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_ChangeLogsAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_ChangeLogs] DROP CONSTRAINT [FK_Article_ChangeLogsAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Article_ContentAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article_Content] DROP CONSTRAINT [FK_Article_ContentAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_ArticleAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article] DROP CONSTRAINT [FK_ArticleAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_NavigationAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation] DROP CONSTRAINT [FK_NavigationAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Navigation_ChangeLogsAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_ChangeLogs] DROP CONSTRAINT [FK_Navigation_ChangeLogsAspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Navigation_PublishLogsAspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Navigation_PublishLogs] DROP CONSTRAINT [FK_Navigation_PublishLogsAspNetUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Article]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article];
GO
IF OBJECT_ID(N'[dbo].[Article_ChangeLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[Article_Content]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_Content];
GO
IF OBJECT_ID(N'[dbo].[Article_Metadata]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_Metadata];
GO
IF OBJECT_ID(N'[dbo].[Article_PublishLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Config]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Config];
GO
IF OBJECT_ID(N'[dbo].[Navigation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation];
GO
IF OBJECT_ID(N'[dbo].[Navigation_ChangeLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation_ChangeLogs];
GO
IF OBJECT_ID(N'[dbo].[Navigation_Content]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation_Content];
GO
IF OBJECT_ID(N'[dbo].[Navigation_PublishLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Navigation_PublishLogs];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Article'
CREATE TABLE [dbo].[Article] (
    [Article_Id] int IDENTITY(1,1) NOT NULL  ,
    [Template] nvarchar(max)  NOT NULL  ,
    [Active] tinyint  NOT NULL  ,
    [Created_By] nvarchar(128)  NOT NULL  ,
    [Creation_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Article_ChangeLogs'
CREATE TABLE [dbo].[Article_ChangeLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Changed_By] nvarchar(128)  NOT NULL  ,
    [Changed_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Article_Content'
CREATE TABLE [dbo].[Article_Content] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Title] varchar(250)  NULL  ,
    [Text] varchar(max)  NULL  ,
    [Level] int  NOT NULL  ,
    [Controller] nvarchar(max)  NOT NULL  ,
    [Action] nvarchar(max)  NOT NULL  ,
    [Active] tinyint  NOT NULL  ,
    [Created_By] nvarchar(128)  NOT NULL  ,
    [Creation_Date] datetime  NOT NULL  
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

-- Creating table 'Article_PublishLogs'
CREATE TABLE [dbo].[Article_PublishLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Article_Id] int  NULL  ,
    [Published_By] nvarchar(128)  NOT NULL  ,
    [Published_Date] datetime  NOT NULL  
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL  ,
    [Name] nvarchar(256)  NOT NULL  
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [UserId] nvarchar(128)  NOT NULL  ,
    [ClaimType] nvarchar(max)  NULL  ,
    [ClaimValue] nvarchar(max)  NULL  
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL  ,
    [ProviderKey] nvarchar(128)  NOT NULL  ,
    [UserId] nvarchar(128)  NOT NULL  
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL  ,
    [Email] nvarchar(256)  NULL  ,
    [EmailConfirmed] bit  NOT NULL  ,
    [PasswordHash] nvarchar(max)  NULL  ,
    [SecurityStamp] nvarchar(max)  NULL  ,
    [PhoneNumber] nvarchar(max)  NULL  ,
    [PhoneNumberConfirmed] bit  NOT NULL  ,
    [TwoFactorEnabled] bit  NOT NULL  ,
    [LockoutEndDateUtc] datetime  NULL  ,
    [LockoutEnabled] bit  NOT NULL  ,
    [AccessFailedCount] int  NOT NULL  ,
    [UserName] nvarchar(256)  NOT NULL  
);
GO

-- Creating table 'Config'
CREATE TABLE [dbo].[Config] (
    [Config_Id] int IDENTITY(1,1) NOT NULL  ,
    [Data] nvarchar(max)  NOT NULL  
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
    [Created_By] nvarchar(128)  NOT NULL  ,
    [Creation_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Navigation_ChangeLogs'
CREATE TABLE [dbo].[Navigation_ChangeLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Navigation_Id] int  NULL  ,
    [Changed_By] nvarchar(128)  NOT NULL  ,
    [Changed_Date] datetime  NOT NULL  
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

-- Creating table 'Navigation_PublishLogs'
CREATE TABLE [dbo].[Navigation_PublishLogs] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [Navigation_Id] int  NULL  ,
    [Published_By] nvarchar(128)  NOT NULL  ,
    [Published_Date] datetime  NOT NULL  
);
GO

-- Creating table 'Documents_Settings'
CREATE TABLE [dbo].[Documents_Settings] (
    [Id] int IDENTITY(1,1) NOT NULL  ,
    [stream_id] nvarchar(max)  NOT NULL  ,
    [settings] nvarchar(max)  NOT NULL  
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [RoleId] nvarchar(128)  NOT NULL  ,
    [UserId] nvarchar(128)  NOT NULL  
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

-- Creating primary key on [Id] in table 'Article_ChangeLogs'
ALTER TABLE [dbo].[Article_ChangeLogs]
ADD CONSTRAINT [PK_Article_ChangeLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_Content'
ALTER TABLE [dbo].[Article_Content]
ADD CONSTRAINT [PK_Article_Content]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_Metadata'
ALTER TABLE [dbo].[Article_Metadata]
ADD CONSTRAINT [PK_Article_Metadata]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Article_PublishLogs'
ALTER TABLE [dbo].[Article_PublishLogs]
ADD CONSTRAINT [PK_Article_PublishLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Config_Id] in table 'Config'
ALTER TABLE [dbo].[Config]
ADD CONSTRAINT [PK_Config]
    PRIMARY KEY CLUSTERED ([Config_Id] ASC);
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

-- Creating primary key on [Id] in table 'Navigation_Content'
ALTER TABLE [dbo].[Navigation_Content]
ADD CONSTRAINT [PK_Navigation_Content]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Navigation_PublishLogs'
ALTER TABLE [dbo].[Navigation_PublishLogs]
ADD CONSTRAINT [PK_Navigation_PublishLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents_Settings'
ALTER TABLE [dbo].[Documents_Settings]
ADD CONSTRAINT [PK_Documents_Settings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [RoleId], [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([RoleId], [UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
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

-- Creating foreign key on [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([UserId]);
GO

-- Creating foreign key on [Published_By] in table 'Article_PublishLogs'
ALTER TABLE [dbo].[Article_PublishLogs]
ADD CONSTRAINT [FK_AspNetUsersArticle_PublishLogs]
    FOREIGN KEY ([Published_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersArticle_PublishLogs'
CREATE INDEX [IX_FK_AspNetUsersArticle_PublishLogs]
ON [dbo].[Article_PublishLogs]
    ([Published_By]);
GO

-- Creating foreign key on [Changed_By] in table 'Article_ChangeLogs'
ALTER TABLE [dbo].[Article_ChangeLogs]
ADD CONSTRAINT [FK_Article_ChangeLogsAspNetUsers]
    FOREIGN KEY ([Changed_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_ChangeLogsAspNetUsers'
CREATE INDEX [IX_FK_Article_ChangeLogsAspNetUsers]
ON [dbo].[Article_ChangeLogs]
    ([Changed_By]);
GO

-- Creating foreign key on [Created_By] in table 'Article_Content'
ALTER TABLE [dbo].[Article_Content]
ADD CONSTRAINT [FK_Article_ContentAspNetUsers]
    FOREIGN KEY ([Created_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Article_ContentAspNetUsers'
CREATE INDEX [IX_FK_Article_ContentAspNetUsers]
ON [dbo].[Article_Content]
    ([Created_By]);
GO

-- Creating foreign key on [Created_By] in table 'Article'
ALTER TABLE [dbo].[Article]
ADD CONSTRAINT [FK_ArticleAspNetUsers]
    FOREIGN KEY ([Created_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleAspNetUsers'
CREATE INDEX [IX_FK_ArticleAspNetUsers]
ON [dbo].[Article]
    ([Created_By]);
GO

-- Creating foreign key on [Created_By] in table 'Navigation'
ALTER TABLE [dbo].[Navigation]
ADD CONSTRAINT [FK_NavigationAspNetUsers]
    FOREIGN KEY ([Created_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NavigationAspNetUsers'
CREATE INDEX [IX_FK_NavigationAspNetUsers]
ON [dbo].[Navigation]
    ([Created_By]);
GO

-- Creating foreign key on [Changed_By] in table 'Navigation_ChangeLogs'
ALTER TABLE [dbo].[Navigation_ChangeLogs]
ADD CONSTRAINT [FK_Navigation_ChangeLogsAspNetUsers]
    FOREIGN KEY ([Changed_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Navigation_ChangeLogsAspNetUsers'
CREATE INDEX [IX_FK_Navigation_ChangeLogsAspNetUsers]
ON [dbo].[Navigation_ChangeLogs]
    ([Changed_By]);
GO

-- Creating foreign key on [Published_By] in table 'Navigation_PublishLogs'
ALTER TABLE [dbo].[Navigation_PublishLogs]
ADD CONSTRAINT [FK_Navigation_PublishLogsAspNetUsers]
    FOREIGN KEY ([Published_By])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Navigation_PublishLogsAspNetUsers'
CREATE INDEX [IX_FK_Navigation_PublishLogsAspNetUsers]
ON [dbo].[Navigation_PublishLogs]
    ([Published_By]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------