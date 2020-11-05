// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Handfaker
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField FileDirectoryField { get; set; }

		[Outlet]
		AppKit.NSPopUpButton Font1Outlet { get; set; }

		[Outlet]
		AppKit.NSPopUpButton Font2Outlet { get; set; }

		[Outlet]
		AppKit.NSPopUpButton Font3Outlet { get; set; }

		[Action ("ChooseFile:")]
		partial void ChooseFile (AppKit.NSButton sender);

		[Action ("Generate:")]
		partial void Generate (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Font1Outlet != null) {
				Font1Outlet.Dispose ();
				Font1Outlet = null;
			}

			if (Font2Outlet != null) {
				Font2Outlet.Dispose ();
				Font2Outlet = null;
			}

			if (Font3Outlet != null) {
				Font3Outlet.Dispose ();
				Font3Outlet = null;
			}

			if (FileDirectoryField != null) {
				FileDirectoryField.Dispose ();
				FileDirectoryField = null;
			}
		}
	}
}
