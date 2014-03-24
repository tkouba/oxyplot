﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphViewController.cs" company="OxyPlot">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 OxyPlot contributors
//   
//   Permission is hereby granted, free of charge, to any person obtaining a
//   copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//   
//   The above copyright notice and this permission notice shall be included
//   in all copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//   OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//   IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//   CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//   SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleBrowser
{

	using System.Drawing;

	using MonoTouch.Foundation;
	using MonoTouch.UIKit;
	using MonoTouch.MessageUI;

	using ExampleLibrary;

	using OxyPlot.XamarinIOS;

	public class GraphViewController : UIViewController
	{
		private readonly ExampleInfo exampleInfo;

		public GraphViewController (ExampleInfo exampleInfo)
		{
			this.exampleInfo = exampleInfo;
		}

		public override void LoadView ()
		{
			NavigationItem.RightBarButtonItem= new UIBarButtonItem(UIBarButtonSystemItem.Compose,
				delegate {
					var actionSheet = new UIActionSheet ("Email", null, "Cancel", "PNG", "PDF"){
						Style = UIActionSheetStyle.Default
					};

					actionSheet.Clicked += delegate (object sender, UIButtonEventArgs args){

						if(args.ButtonIndex > 1)
							return;

						Email(args.ButtonIndex == 0 ? "png" : "pdf");
					};

					actionSheet.ShowInView (View);
				});

			// Only for iOS 7 and later?
			this.EdgesForExtendedLayout = UIRectEdge.None;

			var plot = new PlotView ();
			plot.Model = exampleInfo.PlotModel;
			plot.BackgroundColor = UIColor.White;
			this.View = plot;
		}

		private void Email(string exportType)
		{
			if(!MFMailComposeViewController.CanSendMail)
				return;

			var title = exampleInfo.Title + "." + exportType;
			NSData nsData = null;
			string attachmentType = "text/plain";
			var rect = new RectangleF(0,0,800,600);
			switch(exportType)
			{
			case "png":
				nsData =  View.ToPng(rect);
				attachmentType = "image/png";
				break;
			case "pdf":
				nsData = View.ToPdf(rect);
				attachmentType = "text/x-pdf";
				break;
			}

			var mail = new MFMailComposeViewController ();
			mail.SetSubject("OxyPlot - " + title);
			mail.SetMessageBody ("Please find attached " + title, false);
			mail.Finished += HandleMailFinished;
			mail.AddAttachmentData(nsData, attachmentType, title);

			this.PresentViewController (mail, true, null);
		}

		private void HandleMailFinished (object sender, MFComposeResultEventArgs e)
		{
			if (e.Result == MFMailComposeResult.Sent) {
				UIAlertView alert = new UIAlertView ("Mail Alert", "Mail Sent",
					null, "Yippie", null);
				alert.Show ();

				// you should handle other values that could be returned
				// in e.Result and also in e.Error
			}

			e.Controller.DismissViewController(true, null);
		}
	}
}