using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web_to_PDF.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public IndexModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _env = env;
        }



        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {

            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.Blink);

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();

            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(1440, 0);

            //Set the BlinkBinaries folder path
            blinkConverterSettings.BlinkPath = Path.Combine(_env.ContentRootPath, "BlinkBinariesWindows");

            //Assign Blink converter settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            string url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(HttpContext.Request);

            //Convert existing URL to PDF
            PdfDocument document = htmlConverter.Convert(url);

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            //Download the PDF document in the browser
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "web_to_PDF.pdf");
        }
    }
}
