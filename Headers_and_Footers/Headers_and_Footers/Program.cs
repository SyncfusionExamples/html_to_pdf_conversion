using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.IO;

namespace Headers_and_Footers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.Blink);

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();

            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(794, 1123);           

            //Adding header
            blinkConverterSettings.PdfHeader = CreateHeader();
            //Adding footer
            blinkConverterSettings.PdfFooter = CreateFooter();

            //Assign Blink converter settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            //Convert HTML File to PDF
            PdfDocument document = htmlConverter.Convert(Path.GetFullPath("../../../../../Data/html_file.html"));

            FileStream fileStream = new FileStream("Headers_and_Footers.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //Save and close the PDF document 
            document.Save(fileStream);
            document.Close(true);
            htmlConverter.Close();
        }

        //Create header for HTML to PDF converter
        public static PdfPageTemplateElement CreateHeader()
        {
            RectangleF bounds = new RectangleF(0, 0, PdfPageSize.A4.Width, 30);

            //Create a new page template and assigning the bounds
            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

            PdfBrush brush = new PdfSolidBrush(Color.Black);

            string headerText = "Syncfusion HTML to PDF Converter";

            SizeF textSize = font.MeasureString(headerText);

            //Drawing text field in header
            header.Graphics.DrawString(headerText, font, brush, new PointF((bounds.Width - textSize.Width) / 2, 5));

            //Drawing date text in header
            header.Graphics.DrawString(DateTime.Now.ToString("dd/M/yyyy"), font, brush, new PointF(10, 5));

            //Drawing line in header
            header.Graphics.DrawLine(PdfPens.Gray, new PointF(0, bounds.Height - 2), new PointF(bounds.Width, bounds.Height - 2));

            return header;
        }

        //Create footer for HTML to PDF converter
        public static PdfPageTemplateElement CreateFooter()
        {
            RectangleF bounds = new RectangleF(0, 0, PdfPageSize.A4.Width, 30);

            //Create a new page template and assigning the bounds
            PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds);

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);

            PdfBrush brush = new PdfSolidBrush(Color.Black);

            string footerText = "Copyright © 2001 - 2021 Syncfusion Inc. All Rights Reserved";

            SizeF textSize = font.MeasureString(footerText);

            //Create a text field to draw in footer
            PdfCompositeField compositeField = new PdfCompositeField(font, brush, footerText);

            //Create page number field to show page numbering in footer, this field automatically get update for each page.
            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);
            PdfPageCountField count = new PdfPageCountField(font, brush);
            PdfCompositeField pageNumberField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);

            //Drawing line in footer
            footer.Graphics.DrawLine(PdfPens.Gray, new PointF(0, 2), new PointF(bounds.Width, 2));
            //Drawing text field in footer
            compositeField.Draw(footer.Graphics, new PointF((bounds.Width - textSize.Width) / 2, 5));
            //Drawing page number field in footer
            pageNumberField.Draw(footer.Graphics, new PointF((bounds.Width - 120), 5));

            return footer;
        }
    }
}
