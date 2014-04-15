Card.IO Xamarin Binding for iOS
=============

A Xamarin.iOS binding for the Card.IO library. You can view the library here:
[https://github.com/card-io/card.io-iOS-SDK](https://github.com/card-io/card.io-iOS-SDK)

This solution contains both the binding project as well as a sample project.

Sample Project and Dispatchers
-------------

The sample project contains two special classes:

* **`CardIODispatcher`**
* **`DispatchingCardIOViewController`**

**`CardIODispatcher`** is an extension of the bound protocol class `CardIOPaymentViewControllerDelegate` which invokes events when the two main methods of the delegate are invoked (*canceled* and *completed*).

**`DispatchingCardIOViewController`** is an extension of the bound class `CardIOPaymentViewController` that uses the previously-mentioned dispatching protocol as its delegate.

Using these classes together make it very simple to subscribe to the *canceled* and *completed* events. You can see this in the **MainViewController** class when the button is tapped:

	void HandleStartButtonTapped (object sender, EventArgs e)
	{
		DispatchingCardIOViewController CardIOVC = new DispatchingCardIOViewController (Constants.CardIOAppToken);

		CardIOVC.CardIOCanceled += delegate(DispatchingCardIOViewController ViewController) {
			new UIAlertView("Canceled",
				"The Card.IO operation was canceled.",
				null,
				"OK").Show();
		};

		CardIOVC.CardIOCompleted += delegate(DispatchingCardIOViewController ViewController, CardIOCreditCardInfo CreditCardInfo) {
			string Message = string.Format("{0}: {1}",
				CreditCardInfo.CardType.ToString(), 
				CreditCardInfo.CardNumber);
			new UIAlertView("Complete",
				Message,
				null,
				"OK").Show();
		};

		PresentViewController (CardIOVC, true, () => {});
	}

Rather than having to create your own custom delegate to handle these protocol methods in your view controller, these two classes handle it for you. Also, the **`DispatchingCardIOViewController`** class contains a constructor that allows you to provide your [Card.IO API token](https://www.card.io/accounts/register/developer).

Of course, if you want to create your own custom `CardIOPaymentViewControllerDelegate` protocol class, you're able to.