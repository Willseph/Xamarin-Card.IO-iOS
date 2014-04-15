using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CardIOSDK.iOS
{
	[Model, BaseType (typeof (NSObject))]
	[Protocol]
	public partial interface CardIOPaymentViewControllerDelegate {

		[Export ("userDidCancelPaymentViewController:")]
		void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController);

		[Export ("userDidProvideCreditCardInfo:inPaymentViewController:")]
		void InPaymentViewController (CardIOCreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController);
	}

	[BaseType (typeof (UINavigationController))]
	public partial interface CardIOPaymentViewController {

		[Export ("initWithPaymentDelegate:")]
		IntPtr Constructor (CardIOPaymentViewControllerDelegate aDelegate);

		[Export ("initWithPaymentDelegate:scanningEnabled:")]
		IntPtr Constructor (CardIOPaymentViewControllerDelegate aDelegate, bool scanningEnabled);

		[Export ("appToken", ArgumentSemantic.Copy)]
		string AppToken { get; set; }

		[Export ("languageOrLocale", ArgumentSemantic.Copy)]
		string LanguageOrLocale { get; set; }

		[Export ("keepStatusBarStyle")]
		bool KeepStatusBarStyle { get; set; }

		[Export ("navigationBarStyle")]
		UIBarStyle NavigationBarStyle { get; set; }

		[Export ("navigationBarTintColor", ArgumentSemantic.Retain)]
		UIColor NavigationBarTintColor { get; set; }

		[Export ("disableBlurWhenBackgrounding")]
		bool DisableBlurWhenBackgrounding { get; set; }

		[Export ("guideColor", ArgumentSemantic.Retain)]
		UIColor GuideColor { get; set; }

		[Export ("suppressScanConfirmation")]
		bool SuppressScanConfirmation { get; set; }

		[Export ("collectExpiry")]
		bool CollectExpiry { get; set; }

		[Export ("collectCVV")]
		bool CollectCVV { get; set; }

		[Export ("collectPostalCode")]
		bool CollectPostalCode { get; set; }

		[Export ("useCardIOLogo")]
		bool UseCardIOLogo { get; set; }

		[Export ("disableManualEntryButtons")]
		bool DisableManualEntryButtons { get; set; }

		[Export ("paymentDelegate", ArgumentSemantic.Assign)]
		CardIOPaymentViewControllerDelegate PaymentDelegate { get; set; }

		[Static, Export ("canReadCardWithCamera") ]
		bool CanReadCardWithCamera { get; }

		[Static, Export ("libraryVersion")]
		string LibraryVersion { get; }

		[Export ("showsFirstUseAlert")]
		bool ShowsFirstUseAlert { get; set; }
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface CardIOViewDelegate {

		[Export ("cardIOView:didScanCard:")]
		void DidScanCard (CardIOView cardIOView, CardIOCreditCardInfo cardInfo);
	}

	[BaseType (typeof (UIView))]
	public partial interface CardIOView {

		[Static, Export ("canReadCardWithCamera")]
		bool CanReadCardWithCamera { get; }

		[Export ("appToken", ArgumentSemantic.Copy)]
		string AppToken { get; set; }

		[Export ("delegate", ArgumentSemantic.Retain)]
		CardIOViewDelegate Delegate { get; set; }

		[Export ("languageOrLocale", ArgumentSemantic.Copy)]
		string LanguageOrLocale { get; set; }

		[Export ("guideColor", ArgumentSemantic.Retain)]
		UIColor GuideColor { get; set; }

		[Export ("useCardIOLogo")]
		bool UseCardIOLogo { get; set; }

		[Export ("scannedImageDuration")]
		float ScannedImageDuration { get; set; }

		[Export ("cameraPreviewFrame", ArgumentSemantic.Assign)]
		RectangleF CameraPreviewFrame { get; }
	}

	[BaseType (typeof (NSObject))]
	public partial interface CardIOCreditCardInfo {

		[Export ("cardNumber", ArgumentSemantic.Copy)]
		string CardNumber { get; set; }

		[Export ("redactedCardNumber", ArgumentSemantic.Copy)]
		string RedactedCardNumber { get; }

		[Export ("expiryMonth")]
		uint ExpiryMonth { get; set; }

		[Export ("expiryYear")]
		uint ExpiryYear { get; set; }

		[Export ("cvv", ArgumentSemantic.Copy)]
		string Cvv { get; set; }

		[Export ("postalCode", ArgumentSemantic.Copy)]
		string PostalCode { get; set; }

		[Export ("scanned")]
		bool Scanned { get; set; }

		[Export ("cardType")]
		CardIOCreditCardType CardType { get; }

		[Static, Export ("displayStringForCardType:usingLanguageOrLocale:")]
		string DisplayStringForCardType (CardIOCreditCardType cardType, string languageOrLocale);
	}
}

