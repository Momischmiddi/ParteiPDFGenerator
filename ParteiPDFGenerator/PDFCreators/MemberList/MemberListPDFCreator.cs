using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Threading.Tasks;

namespace CloudbobsPDFRendering.PDFCreators
{
    public class MemberListPDFCreator
    {
        private static readonly string[] tableColumns = new string[]
        {
            "Vorname",
            "Nachname",
            "Postleitzahl",
            "Wohnort",
            "Adresse",
            "Geburtstag",
            "Beitrag"
        };
        public static async Task<PDFCreateResult> Create(MemberListPDFModel model)
        {
            var document = new Document();
            Styles.Define(document);

            var section = CreateDocumentSection(document);
            CreateDocumentTable(section, model);

            return await PDFBlobHelper.AddPdfAsync(document);
        }

        private static Section CreateDocumentSection(Document document)
        {
            var section = document.AddSection();

            section.AddParagraph("Mitgliederliste", "Heading1");
            section.AddParagraph("Stand: " + DateTime.Now.ToString("dd/MM/yyyy"), "Heading2");
            section.AddParagraph("");
            section.AddParagraph("");

            return section;
        }

        private static void CreateDocumentTable(Section section, MemberListPDFModel model)
        {
            var table = section.AddTable();
            table.Borders.Width = 0.75;

            TableUtils.CreateTableColumns(table, tableColumns);
            TableUtils.CreateTableHeader(table, tableColumns);
            FillTable(model, table);            
        }

        private static void FillTable(MemberListPDFModel model, Table table)
        {
            for (int i = 0; i < model.Members.Count; i++)
            {
                var member = model.Members[i];

                Row row = table.AddRow();

                if (i == 0 || i % 2 == 0)
                {
                    row.Shading.Color = new Color(255, 255, 255);
                }
                else
                {
                    row.Shading.Color = new Color(222, 234, 246);
                }

                row.Cells[0].AddParagraph(member.PreName);
                row.Cells[1].AddParagraph(member.LastName);
                row.Cells[2].AddParagraph(member.Postal);
                row.Cells[3].AddParagraph(member.City);
                row.Cells[4].AddParagraph(member.Address);
                row.Cells[5].AddParagraph(member.DateOfBirth.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")) + " (" + (DateHelpers.CalculateAge(member.DateOfBirth).Item1) + ")");
                row.Cells[6].AddParagraph(member.Contribution.ToString() + "€");
            }
        }
    }
}
