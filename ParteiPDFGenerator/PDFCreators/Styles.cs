using MigraDoc.DocumentObjectModel;

namespace CloudbobsPDFRendering.PDFCreators
{
    public class Styles
    {
        public static void Define(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Arial";

            style = document.Styles["Heading1"];
            style.Font.Size = 26;
            style.Font.Bold = false;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles["Heading2"];
            style.Font.Size = 17;
            style.Font.Bold = false;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles["Heading3"];
            style.Font.Size = 15;
            style.Font.Bold = false;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            style = document.Styles["Heading4"];
            style.Font.Size = 15;
            style.Font.Bold = false;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            style = document.Styles["Heading5"];
            style.Font.Size = 15;
            style.Font.Bold = true;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            style = document.Styles["Heading6"];
            style.Font.Size = 16;
            style.Font.Bold = false;
            style.Font.Color = Colors.Black;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
        }
    }
}
