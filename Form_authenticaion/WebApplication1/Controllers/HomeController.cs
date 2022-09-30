using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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
            string cookieName = ".AspNetCore.Identity.Application";
            //Get cookie value from HttpRequest object for the requested page
            string cookieValue = string.Empty;
            if (Request.Cookies[cookieName] != null)
            {
                cookieValue = Request.Cookies[cookieName];
            }

            //Initialize HTML to PDF converter with Blink rendering engine 
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            BlinkConverterSettings settings = new BlinkConverterSettings();

            settings.ViewPortSize = new Syncfusion.Drawing.Size(1440, 0);

            //Add cookies as name and value pair
            settings.Cookies.Add(cookieName, cookieValue);

            //Assign Blink settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Get the current URL
            string url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(HttpContext.Request);

            url = url.Substring(0, url.LastIndexOf('/'));

             //Convert URL to PDF
             PdfDocument document = htmlConverter.Convert(url);
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "MVC_view_to_PDF.pdf");
        }
    }
}
