using Microsoft.AspNetCore.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.Diagnostics;
using Web_MVC_to_PDF.Models;

namespace Web_MVC_to_PDF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ExportToPDF()
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings()
            {
                ViewPortSize = new Syncfusion.Drawing.Size(1440, 0),
                Orientation = PdfPageOrientation.Landscape
            };

            //Assign Blink settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            //Get the current URL
            string url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(HttpContext.Request);

            url = url.Substring(0, url.LastIndexOf('/'));

            //Convert URL to PDF
            PdfDocument document = htmlConverter.Convert(url);

            MemoryStream stream = new MemoryStream();
            //Close the PDF document and the HTML converter
            document.Save(stream);
            document.Close(true);
            htmlConverter.Close();
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "MVC_view_to_PDF.pdf");
        }
    }
}
