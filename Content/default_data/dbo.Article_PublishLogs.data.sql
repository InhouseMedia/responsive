SET IDENTITY_INSERT [dbo].[Article_PublishLogs] ON
INSERT INTO [dbo].[Article_PublishLogs] ([Id], [Article_Id], [Published_By], [Published_Date]) VALUES (1, 100, 1, N'2014-10-08 10:10:00')
INSERT INTO [dbo].[Article_PublishLogs] ([Id], [Article_Id], [Published_By], [Published_Date]) VALUES (2, 100, 1, N'2014-10-09 09:00:00')
SET IDENTITY_INSERT [dbo].[Article_PublishLogs] OFF
