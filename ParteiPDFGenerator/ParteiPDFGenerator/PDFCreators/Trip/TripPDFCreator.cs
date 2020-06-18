using CloudbobsPDFRendering.PDFCreators.Trip;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CloudbobsPDFRendering.PDFCreators
{
    public class TripPDFCreator
    {
        private static readonly string[] tableColumns = new string[]
        {
            "Vorname",
            "Nachname",
            "Wohnort",
            "Stop",
            "Eigentliche Kosten",
            "Tatsächliche Kosten"
        };

        public static async Task<PDFCreateResult> Create(TripPDFModel model)
        {
            var document = new Document();
            Styles.Define(document);

            var headerResult = CreateHeader(model, document);
            CreateInfoListing(model, headerResult.Item2);
            CreateMemberList(model, document);

            var result = await PDFBlobHelper.AddPdfAsync(document);

            if (headerResult.Item1 != null)
            {
                File.Delete(headerResult.Item1);
            }

            return result;
        }

        private static void CreateMemberList(TripPDFModel model, Document document)
        {
            var memberSection = document.AddSection();

            memberSection.AddParagraph("Reisemitglieder", "Heading1");
            memberSection.AddParagraph();
            var table = memberSection.AddTable();
            table.Borders.Width = 0.75;

            TableUtils.CreateTableColumns(table, tableColumns);
            TableUtils.CreateTableHeader(table, tableColumns);
            FillMemberTable(model.Members, table);
        }

        private static void FillMemberTable(List<TravelMemberPDFModel> members, Table table)
        {
            for (int i = 0; i < members.Count; i++)
            {
                TravelMemberPDFModel member = members[i];

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
                row.Cells[2].AddParagraph(member.City);
                row.Cells[3].AddParagraph(member.Stop);
                row.Cells[4].AddParagraph($"{member.TargetCosts}€");
                row.Cells[5].AddParagraph($"{member.ActualCosts}€");
            }
        }

        private static void CreateInfoListing(TripPDFModel model, Section headerSection)
        {
            headerSection.AddParagraph();
            headerSection.AddParagraph($"{model.Description}", "Heading6");
            headerSection.AddParagraph();

            var costParagraph = headerSection.AddParagraph("", "Heading4");
            costParagraph.AddFormattedText("Kosten: ", TextFormat.Bold);
            costParagraph.AddText($"{model.Costs}€");

            var timeParagraph = headerSection.AddParagraph("", "Heading4");
            timeParagraph.AddFormattedText("Von: ", TextFormat.Bold);
            timeParagraph.AddText($"{model.StartDate} Uhr");
            timeParagraph.AddFormattedText(" Bis: ", TextFormat.Bold);
            timeParagraph.AddText($"{model.EndDate} Uhr");

            var travelMembersParagraph = headerSection.AddParagraph("", "Heading4");
            travelMembersParagraph.AddFormattedText("Reisemitglieder: ", TextFormat.Bold);
            travelMembersParagraph.AddText($"{model.Members.Count}");
        }

        private static (String, Section) CreateHeader(TripPDFModel model, Document document)
        {
            var section = document.AddSection();

            section.AddParagraph("Reise nach", "Heading1");
            section.AddParagraph(model.Destination, "Heading1");
            section.AddParagraph();

            string fileName = null;

            try
            {
                fileName = Path.GetFileName(model.ImageBlobURL);
                WebClient cln = new WebClient();
                cln.DownloadFile(model.ImageBlobURL, fileName);

                var image = section.AddImage(fileName);
                image.Width = "15.0cm";
                image.LockAspectRatio = true;
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                fileName = null;
            }
            
            return (fileName, section);
        }
    }
}
