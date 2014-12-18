namespace Library.Setup
{
	//using System;
	using System.Collections.Generic;
	using System.Linq;
	//using System.Web;
	using System.Web.Hosting;
	using System.Data.SqlClient;
	using System.IO;

	using Library.Models;

	public class AutoFillDatabaseClass
	{
		private const string DatabaseFiles = "Library\\Config\\Default_Data";

		public static void GetScripts() {

			string path = Path.Combine(new DirectoryInfo(HostingEnvironment.MapPath("~/")).Parent.FullName, DatabaseFiles);

			DirectoryInfo directory = new DirectoryInfo(path);
			List<FileInfo> files = directory.GetFiles().ToList();

			LibraryEntities Database = new LibraryEntities();
			var conn = new SqlConnection(Database.Database.Connection.ConnectionString);
			
			SqlCommand command = new SqlCommand(@"SELECT COUNT(*) FROM [dbo].[Article]", conn);
			conn.Open();
			int test = (int)command.ExecuteScalar();
			conn.Close();

			if ( test == 0)
			{
				foreach (FileInfo item in files) {
					var script = File.ReadAllText(item.FullName);
					ExecuteScript(conn,script);
				}

			}
		}

		private static void ExecuteScript(SqlConnection connection, string script)
		{
			string[] commandTextArray = System.Text.RegularExpressions.Regex.Split(script, "\r\n[\t ]*GO");

			connection.Open();

			foreach (string commandText in commandTextArray)
			{
				if (commandText.Trim() == string.Empty) continue;
				SqlCommand command = new SqlCommand(commandText, connection);
				command.ExecuteNonQuery();
			}
			connection.Close();
		}
	}
}