using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.IO;

namespace URL_to_PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();

            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(1440, 0);

            //Assign Blink converter settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            //Convert existing URL to PDF
            PdfDocument document = htmlConverter.Convert("https://www.syncfusion.com/");

            //Save and close the PDF document 
            document.Save("URL_to_PDF.pdf");
            document.Close(true);
        }
    }
}
