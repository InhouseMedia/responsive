namespace Library.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Globalization;
	using System.Text;
	using System.Threading.Tasks;

	public static class ExtensionMethods
	{
		public static string ToCapitalize(this string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return text;
			}

			//return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(self);
			return string.Format(
					"{0}{1}",
					text.Substring(0, 1).ToUpper(),
					text.Substring(1));

		}
	}

	
}
