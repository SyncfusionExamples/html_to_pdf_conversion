using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;

namespace Html_form_to_PDF_form
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize HTML to PDF converter with Blink rendering engine
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();

            //Convert web forms to PDF interactive forms
            blinkConverterSettings.EnableForm = true;

            //Assign Blink converter settings to HTML converter
            htmlConverter.ConverterSettings = blinkConverterSettings;

            //Convert HTML File to PDF
            PdfDocument document = htmlConverter.Convert(Path.GetFullPath("../../../../../Data/HTMLForm.htm"));

            FileStream fileStream = new FileStream("HTML_form_to_PDF_form.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //Save and close the PDF document 
            document.Save(fileStream);
            document.Close(true);
            htmlConverter.Close();
        }
    }
}
