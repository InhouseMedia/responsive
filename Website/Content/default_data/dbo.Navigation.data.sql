SET IDENTITY_INSERT [dbo].[Navigation] ON
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (1, 100, NULL, N'1', 1, 1, 1, N'2014-09-25 17:11:45')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (2, 101, 1, N'1', 0.8, 1, 1, N'2014-09-25 17:12:11')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (3, 102, 1, N'2', 0.8, 1, 1, N'2014-09-25 17:14:49')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (4, 103, 1, N'3', 0.8, 1, 1, N'2014-09-25 17:17:44')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (5, 104, 1, N'4', 0.8, 1, 1, N'2014-09-25 17:18:29')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (6, 105, 1, N'5', 1, 1, 1, N'2014-09-25 17:19:03')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (7, 106, 3, N'1', 0.5, 1, 1, N'2014-10-04 15:46:00')
INSERT INTO [dbo].[Navigation] ([Navigation_Id], [Article_Id], [Parent_Id], [Level], [Priority], [Active], [Created_By], [Creation_Date]) VALUES (8, 107, 3, N'2', 0.5, 1, 1, N'2014-10-04 15:47:00')
SET IDENTITY_INSERT [dbo].[Navigation] OFF
