using IronPDF_HTMLtoPDF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IronPDF_HTMLtoPDF.Controllers
{
    public class HomeController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public HomeController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportToPDF()
        {
            IronPdf.License.LicenseKey = "IRONSUITE.KARMEGAM.SF3701.GMAIL.COM.14291-466FC685B4-DUQAMMV6MSAJ33-AUP5T72JA3SB-RCNQDQLFE33U-3XYAXTONU5O2-VLGKHK5FV2VL-R2XU3U7VW57M-WYN435-TDFFQPRYAQWNEA-DEPLOYMENT.TRIAL-4ZVRJJ.TRIAL.EXPIRES.24.AUG.2024";
            //string fileName = "HtmlSample_Thousand";
            string fileName = "HtmlSample";
            //string fileName = "HtmlSample_FiveHundred";
            //string fileName = "HtmlSample_Hundred";
            double totalTime = 0;
            string ConsoleLog = "";
            PdfDocument pdf = null;

            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Data", fileName + ".html");
            // Disable local disk access or cross-origin requests
            Installation.EnableWebSecurity = true;
            for (int i = 0; i < 5; i++)
            {
                
                // Instantiate Renderer
                var renderer = new ChromePdfRenderer();
                ChromePdfRenderOptions chromePdfRenderOptions = new ChromePdfRenderOptions();
                chromePdfRenderOptions.MarginBottom = 0;
                chromePdfRenderOptions.MarginTop = 0;
                chromePdfRenderOptions.MarginLeft = 0;
                chromePdfRenderOptions.MarginRight = 0;
                renderer.RenderingOptions = chromePdfRenderOptions;
                Stopwatch watch = new Stopwatch();
                watch.Start();
                // Create a PDF from a HTML string using C#
                 pdf = renderer.RenderUrlAsPdf(htmlFilePath);
                watch.Stop();
                totalTime += watch.Elapsed.TotalSeconds;
                ConsoleLog += (i + 1) + " conversion time:" + watch.Elapsed.TotalSeconds + "\n";
                if (i == 0)
                {
                    pdf.SaveAs(fileName + ".pdf");
                }
            }
            double averageTime = totalTime / 5;

            ConsoleLog += "Average conversion time:" + averageTime + "\n";
            System.IO.File.WriteAllText(Path.Combine(_hostingEnvironment.WebRootPath, "Data", fileName + ".txt"), ConsoleLog);
            return File(pdf.BinaryData, System.Net.Mime.MediaTypeNames.Application.Pdf, "HTML-to-PDF.pdf");
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
    }
}
