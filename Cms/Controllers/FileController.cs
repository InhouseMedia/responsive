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

	using System.Net.NetworkInformation;

	using ImageResizer;

	using Library.Classes;
	using Library.Models;

	public class FileController : Controller
    {
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

        // GET: file
        public ActionResult Index()
        {
            return View();
        }

		// GET: getFile
		[HttpGet]
		public ActionResult GetFile(string fileType, string fileName)
		{
			string filePath = Path.Combine("\\", "Documents", fileType, fileName);

			var ctx = new DocumentEntities();
				ctx.DocumentsView.Load();

			var data = ctx.DocumentsView.Local;
			
 			var stream = data.FirstOrDefault(m => m.file_path == filePath);

			// When the image isn't found in the database we return a 404 image
			if (stream == null) { 
				string errorImage = "/Content/img/404.jpg";
				Response.StatusCode = 404; 
				Response.TrySkipIisCustomErrors = true;
				return File(errorImage, MimeMapping.GetMimeMapping(errorImage));
			}


			Stream target = new MemoryStream();
			
			// Check the type of internet connection and determen the image quality/filesize that's going to be presented
			// This will prevent that you are using 3mb images on a mobile device that's using a 3G internet connection.
			Instructions imageSettings = new Instructions(Request.QueryString);
			imageSettings.OutputFormat = OutputFormat.Jpeg;
			imageSettings.JpegQuality = getConnectionTypeQuality();

			new ImageJob(stream.file_stream, target, imageSettings).Build();

			target.Seek(0, SeekOrigin.Begin);

			return File(target, MimeMapping.GetMimeMapping(fileName));
		}


		public ActionResult SaveFile()
		{
			bool isSavedSuccessfully = true;
			string fName = "";
			try
			{
				using (DocumentEntities db = new DocumentEntities())
				{
					foreach (string fileName in Request.Files)
					{
						HttpPostedFileBase file = Request.Files[fileName];
						//Save file content goes here
						fName = file.FileName;

						MemoryStream target = new MemoryStream();
						file.InputStream.CopyTo(target);
						byte[] data = target.ToArray();
						
						try
						{
							Image image = Bitmap.FromStream(file.InputStream); // valid image stream
							

							double imageWidth = image.Width;
							double imageHeight = image.Height;

							if (imageWidth > 3200 || imageHeight > 3200)
							{
								double ratio = imageWidth / imageHeight;
								string imageType = (ratio < 1)? "portrait" : "landscape";

								int newHeight = (ratio < 1)? 3200 : (int) Math.Round(imageHeight / imageWidth * 3200);
								int newWidth = (ratio >= 1)? 3200 : (int) Math.Round(imageWidth / imageHeight * 3200);

								Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
								newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
								
								using (Graphics gr = Graphics.FromImage(newImage))
								{
									gr.Clear(Color.Transparent);
									gr.SmoothingMode = SmoothingMode.AntiAlias;
									//gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
									//gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
									gr.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
			
									MemoryStream target2 = new MemoryStream();
									newImage.Save(target2,ImageFormat.Jpeg);
									
									data = target2.ToArray();

									myEncoderParameters = new EncoderParameters(1);
								}

								
							}


							var tempFile = db.Documents_Add(fName, data);


							/*
							//Save multiple image sizes
							var imageSettings = ConfigClass.Settings.controllers.files.image.sizes;
							PropertyInfo[] imageProperties = imageSettings.GetType().GetProperties();
							
							foreach (PropertyInfo property in imageProperties) {
								string name = (property.Name == "large") ? "" : "_" + property.Name;

								int size = (int)property.GetValue(imageSettings, null);

								string extension = Path.GetExtension(fName);
								string filename = Path.GetFileNameWithoutExtension(fName);

								using (Graphics gr = Graphics.FromImage(newImage))
								{
									gr.Clear(Color.Transparent);
									gr.SmoothingMode = SmoothingMode.AntiAlias;
									gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
									gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
									gr.DrawImage(image, new Rectangle(0, 0, size, size));
								}

								byte[] test = ImageToByte2(newImage);

								var tempFile = db.Documents_Add(filename + name + extension, test);
							}*/
						}
						catch
						{
							// not an image
							var tempFile = db.Documents_Add(fName, data);
						}


					}
				}
			}
			catch (Exception ex)
			{
				isSavedSuccessfully = false;
			}


			if (isSavedSuccessfully)
			{
				return Json(new { Message = fName });
			}
			else
			{
				return Json(new { Message = "Error in saving file" });
			}
		}

		[OutputCache(Duration = 3600, VaryByParam = "none")]
		public int getConnectionTypeQuality()
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
    }
}