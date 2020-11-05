using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using AppKit;
using Foundation;

namespace Handfaker
{
    public partial class ViewController : NSViewController
    {
        private string currentFilePath { get; set; }
        private List<string> fontFamilies { get; set; }
        private static List<string> AllowedFonts { get; set; } = new List<string>{ "Codizal Law", "Calibri", "Arial", "Proxima Nova" };

        public ViewController(IntPtr handle) : base(handle)
        {
            fontFamilies = new List<string>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Remove all items in the dropdown
            Font1Outlet.RemoveAllItems();
            Font2Outlet.RemoveAllItems();
            Font3Outlet.RemoveAllItems();

            // Get all available fonts in the system by family
            var fontFamilies = NSFontManager.SharedFontManager.AvailableFontFamilies.Where(f => AllowedFonts.Contains(f)).ToArray();
            Font1Outlet.AddItems(fontFamilies);
            Font2Outlet.AddItems(fontFamilies);
            Font3Outlet.AddItems(fontFamilies);
        }

        partial void ChooseFile(NSButton sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "docx", "doc" };

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    FileDirectoryField.StringValue = url.Path;
                }
            }
        }

        partial void Generate(AppKit.NSButton sender)
        {
            currentFilePath = FileDirectoryField.StringValue;

            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "Randomizing fonts for the word file successful!",
                MessageText = "Generate",
            };
            alert.RunModal();
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
