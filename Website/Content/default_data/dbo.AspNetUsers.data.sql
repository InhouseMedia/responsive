DELETE FROM [dbo].[AspNetUsers]
GO
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ecec2056-053c-438e-8307-f2b8d73ec2dc', N'ramonklanke@hotmail.com', 0, N'AIppPFXopbPJLZ7194v7SX8VbNRL136+8vRkIsIRG+V11H7qXx4QYM+nP7UsAaoz3Q==', N'030bcd6d-1511-40f7-8396-ebda7bca506b', NULL, 0, 0, NULL, 1, 0, N'ramonklanke@hotmail.com')
GO