using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;

namespace HTML_File_To_PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            //Convert HTML File to PDF
            PdfDocument document = htmlConverter.Convert(Path.GetFullPath("../../../../../Data/html_file_converter.htm"));

            //Save and close the PDF document 
            document.Save("HTML_file_to_PDF.pdf");
            document.Close(true);
        }
    }
}
