using Aufgabe_2.ExportManagers;
using CloudbobsPDFRendering.PDFCreators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CloudbobsPDFRendering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomFontResolver.Apply();
            PDFBlobHelper.Setup();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
