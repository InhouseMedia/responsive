namespace Cms.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Reflection;

	using System.Linq;
	using System.Web;
	using System.Web.Helpers;
	using System.Web.Mvc;
	using System.Web.Script.Serialization;

	using System.Net.NetworkInformation;

	using ImageResizer;
	using Newtonsoft.Json.Linq;

	using Library.Classes;
	using Library.Models;

	public class FileController : Controller
	{
		// GET: file
		public ActionResult Index()
		{
			return View();
		}


		// GET: getFile
		[HttpGet]
		public ActionResult GetFile(string fileType, string fileName)
		{

			switch (fileType)
			{
				case "Images":
					return GetImage(fileType, fileName);
				default:
					return null;
			}
		}

		public static DocumentsView GetImageSettings(string fileType, string fileName)
		{
			string filePath = Path.Combine("\\", "Documents", fileType, fileName);

			var ctx = new DocumentEntities();
			ctx.DocumentsView.Load();

			var data = ctx.DocumentsView.Local;

			return data.FirstOrDefault(m => m.file_path == filePath);
		}

		public ActionResult GetImageFile(DocumentsView stream) {

			// When the image isn't found in the database we return a 404 image
			if (stream == null)
			{
				string errorImage = "/Content/img/404.jpg";	
				Response.StatusCode = 404;
				Response.TrySkipIisCustomErrors = true;
				return File(errorImage, MimeMapping.GetMimeMapping(errorImage));
			}

			string fileName = stream.name;

			Stream target = new MemoryStream();

			// Check the type of internet connection and determen the image quality/filesize that's going to be presented
			// This will prevent that you are using 3mb images on a mobile device that's using a 3G internet connection.
			//string json = "{s.sepia:true,sflip:xy}";
			JObject jObj = JObject.Parse(stream.settings);
			float output;

			var settings = String.Join("&",
				jObj.Children().Cast<JProperty>().Select(
					jp => jp.Name + "=" +
						HttpUtility.UrlEncode(
							(float.TryParse(jp.Value.ToString(), out output)) ? jp.Value.ToString().Replace(",", ".") : jp.Value.ToString()
						)
				)
			);

			//Instructions imageSettings = new Instructions(Request.QueryString);
			Instructions imageSettings = new Instructions(settings);
			imageSettings.OutputFormat = OutputFormat.Jpeg;
			imageSettings.JpegQuality = getConnectionTypeQuality();
			imageSettings.Mode = FitMode.Crop;

			new ImageJob(stream.file_stream, target, imageSettings).Build();

			target.Seek(0, SeekOrigin.Begin);
			
			return File(target, MimeMapping.GetMimeMapping(fileName));
		}

		// GET: getImage
		[HttpGet]
		public ActionResult GetImage(string fileType, string fileName)
		{
			DocumentsView settings = GetImageSettings(fileType, fileName);
			return GetImageFile(settings);
		}


		public ActionResult SaveImage()
		{
			string returnMessage = "Undefined error";
			string filename = "";
			try
			{
				using (DocumentEntities db = new DocumentEntities())
				{
					foreach (string fileName in Request.Files)
					{
						HttpPostedFileBase file = Request.Files[fileName];
						//Save file content goes here
						filename = file.FileName.ToLower();

						MemoryStream target = new MemoryStream();

						try
						{
							Image image = Bitmap.FromStream(file.InputStream); // valid image stream

							// All images that are uploaded will be generated again.
							// This is done because we are saving them all as jpeg with a 10% compression.
							// This way we save datastorage, increase download speed and protect the server for virusses.

							double imageWidth = image.Width;
							double imageHeight = image.Height;
							int newWidth = image.Width;
							int newHeight = image.Height;

							// Scale the image if it is to big. 
							// ImageResizer library can only handle images that are smaller then 3200 pixels.
							if (imageWidth > 3200 || imageHeight > 3200)
							{
								double ratio = imageWidth / imageHeight;
								//string imageType = (ratio < 1)? "portrait" : "landscape";

								newHeight = (ratio < 1) ? 3200 : (int)Math.Round(imageHeight / imageWidth * 3200);
								newWidth = (ratio >= 1) ? 3200 : (int)Math.Round(imageWidth / imageHeight * 3200);
							}

							// Create an imageholder for the newly created image
							Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
							newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

							// Compress the image with 10%. this will create a much smaller image and the quality is still high.
							EncoderParameters encoderParams = new EncoderParameters()
							{
								Param = new[] { new EncoderParameter(Encoder.Quality, 90L) }
							};

							// Set encoder to JPEG
							var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);


							using (Graphics gr = Graphics.FromImage(newImage))
							{
								gr.Clear(Color.Transparent);
								gr.SmoothingMode = SmoothingMode.AntiAlias;
								gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
								gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
								gr.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));

								newImage.Save(target, encoder, encoderParams);
							}

							var tempFile = db.Documents_Add(filename, target.ToArray());
							returnMessage = filename;
						}
						catch
						{
							// not an image
							//file.InputStream.CopyTo(target);
							//var tempFile = db.Documents_Add(filename, target.ToArray());
							returnMessage = "File is not an image";
						}
					}
				}
			}
			catch (Exception ex)
			{
				returnMessage = "Error in saving image";
			}

			return Json(new { Message = returnMessage });
		}

		[OutputCache(Duration = 3600, VaryByParam = "none")]
		public static int getConnectionTypeQuality()
		{

			NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

			NetworkInterfaceType type = adapters.FirstOrDefault(m => m.OperationalStatus == OperationalStatus.Up).NetworkInterfaceType;

			switch (type)
			{
				case NetworkInterfaceType.Ethernet:
					return 100;
				case NetworkInterfaceType.Wireless80211:
					return 80;
				case NetworkInterfaceType.Ppp:
					return 50;
				default:
					return 75;
			}
		}

		// Not used
		public static byte[] ImageToByte2(Image img)
		{
			byte[] byteArray = new byte[0];
			using (MemoryStream stream = new MemoryStream())
			{
				img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
				stream.Close();

				byteArray = stream.ToArray();
			}
			return byteArray;
		}
	}
}