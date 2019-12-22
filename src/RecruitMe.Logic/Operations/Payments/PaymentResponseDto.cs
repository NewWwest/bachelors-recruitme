using Newtonsoft.Json;
using RecruitMe.Logic.Operations.Payments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments
{
    public class PaymentResponseDto
    {
        /// <summary>
        /// Id płatności w systemie Dotpay
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Numer operacji w systemie Dotpay
        /// </summary>
        [JsonProperty("operation_number")]
        public string Number { get; set; }

        /// <summary>
        /// Typ operacji w systemie Dotpay
        /// </summary>
        [JsonProperty("operation_type")]
        public OperationType Type { get; set; }

        /// <summary>
        /// Status operacji
        /// </summary>
        [JsonProperty("operation_status")]
        public OperationStatus Status { get; set; }

        /// <summary>
        /// Kwota operacji zaksięgowanej w panelu Dotpay.
        /// </summary>
        [JsonProperty("operation_amount")]
        public decimal OperationAmount { get; set; }

        /// <summary>
        /// Waluta określająca <see cref="PaymentResponseDto.OperationAmount"/>, format zgodny ze standardem standardem ISO 4217. 
        /// </summary>
        [JsonProperty("operation_currency")]
        public string OperationCurrency { get; set; }

        /// <summary>
        /// Kwota operacji (transakcji) pobrana z parametru <i>amount</i> jaki został przesłany przez serwis sprzedawcy 
        /// w przekierowaniu Kupującego do serwisu Dotpay. 
        /// </summary>
        [JsonProperty("operation_original_amount")]
        public string OperationOriginalAmount { get; set; }

        /// <summary>
        /// Waluta operacji (transakcji) pobrana z parametru <i>currency</i> jaki został przesłany przez serwis sprzedawcy
        /// w przekierowaniu Kupującego do serwisu Dotpay, format zgodny ze standardem ISO 4217.
        /// </summary>
        [JsonProperty("operation_original_currency")]
        public string OperationOriginalCurrency { get; set; }

        /// <summary>
        /// Data realizacji operacji (transakcji) lub zmiany statusu operacji.
        /// </summary>
        [JsonProperty("operation_datetime")]
        public DateTime OperationDatetime { get; set; }
        
        /// <summary>
        /// Paramentr kontrolny, taki sam jak w linku do płatności.
        /// </summary>
        [JsonProperty("control")]
        public string Control { get; set; }
        
        /// <summary>
        /// Opis transakcji.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Adres e mail podany przez osobę dokonującą płatność.
        /// </summary>
        [JsonProperty("email")]
        public string PayerEmail { get; set; }
        
        /// <summary>
        /// Nazwa odbiorcy płatności.
        /// </summary>
        [JsonProperty("p_info")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Adres e-mail w celu kontaktu ze sprzedawcą.
        /// </summary>
        [JsonProperty("p_email")]
        public string DisplayEmail { get; set; }

        /// <summary>
        /// Kanał płatności, jakim została wykonana operacja (transakcja).
        /// </summary>
        [JsonProperty("channel")]
        public int SalesChannel { get; set; }

        /// <summary>
        /// Suma kontrolna będąca wynikiem działania funkcji skrótu.
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
