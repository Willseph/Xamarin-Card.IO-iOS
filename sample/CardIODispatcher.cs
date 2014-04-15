using System;
using CardIOSDK.iOS;

namespace CardIOSample.iOS
{
	/// <summary>
	/// An extension of the Card IO View Controller delegate class, which fires events after the user interacts with the Card IO features.
	/// </summary>
	internal class CardIODispatcher
		: CardIOPaymentViewControllerDelegate
	{
		#region Delegates
		internal delegate void CardIOEventHandler<T> (T Arguments) where T:CardIOGenericEventArgs;
		#endregion //Delegates

		#region Events
		/// <summary>
		/// Occurs after the user cancels the Card IO view controller.
		/// </summary>
		internal event CardIOEventHandler<CardIORetrievalCanceledEventArgs> CardIORetrievalCanceled;

		/// <summary>
		/// Occurs after the user completes the Card IO view controller.
		/// </summary>
		internal event CardIOEventHandler<CardIORetrievalCompletedEventArgs> CardIORetrievalCompleted;
		#endregion //Events

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="CardIOSDK.CardIODispatcher"/> class.
		/// </summary>
		internal CardIODispatcher()
			: base()
		{
		}
		#endregion //Constructor

		#region Methods
		#region Overrides
		/// <summary>
		/// Overrides the Card IO delegate method called after the user cancels the Card IO view controller.
		/// </summary>
		/// <param name="PaymentViewController">The Card IO payment view controller.</param>
		public override void UserDidCancelPaymentViewController (CardIOPaymentViewController PaymentViewController)
		{
			if(CardIORetrievalCanceled!=null)
				CardIORetrievalCanceled.Invoke (new CardIORetrievalCanceledEventArgs(PaymentViewController));
		}
		
		/// <summary>
		/// Overrides the Card IO delegate method called after the user completes the Card IO view controller.
		/// </summary>
		/// <param name="CreditCardInfo">The retrieved credit card information.</param>
		/// <param name="PaymentViewController">The Card IO payment view controller.</param>
		public override void InPaymentViewController (CardIOCreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
		{
			if ( CardIORetrievalCompleted != null )
				CardIORetrievalCompleted.Invoke (new CardIORetrievalCompletedEventArgs (paymentViewController, cardInfo));
		}
		#endregion //Overrides
		#endregion //Methods
	}

	#region Event argument classes
	/// <summary>
	/// An abstract event arguments class from which each concrete event arguments class is derived.
	/// </summary>
	public abstract class CardIOGenericEventArgs
		: EventArgs
	{
		/// <summary>
		/// The Card IO view controller that ultimately triggered the event.
		/// </summary>
		public CardIOPaymentViewController ViewController { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CardIOSDK.CardIOGenericEventArgs"/> class.
		/// </summary>
		/// <param name="ViewController">The Card IO view controller that ultimately triggered the event.</param>
		public CardIOGenericEventArgs( CardIOPaymentViewController ViewController )
		{
			this.ViewController = ViewController;
		}
	}

	/// <summary>
	/// Event arguments generated after the user cancels the Card IO view controller.
	/// </summary>
	public class CardIORetrievalCanceledEventArgs
		: CardIOGenericEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CardIOSDK.CardIORetrievalCanceledEventArgs"/> class.
		/// </summary>
		/// <param name="ViewController">The Card IO view controller that ultimately triggered the event.</param>
		public CardIORetrievalCanceledEventArgs(CardIOPaymentViewController ViewController)
			: base(ViewController)
		{
		}
	}

	/// <summary>
	/// Event arguments generated after the user completes the Card IO view controller.
	/// </summary>
	public class CardIORetrievalCompletedEventArgs
		: CardIOGenericEventArgs
	{
		/// <summary>
		/// The retrieved credit card information.
		/// </summary>
		public CardIOCreditCardInfo CreditCardInfo { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CardIOSDK.CardIORetrievalCompletedEventArgs"/> class.
		/// </summary>
		/// <param name="ViewController">The Card IO view controller that ultimately triggered the event.</param>
		/// <param name="CreditCardInfo">The retrieved credit card information.</param>
		public CardIORetrievalCompletedEventArgs(CardIOPaymentViewController ViewController, CardIOCreditCardInfo CreditCardInfo)
			: base(ViewController)
		{
			this.CreditCardInfo = CreditCardInfo;
		}
	}
	#endregion //Event argument classes
}

