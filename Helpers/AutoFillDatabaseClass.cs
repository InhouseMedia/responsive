using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Data.SqlClient;
using System.IO;

namespace Responsive.Helpers
{
	public class AutoFillDatabaseClass
	{
		public static void GetScripts() {

			DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\default_data"));
			var files = directory.GetFiles().ToList();
			var test = "test";
			//qry = open('create_table_user.sql', 'r').read();

			//ExecuteScript();
		
		}

		protected virtual void ExecuteScript(SqlConnection connection, string script)
		{
			//string[] commandTextArray = script.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries); // See EDIT below!
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