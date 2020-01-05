using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.Enums
{
    /// <summary>
    /// Możliwe błędy w transakcji po powrocie do nas.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Przekroczona data ważności linku płatniczego lub przekazana w parametrze <i>expiration_date</i>
        /// </summary>
        [EnumMember(Value = "PAYMENT_EXPIRED")]
        PaymentExpired,

        /// <summary>
        /// Nieprawidłowa wartość parametru <i>channel</i>
        /// </summary>
        [EnumMember(Value = "UNKNOWN_CHANNEL")]
        UnknownChannel,

        /// <summary>
        /// Wskazany kanał płatności jest niedostępny
        /// </summary>
        [EnumMember(Value = "DISABLED_CHANNEL")]
        DisabledChannel,

        /// <summary>
        /// Kod waluty jest nieprawidłowy
        /// </summary>
        [EnumMember(Value = "UNKNOWN_CURRENCY")]
        UnknownCurrency,

        /// <summary>
        /// Zablokowane konto Dotpay
        /// </summary>
        [EnumMember(Value = "BLOCKED_ACCOUNT")]
        BlockedAccount,

        /// <summary>
        /// Nieaktywne konto Dotpay
        /// </summary>
        [EnumMember(Value = "INACTIVE_SELLER")]
        InactiveSeller,

        /// <summary>
        /// Kwota jest mniejsza niż minimum określone dla sklepu
        /// </summary>
        [EnumMember(Value = "AMOUNT_TOO_LOW")]
        AmountTooLow,

        /// <summary>
        /// Kwota jest większa niż maksimum określone dla sklepu
        /// </summary>
        [EnumMember(Value = "AMOUNT_TOO_HIGH")]
        AmountTooHigh,

        /// <summary>
        /// Przesłane dane posiadają nieprawidłowy format
        /// </summary>
        [EnumMember(Value = "BAD_DATA_FORMAT")]
        BadDataFormat,

        /// <summary>
        /// Ustawienia konta w Dotpay wymagają by adres URLC zawierał protokół SSL 
        /// </summary>
        [EnumMember(Value = "URLC_INVALID")]
        UrlcInvalid,

        /// <summary>
        /// Brak jednego z wymaganych parametrów
        /// </summary>
        [EnumMember(Value = "REQUIRED_PARAMETERS_NOT_PRESENT")]
        RequiredParametersNotPresent,

        // MultiMerchants are ommitted

        /// <summary>
        /// Przesłano dane kartowe, ale konfiguracja konta nie pozwala na ich przetwarzanie
        /// </summary>
        [EnumMember(Value = "CREDIT_CARD_NOT_ACCEPTED")]
        CreditCardNotAccepted,

        /// <summary>
        /// Wartość zwracana w przeciwnym przypadku
        /// </summary>
        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError,
    }
}
