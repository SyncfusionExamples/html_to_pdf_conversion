using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.HtmlToPdf;
using System;
using System.IO;

namespace Automatic_toc_creation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();

            //Enable automatic TOC creation
            blinkConverterSettings.EnableToc = true;

            //Set the style for level 1(H1) items in table of contents
            HtmlToPdfTocStyle tocstyleH1 = new HtmlToPdfTocStyle();

            tocstyleH1.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10, PdfFontStyle.Regular);
            tocstyleH1.BackgroundColor = new PdfSolidBrush(new PdfColor(Color.FromArgb(68, 114, 196)));
            tocstyleH1.ForeColor = PdfBrushes.White;
            tocstyleH1.Padding = new PdfPaddings(5, 5, 3, 3);

            //Apply this style to level 1 (H1) headings of the TOC
            blinkConverterSettings.Toc.SetItemStyle(1, tocstyleH1);

            //Assign Blink converter settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            //Convert HTML File to PDF document
            PdfDocument document = htmlConverter.Convert("https://help.syncfusion.com/file-formats/pdf/convert-html-to-pdf/blink");

            FileStream fileStream = new FileStream("toc_creation.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //Save and close the PDF document 
            document.Save(fileStream);
            document.Close(true);
            htmlConverter.Close();
        }
    }
}
