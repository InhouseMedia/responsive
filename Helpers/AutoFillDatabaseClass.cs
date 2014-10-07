using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

namespace Responsive.Helpers
{
	public class AutoFillDatabaseClass
	{
		public void AutoFillDatabaseClass() {


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