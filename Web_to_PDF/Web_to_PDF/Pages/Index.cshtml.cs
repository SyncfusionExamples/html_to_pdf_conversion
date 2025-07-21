using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace Web_to_PDF.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
    public async Task<IActionResult> OnPostAsync()
    {
        //Initialize HTML to PDF converter with Blink rendering engine
        HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

        BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings()
        {
            ViewPortSize = new Syncfusion.Drawing.Size(1440, 0),
            Orientation = PdfPageOrientation.Landscape
        };

        //Assign Blink converter settings to HTML converter
        htmlConverter.ConverterSettings = blinkConverterSettings;

        string url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(HttpContext.Request);

        //Convert existing URL to PDF
        PdfDocument document = htmlConverter.Convert(url);

        //Saving the PDF to the MemoryStream
        MemoryStream stream = new MemoryStream();

        document.Save(stream);

        //Close the PDF document and the HTML converter
        document.Close(true);
        htmlConverter.Close();

        //Download the PDF document in the browser
        return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Web_to_PDF.pdf");
    }
}
