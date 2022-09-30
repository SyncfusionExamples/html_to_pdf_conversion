using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.HtmlToPdf;
using System;
using System.IO;

namespace Automatic_bookmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();

            //Enable automatic bookmark creation
            blinkConverterSettings.EnableBookmarks = true;

            //Assign Blink converter settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            //Convert HTML File to PDF document
            PdfDocument document = htmlConverter.Convert("https://help.syncfusion.com/file-formats/pdf/convert-html-to-pdf/blink");

            FileStream fileStream = new FileStream("bookmark_creation.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //Save and close the PDF document 
            document.Save(fileStream);
            document.Close(true);
        }
    }
}
