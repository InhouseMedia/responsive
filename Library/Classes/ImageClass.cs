namespace Library.Classes
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using Library.Resources;

	public class ImageSettings
	{
		public int width { get; set; }
		public int s_brightness { get; set; }
		public int s_contrast { get; set; }
		public int s_saturation { get; set; }
		public string s_grayscale { get; set; }
		public bool s_sepia { get; set; }
		public string sflip { get; set; }
		public string watermark { get; set; }
		public string title { get; set; }
	}

	public class ImageConfig
	{

		public class ImageObject
		{
			[Display(Name = "ImageSettingsImage", ResourceType = typeof(Translate))]
			public Image image { get; set; }
			[Display(Name = "ImageSettingsColor", ResourceType = typeof(Translate))]
			public Color[] color { get; set; }
			[Display(Name = "ImageSettingsFilters", ResourceType = typeof(Translate))]
			public Filters filters { get; set; }
			[Display(Name = "ImageSettingsTransform", ResourceType = typeof(Translate))]
			public Transform transform { get; set; }
			[Display(Name = "ImageSettingsWatermark", ResourceType = typeof(Translate))]
			public Watermark watermark { get; set; }
		}

		public class Image
		{
			public string imageId { get; set; }
			public string url { get; set; }
			[Display(Name = "ImageSettingsTitle", ResourceType = typeof(Translate))]
			public string title { get; set; }
			public string alt { get; set; }
			[Display(Name = "ImageSettingsEnlarge", ResourceType = typeof(Translate))]
			public bool enlarge { get; set; }
			[Display(Name = "ImageSettingsLinkTo", ResourceType = typeof(Translate))]
			public string linkTo { get; set; }
			[Display(Name = "ImageSettingsSize", ResourceType = typeof(Translate))]
			public string size { get; set; }
			[Display(Name = "ImageSettingsPlacement", ResourceType = typeof(Translate))]
			public Placement[] placement { get; set; }
		}

		public class Placement
		{
			public string name { get; set; }
			public bool value { get; set; }
		}

		public class Color
		{
			public string name { get; set; }
			public float value { get; set; }
		}

		public class Filters
		{
			public sGrayscale[] s_grayscale { get; set; }
		}

		public class sGrayscale { 
			public string name { get; set; }
			public bool value { get; set; }
		}

		public class Transform
		{
			[Display(Name = "ImageSettingsFlip", ResourceType = typeof(Translate))]
			public Sflip[] sflip { get; set; }
		}

		public class Sflip
		{
			public string name { get; set; }
			public bool value { get; set; }
		}

		public class Watermark
		{
			[Display(Name = "ImageSettingsOverlay", ResourceType = typeof(Translate))]
			public Overlay[] overlay { get; set; }
			[Display(Name = "ImageSettingsTitle", ResourceType = typeof(Translate))]
			public Title title { get; set; }
		}

		public class Overlay {
			public string name { get; set; }
			public bool value { get; set; }
		}

		public class Title
		{
			public bool active { get; set; }
			public string title { get; set; }
		}
	}
}