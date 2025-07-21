using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;


namespace HTML_String_To_PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            //Convert HTML File to PDF
            PdfDocument document = htmlConverter.Convert("<h1>Hello world</h1>", "");

            //Save and close the PDF document 
            document.Save("HTML-To-PDF.pdf");
            document.Close(true);
        }
    }
}
