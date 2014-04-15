using System;
using MonoTouch.UIKit;
using System.Drawing;
using CardIOSDK.iOS;

namespace CardIOSample.iOS
{
	public class MainViewController
		:UIViewController
	{
		public MainViewController ()
			:base(null,null)
		{
			Title = "Card.IO Binding Sample";
			View.BackgroundColor = UIColor.White;

			UIButton TapToStartButton = new UIButton (UIButtonType.RoundedRect) {
				Frame = new RectangleF(0,0,View.Frame.Width,View.Frame.Height)
			};
			TapToStartButton.SetTitleColor (Constants.CardIOGreen, UIControlState.Normal);
			TapToStartButton.SetTitle ("Tap to Launch Card.IO", UIControlState.Normal);
			TapToStartButton.TouchUpInside += HandleStartButtonTapped;
			View.AddSubview (TapToStartButton);
		}

		void HandleStartButtonTapped (object sender, EventArgs e)
		{
			DispatchingCardIOViewController CardIOVC = new DispatchingCardIOViewController (Constants.CardIOAppToken);
			CardIOVC.CardIOCanceled += delegate(DispatchingCardIOViewController ViewController) {
				new UIAlertView("Canceled","The Card.IO operation was canceled.",null,"OK").Show();
			};
			CardIOVC.CardIOCompleted += delegate(DispatchingCardIOViewController ViewController, CardIOCreditCardInfo CreditCardInfo) {
				string Message = string.Format("{0}: {1}",CreditCardInfo.CardType.ToString(), CreditCardInfo.CardNumber);
				new UIAlertView("Complete",Message,null,"OK").Show();
			};
			PresentViewController (CardIOVC, true, () => {});
		}
	}
}

