namespace Library.Helpers
{
	public static class ExtensionMethods
	{
		public static string ToCapitalize(this string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return text;
			}

			return string.Format(
					"{0}{1}",
					text.Substring(0, 1).ToUpper(),
					text.Substring(1));
		}
	}	
}
