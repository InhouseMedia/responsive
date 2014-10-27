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
		public static void GetScripts() {

			DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\default_data"));
			List<FileInfo> files = directory.GetFiles().ToList();

			ResponsiveContext test = new ResponsiveContext();
			var conn = new SqlConnection(test.Database.Connection.ConnectionString);
			
			foreach (FileInfo item in files) {
				var script = File.ReadAllText(item.FullName);
				ExecuteScript(conn,script);
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