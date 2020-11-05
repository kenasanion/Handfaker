using System;
using System.Collections.Generic;
using Handfaker.Core;

namespace Handfaker.Driver
{
    public class Program
    {
        static void Main(string[] args)
        {
            WordManager wordManager = new WordManager(@"/Users/kenasanion/Projects/Handfaker/Data/Lorem Ipsum.docx");
            wordManager.UpdateTextFonts(new List<string>() { "Codizal Law", "Arial", "Codizal Law" }, ReplaceType.Letter);
        }
    }
}
