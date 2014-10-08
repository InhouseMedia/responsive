SET IDENTITY_INSERT [dbo].[Article_Content] ON
INSERT INTO [dbo].[Article_Content] ([Id], [Article_Id], [Title], [Text], [Controller], [Action], [Active], [Created_By], [Creation_Date]) VALUES (1, 1, N'', N'', N'File', N'File', 1, 1, N'2014-01-01 00:00:00')
INSERT INTO [dbo].[Article_Content] ([Id], [Article_Id], [Title], [Text], [Controller], [Action], [Active], [Created_By], [Creation_Date]) VALUES (2, 10, N'Oeps!', N'<p>Deze pagina is helaas niet meer beschikbaar</p>', N'Error', N'404', 1, 1, N'2014-01-01 00:00:00')
INSERT INTO [dbo].[Article_Content] ([Id], [Article_Id], [Title], [Text], [Controller], [Action], [Active], [Created_By], [Creation_Date]) VALUES (3, 100, N'Uw specialist in kunststof kozijnen', N'
<p>
	Comfortabel wonen in een onderhoudsvriendelijk huis, wie wilt dat nou niet?
</p>
<p>
	Wanneer u kunststof kozijnen aanschaft, zijn deze voor het leven. Schilderen behoort tot het verleden en de kozijnen zijn geluidsisolerend en houden warmte binnen. Wilt u liever kozijnen met meer uitstraling? Ook dat kan. Tegenwoordig zijn kunststof kozijnen niet meer te onderscheiden van houten kozijnen. 
</p>
<p>
	Niet alleen kunststof kozijnen zorgen voor een optimaal wooncomfort. Wat dacht u van rolluiken of screens, hordeuren of schuifpuien? Hieronder ziet u wat wij onder andere kunnen leveren.
</p>
<ul>
	<li>Kozijnen</li>
	<li>Rolluiken</li>
	<li>Screens</li>
	<li>Deuren en ramen</li>
	<li>Horren</li>
	<li>Schuifpuien</li>
	<li>En meer...</li>
</ul>
<h2>Natuurlijk ook bij ons 6% Btw over het arbeidsloon.</h2>
<p> 
	{ geldig tot 1 maart 2015.}
</p>
<h3>Let op onze zomeractie 2014 is van start gegaan.</h3>
<p>
	<b>Actie:</b> Gratis binnen afwerking bij elk kunststof kozijn die gemonteerd wordt.<br>
	Geldig voor de maanden Juli - Augustus - September.
</p>
', N'Article', N'Text', 1, 1, N'2014-10-06 21:30:00')
SET IDENTITY_INSERT [dbo].[Article_Content] OFF
