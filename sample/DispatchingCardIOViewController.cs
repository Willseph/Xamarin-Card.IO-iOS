using System;
using CardIOSDK.iOS;

namespace CardIOSample.iOS
{
	/// <summary>
	/// An extension of the CardIOPaymentViewController class, which should be used in its place for better functionality.
	/// </summary>
	public class DispatchingCardIOViewController
		: CardIOPaymentViewController
	{
		#region Delegates
		public delegate void CardIOCanceledEventHandler (DispatchingCardIOViewController ViewController);
		public delegate void CardIOCompletedEventHandler (DispatchingCardIOViewController ViewController, CardIOCreditCardInfo CreditCardInfo);
		#endregion //Delegates

		#region Events
		/// <summary>
		/// Occurs after the user cancels the Card IO view controller.
		/// </summary>
		public event CardIOCanceledEventHandler CardIOCanceled;

		/// <summary>
		/// Occurs after the user completes the Card IO view controller.
		/// </summary>
		public event CardIOCompletedEventHandler CardIOCompleted;
		#endregion //Events

		#region Properties
		/// <summary>
		/// Gets the delegate object which fires events from the view controller.
		/// </summary>
		internal CardIODispatcher Dispatcher { get; set; }
		#endregion //Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CardIOSDK.DispatchingCardIOViewController"/> class.
		/// </summary>
		/// <param name="APIToken">Your Card.IO SDK API token.</param>
		/// <param name="CollectExpiry">If set to <c>true</c>, the view controller will collect card expiry information.</param>
		/// <param name="CollectCVV">If set to <c>true</c>, the view controller will collect CVV information.</param>
		/// <param name="CollectZip">If set to <c>true</c>, the view controller will collect the customer's zip code.</param>
		public DispatchingCardIOViewController (string APIToken, bool CollectExpiry = true, bool CollectCVV = true, bool CollectZip = true)
			: base(new BlankCardIODelegate(), true)
		{
			if (string.IsNullOrWhiteSpace (APIToken))
				throw new ArgumentException ("A valid API Token must be provided.");

			this.AppToken = APIToken;
			this.CollectExpiry = CollectExpiry;
			this.CollectCVV = CollectCVV;
			this.CollectPostalCode = CollectZip;
			this.UseCardIOLogo = true;

			//Adding the custom, event-firing delegate object.
			Dispatcher = new CardIODispatcher ();
			Dispatcher.CardIORetrievalCanceled += HandleCardIORetrievalCanceled;
			Dispatcher.CardIORetrievalCompleted += HandleCardIORetrievalCompleted;
			this.PaymentDelegate = Dispatcher;

			ShowsFirstUseAlert = false;
			this.KeepStatusBarStyle = true;
			this.NavigationBarStyle = MonoTouch.UIKit.UIBarStyle.Default;
		}

		void HandleCardIORetrievalCompleted (CardIORetrievalCompletedEventArgs Arguments)
		{
			DismissViewController (true, delegate {
				if (CardIOCompleted != null)
					CardIOCompleted.Invoke (this, Arguments.CreditCardInfo);
			});
		}

		void HandleCardIORetrievalCanceled (CardIORetrievalCanceledEventArgs Arguments)
		{
			DismissViewController (true, delegate {
				if (CardIOCanceled != null)
					CardIOCanceled.Invoke (this);
			});
		}
		#endregion //Constructors
	}

	internal class BlankCardIODelegate : CardIOPaymentViewControllerDelegate {}
}

				