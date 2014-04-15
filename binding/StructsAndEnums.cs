﻿using System;

namespace CardIOSDK.iOS
{
	public enum CardIOCreditCardType {
		Unknown = 0,
		Unrecognized = 0,
		Ambiguous = 1,
		Amex = 51 /* '3' */,
		JCB = 74 /* 'J' */,
		Visa = 52 /* '4' */,
		Mastercard = 53 /* '5' */,
		Discover = 54 /* '6' */
	}
}

