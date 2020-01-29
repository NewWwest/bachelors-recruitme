using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.Enums
{
    /// <summary>
    /// Typ operacji w systemie Dotpay
    /// <br /><br/>
    /// W naszym systemie zazwyczaj to będzie <see cref="OperationType.Payment"/>
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Zwykła płatność
        /// </summary>
        [EnumMember(Value ="payment")]
        Payment,

        /// <summary>
        /// Płatność multi-merchant
        /// </summary>
        [EnumMember(Value = "payment_multimerchant_child")]
        PaymentMultiMerchantChild,

        /// <summary>
        /// Nadpłata multi-merchant
        /// </summary>
        [EnumMember(Value = "payment_multimerchant_parent")]
        PaymentMultiMerchantParent,

        /// <summary>
        /// Zwrot
        /// </summary>
        [EnumMember(Value = "refund")]
        Refund,

        /// <summary>
        /// Wypłata
        /// </summary>
        [EnumMember(Value = "payout")]
        Payout,

        /// <summary>
        /// Wypłata dowolnej kwoty
        /// </summary>
        [EnumMember(Value = "payout_any_amount")]
        PayoutAnyAmount,

        /// <summary>
        /// Zwolnienie rollbacka
        /// </summary>
        [EnumMember(Value = "release_rollback")]
        ReleaseRollback,

        /// <summary>
        /// Płatność niezidentyfikowana
        /// </summary>
        [EnumMember(Value = "unidentified_payment")]
        UnidentifiedPayment,

        /// <summary>
        /// Reklamacja
        /// </summary>
        [EnumMember(Value = "complaint")]
        Complaint,

        /// <summary>
        /// Rejestracja karty
        /// </summary>
        [EnumMember(Value = "credit_card_registration")]
        CreditCardRegistration,

        /// <summary>
        /// Prowizja od wypłaty
        /// </summary>
        [EnumMember(Value = "payout_commission")]
        PayoutCommission
    }
}
