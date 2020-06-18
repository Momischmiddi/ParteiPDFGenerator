using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace CloudbobsPDFRendering.PDFCreators
{
    public class TableUtils
    {
        public static void CreateTableColumns(Table table, string[] tableColumns)
        {
            foreach (string colum in tableColumns)
            {
                Column tableColumn = table.AddColumn();
                tableColumn.Format.Alignment = ParagraphAlignment.Center;
            }
        }

        public static void CreateTableHeader(Table table, string[] tableColumns)
        {
            Row row = table.AddRow();
            row.Shading.Color = new Color(91, 155, 213);

            for (int i = 0; i < tableColumns.Length; i++)
            {
                row.Cells[i].AddParagraph(tableColumns[i]);
            }
        }
    }
}
