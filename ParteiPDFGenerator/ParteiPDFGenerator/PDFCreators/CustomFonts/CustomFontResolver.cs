using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

namespace Aufgabe_2.ExportManagers
{
    public class CustomFontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            var name = familyName.ToLower().TrimEnd('#');

            switch (name)
            {
                case "arial":
                    if (isBold)
                    {
                        return new FontResolverInfo("Arial#b");
                    }

                    return new FontResolverInfo("Arial#");
            }

            return new FontResolverInfo("Arial#");
        }

        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "Arial#":
                    return FontHelper.Arial;

                case "Arial#b":
                    return FontHelper.ArialBold;
            }

            return null;
        }


        internal static CustomFontResolver OurGlobalFontResolver = null;
        internal static void Apply()
        {
            if (OurGlobalFontResolver == null || GlobalFontSettings.FontResolver == null)
            {
                if (OurGlobalFontResolver == null)
                    OurGlobalFontResolver = new CustomFontResolver();

                GlobalFontSettings.FontResolver = OurGlobalFontResolver;
            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
    }

    public static class FontHelper
    {
        public static byte[] Arial
        {
            get { return LoadFontData("ParteiPDFGenerator.PDFCreators.CustomFonts.arial.arial.ttf"); }
        }

        public static byte[] ArialBold
        {
            get { return LoadFontData("ParteiPDFGenerator.PDFCreators.CustomFonts.arial.arialbd.ttf"); }
        }

        static byte[] LoadFontData(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Could not load font with name " + name + ". Make sure to add set it as an embedded resource.");
                }
                    
                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                return data;
            }
        }
    }
}
