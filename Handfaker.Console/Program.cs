using System;
using System.Collections.Generic;
using Handfaker.Core;

namespace Handfaker.Driver
{
    public class Program
    {
        static void Main(string[] args)
        {
            WordManager wordManager = new WordManager(@"/Users/kenasanion/Projects/Handfaker/Data/cases-corpo-converted.docx");
            wordManager.UpdateTextFonts(new List<string>() { "Codizal Law One", "Codizal Law Two", "Codizal Law Three" }, ReplaceType.Paragraph);
        }
    }
}
