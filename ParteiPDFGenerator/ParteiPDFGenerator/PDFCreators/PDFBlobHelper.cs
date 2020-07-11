using Azure.Storage.Blobs;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CloudbobsPDFRendering.PDFCreators
{
    public class PDFBlobHelper
    {
        private static BlobContainerClient pdfBlobContainer;

        public static async Task<PDFCreateResult> AddPdfAsync(Document document)
        {
            var result = new PDFCreateResult();

            try
            {
                string blobName = Guid.NewGuid().ToString() + ".pdf";

                BlobClient blobClient = pdfBlobContainer.GetBlobClient(blobName);

                var renderer = new PdfDocumentRenderer(true);

                renderer.Document = document;
                renderer.RenderDocument();

                using (MemoryStream stream = new MemoryStream())
                {
                    renderer.PdfDocument.Save(stream, false);
                    var resp = await blobClient.UploadAsync(stream, true);
                }

                result.Successfull = true;
                result.PayLoad = blobClient.Uri.AbsoluteUri;

                Thread t = new Thread(new ParameterizedThreadStart(DeletePdfBlob));
                t.Start(blobName);
            }
            catch (Exception e)
            {
                result.Successfull = false;
                result.PayLoad = e.Message;
            }

            return result;
        }

        private static void DeletePdfBlob(object blobName)
        {
            BlobClient blobClient = pdfBlobContainer.GetBlobClient(blobName.ToString());

            Thread.Sleep(1000 * 90);

            if (blobClient.Exists())
            {
                blobClient.Delete();
            }
        }

        public static void Setup(string key)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(key);
                pdfBlobContainer = blobServiceClient.GetBlobContainerClient("pdfs");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
