using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Handfaker.Core
{
    public class WordManager1 : IWorkManager
    {
        public string FileName { get; set; }
        public List<RunProperties> Fonts { get; set; }

        public WordManager1(string fileName)
        {
            Fonts = new List<RunProperties>();
            FileName = fileName;
        }

        public void UpdateTextFonts(IEnumerable<string> fontFamilies, ReplaceType replaceType)
        {
            // Wordfile write stream.
            Random rand = new Random();

            InitializeFontFamilies(fontFamilies);

            using (var doc = WordprocessingDocument.Open(FileName, true))
            {
                Body body = doc.MainDocumentPart.Document.Body;

                //Get all paragraphs
                var paragraphs = body.Descendants<Paragraph>().ToList();
                foreach (var paragraph in paragraphs)
                {
                    foreach(var run in paragraph.Elements<Run>())
                    {
                        int index = rand.Next(0, 2);
                        var font = Fonts[0];
                        RunProperties customFont = new RunProperties(
                            new RunFonts()
                            {
                                Ascii = "Codizal Law",
                                HighAnsi = "Codizal Law"
                            });

                        run.AppendChild(customFont);
                    }
                }

                ApplyDocumentChanges(doc);
            }
        }

        private void InitializeFontFamilies(IEnumerable<string> fontFamilies)
        {
            foreach(var fontFamily in fontFamilies)
            {
                Fonts.Add(
                    new RunProperties(
                       new RunFonts()
                       {
                           Ascii = fontFamily,
                           HighAnsi = fontFamily
                       })
                    );
            };
        }

        private void ApplyDocumentChanges(WordprocessingDocument doc)
        {
            doc.MainDocumentPart.Document.Save();
            doc.Close();
        }
    }
}
