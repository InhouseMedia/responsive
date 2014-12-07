namespace Library.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using Library.Resources;


	public class Article_Datanotation
	{
		[Range(0, 2)]
		[Display(Name = "WebgridActive", ResourceType = typeof(Translate))]
		public byte Active;

		[Display(Name = "WebgridCreatedBy", ResourceType = typeof(Translate))]
		public string Created_By;

		[Display(Name = "WebgridCreationDate", ResourceType = typeof(Translate))]
		public DateTime Creation_Date;
	}

	public class Article_Metadata_Datanotation
	{
		[StringLength(50)]
		[Display(Name = "WebgridMetaTitle", Prompt = "WebgridMetaTitleInfo", ResourceType = typeof(Translate))]
		public string Meta_Title;

		[StringLength(50)]
		[Display(Name = "WebgridKeywords", Prompt = "WebgridKeywordsInfo", ResourceType = typeof(Translate))]
		public string Meta_Keywords;


		[StringLength(50)]
		[Display(Name = "WebgridMetaDescription", Prompt = "WebgridMetaDescriptionInfo", ResourceType = typeof(Translate))]
		public string Meta_Description;
	}

	public class Article_Content_Datanotation
	{
		[Range(0, 4)]
		public Nullable<decimal> Grade;
	}

	[MetadataType(typeof(Article_Datanotation))]
	public partial class Article { }

	[MetadataType(typeof(Article_Metadata_Datanotation))]
	public partial class Article_Metadata { }

	[MetadataType(typeof(Article_Content_Datanotation))]
	public partial class Article_Content { }
}