SET IDENTITY_INSERT [dbo].[Config] ON
INSERT INTO [dbo].[Config] ([Config_Id], [Data]) VALUES (1, N'{"language": {"locale": [ "en-US" ]},"searchEngines": {"googleverification": "TEST", "author":"John van Osch"},"socialMedia": {"socialMediaTags": true}}')
SET IDENTITY_INSERT [dbo].[Config] OFF
