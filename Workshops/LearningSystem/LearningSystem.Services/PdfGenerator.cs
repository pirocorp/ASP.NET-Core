namespace LearningSystem.Services
{
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;

    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratePdfFromHtml(string html)
        {
            var pdfDocument = new Document(PageSize.A4, 10, 10, 10, 10);
            var htmlParser = new HtmlWorker(pdfDocument);

            using var memoryStream = new MemoryStream();

            var writer = PdfWriter.GetInstance(pdfDocument, memoryStream);
            pdfDocument.Open();

            using (var stringReader = new StringReader(html))
            {
                htmlParser.Parse(stringReader);
            }

            pdfDocument.Close();

            return memoryStream.ToArray();
        }
    }
}