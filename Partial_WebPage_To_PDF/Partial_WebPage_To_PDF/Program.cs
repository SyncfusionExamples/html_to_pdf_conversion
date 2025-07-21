using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

//Initialize HTML to PDF converter. 
HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

htmlConverter.ConverterSettings = new BlinkConverterSettings() { ViewPortSize = new Syncfusion.Drawing.Size(1280, 1024) };

//Convert URL to PDF document. 
PdfDocument document = htmlConverter.ConvertPartialHtml(File.ReadAllText("../../../../../Data/partialdemo.html"), "", "details");

//Save and closes the PDF document.
document.Save("Partial-webpage-to-PDF.pdf");
document.Close(true);
htmlConverter.Close();

