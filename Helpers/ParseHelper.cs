namespace Responsive.Helpers
{
	using System;
	using System.IO;
	using System.Collections;
	using System.Collections.Generic;
	using System.Web.Script.Serialization; // Add reference: System.Web.Extensions
	using System.Xml;
	using System.Xml.Serialization;
	using System.Linq;
	using System.Reflection;
	using System.ComponentModel;
	
	internal static class ParseHelpers
	{
		private static JavaScriptSerializer json;

		private static JavaScriptSerializer JSON { get { return json ?? (
			json = new JavaScriptSerializer());
		} }

		public static Stream ToStream(this string @this)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(@this);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}


		public static T ParseXML<T>(this string @this) where T : class
		{
			var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
			return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
		}

		public static T ParseJSON<T>(this string @this) where T : class
		{
			return JSON.Deserialize<T>(@this.Trim());
		}
		/*
		public static T GetObject<T>(this Dictionary<string, object> source)
			 where T : class, new()
		{
			T someObject = new T();
			Type someObjectType = someObject.GetType();

			foreach (KeyValuePair<string, object> item in source)
			{
				someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
			}

			return someObject;
		}*/
		

		public static T GetObject<T>(IDictionary<string, object> dict) where T : new()
		{
			var t = new T();
			PropertyInfo[] properties = t.GetType().GetProperties();

			foreach (PropertyInfo property in properties)
			{
				if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
					continue;

				KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

				// Find which property type (int, string, double? etc) the CURRENT property is...
				Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

				// Fix nullables...
				Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

				// ...and change the type
				//object newA = Convert.ChangeType(item.Value, newT);

				

				//Type convertToType = typeof(int);

				TypeConverter tc = TypeDescriptor.GetConverter(newT);

				object newA = tc.ConvertTo(item.Value, newT);
				
				
				t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
			}
			return t;
		}
	}
}