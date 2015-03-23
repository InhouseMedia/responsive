namespace Library.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Data.Entity;
	using System.Web.Mvc;
	using Microsoft.AspNet.Identity.EntityFramework;

	using Library.Resources;

	public class Article_Datanotation
	{
		[Range(0, 2)]
		[Display(Name = "WebgridActive", ResourceType = typeof(Translate))]
		public byte Active;

		[Display(Name = "WebgridTemplate", ResourceType = typeof(Translate))]
		public string Template;

		[DisplayFormat(DataFormatString = "{0:g}")]
		[Display(Name = "WebgridCreatedBy", ResourceType = typeof(Translate))]
		public string Created_By;

		[DisplayFormat(DataFormatString = "{0:g}")]
		[Display(Name = "WebgridCreationDate", ResourceType = typeof(Translate))]
		public DateTime Creation_Date;
	}

	public class Article_Metadata_Datanotation
	{
		[StringLength(100)]
		[Display(Name = "WebgridMetaTitle", Prompt = "WebgridMetaTitleInfo", ResourceType = typeof(Translate))]
		public string Meta_Title;

		[StringLength(250)]
		[Display(Name = "WebgridKeywords", Prompt = "WebgridKeywordsInfo", ResourceType = typeof(Translate))]
		public string Meta_Keywords;

		[StringLength(250)]
		[Display(Name = "WebgridMetaDescription", Prompt = "WebgridMetaDescriptionInfo", ResourceType = typeof(Translate))]
		public string Meta_Description;
	}
	
	public class Article_Content_Datanotation
	{

		[AllowHtml]
		[Display(Name = "ArticleText", ResourceType = typeof(Translate))]	
		public string Text {get; set;}

		//[Range(0, 4)]
		//public Nullable<decimal> Grade;
	}

	public class Article_PublishLogs_Datanotation
	{
		//[Key, ForeignKey("User")]
		[Display(Name = "WebgridPublishedBy", ResourceType = typeof(Translate))]
		public string Published_By;

		//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
		[Display(Name = "WebgridPublishedDate", ResourceType = typeof(Translate))]
		public DateTime Published_Date;
	}

	[MetadataType(typeof(Article_Datanotation))]
	public partial class Article { }

	[MetadataType(typeof(Article_Metadata_Datanotation))]
	public partial class Article_Metadata { }

	[MetadataType(typeof(Article_Content_Datanotation))]
	public partial class Article_Content { }

	[MetadataType(typeof(Article_PublishLogs_Datanotation))]
	public partial class Article_PublishLogs { }
}