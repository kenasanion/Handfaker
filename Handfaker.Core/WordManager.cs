using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Handfaker.Core
{
    public class WordManager : IWorkManager
    {
        public string FileName { get; set; }
        public List<RunFonts> Fonts { get; set; }

        public WordManager(string fileName)
        {
            Fonts = new List<RunFonts>();
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
                    switch (replaceType)
                    {
                        case ReplaceType.Paragraph:
                            ExtractPerParagraph(rand, paragraph);
                            break;
                        case ReplaceType.Letter:
                            ExtractPerLetter(rand, paragraph);
                            break;
                        default:
                            throw new Exception("Replace type is not supported.");
                    }
                }

                ApplyDocumentChanges(doc);
            }
        }

        private void ExtractPerParagraph(Random rand, Paragraph paragraph)
        {
            Run previousRun = null;

            foreach (var run in paragraph.Descendants<Run>())
            {
                if (run.GetFirstChild<Text>() != null)
                {
                    var previousText = run.GetFirstChild<Text>().Text;
                    previousRun = run;

                    Run newRun = new Run();
                    Text textRun = new Text() { Text = previousText };

                    int index = rand.Next(0, 2);
                    var fontRun = Fonts[index].CloneNode(true);

                    var rPr = new RunProperties();
                    rPr.Append(fontRun);

                    if (newRun != null)
                        run.ReplaceChild<RunProperties>(rPr, run.GetFirstChild<RunProperties>());
                }
            }
        }

        private void ExtractPerLetter(Random rand, Paragraph paragraph)
        {
            Run previousRun = null;

            foreach (var run in paragraph.Descendants<Run>())
            {
                var previousText = run.GetFirstChild<Text>().Text;
                previousRun = run;

                if (!string.IsNullOrEmpty(previousText))
                    run.GetFirstChild<Text>().Text = string.Empty;

                foreach (char letter in previousText)
                {
                    Run newRun = new Run();
                    Text textRun = new Text() { Text = letter.ToString() };

                    int index = rand.Next(0, 2);
                    var fontRun = Fonts[index].CloneNode(true);

                    var rPr = new RunProperties();
                    rPr.Append(textRun);
                    rPr.Append(fontRun);

                    if (newRun != null)
                        run.AppendChild<RunProperties>(rPr);
                }
            }
        }

        private void InitializeFontFamilies(IEnumerable<string> fontFamilies)
        {
            foreach (var fontFamily in fontFamilies)
            {
                Fonts.Add(
                       new RunFonts()
                       {
                           Ascii = fontFamily,
                           HighAnsi = fontFamily
                       });
            };
        }

        private void ApplyDocumentChanges(WordprocessingDocument doc)
        {
            doc.MainDocumentPart.Document.Save();
            doc.Close();
        }
    }
}
