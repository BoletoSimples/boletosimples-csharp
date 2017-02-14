using BoletoSimplesApiClient.Common;
using BoletoSimplesApiClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BoletoSimplesApiClient.APIs.BankBillets.Models
{
    /// <summary>
    /// Classe que representa o boleto
    /// </summary>
    [JsonRoot("bank_billet")]
    public class BankBillet
    {
        public int Id { get; set; }
        public DateTime ExpireAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ShortenUrl { get; set; }
        public string CustomerPersonType { get; set; }
        public string CustomerPersonName { get; set; }
        public string CustomerCnpjCpf { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerState { get; set; }
        public string CustomerNeighborhood { get; set; }
        public string CustomerZipcode { get; set; }
        public string CustomerAddressNumber { get; set; }
        public string CustomerAddressComplement { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public bool CreatedViaApi { get; set; }
        public string CustomerCityName { get; set; }
        [JsonConverter(typeof(BrazilianCurrencyJsonConverter))]
        public decimal PaidAmount { get; set; }
        [JsonConverter(typeof(BrazilianCurrencyJsonConverter))]
        public decimal Amount { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Formats { get; set; }
        public string Meta { get; set; }
        public decimal FineForDelay { get; set; }
        public decimal LatePaymentInterest { get; set; }
        public string Notes { get; set; }
        public decimal BankRate { get; set; }
        public int BankBilletAccountId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryCnpjCpf { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string BeneficiaryAssignorCode { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorCnpjCpf { get; set; }
        public string PaymentPlace { get; set; }
        public string Instructions { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        [JsonConverter(typeof(BrazilianCurrencyJsonConverter))]
        public decimal DocumentAmount { get; set; }
        /// <summary>
        /// Aceite 
        /// </summary>
        /// <value>S ou N</value>
        public char Acceptance { get; set; }
        public string ProcessedOurNumber { get; set; }
        public string ProcessedOurNumberRaw { get; set; }
        public string BankContractSlug { get; set; }
        public string AgencyNumber { get; set; }
        public string AgencyDigit { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDigit { get; set; }
        public string Extra1 { get; set; }
        public string Extra1Digit { get; set; }
        public string Extra2 { get; set; }
        public string Extra2Digit { get; set; }
        public string Line { get; set; }
        public string OurNumber { get; set; }
        public int? CustomerSubscriptionId { get; set; }
        public int? InstallmentNumber { get; set; }
        public int? InstallmentId { get; set; }
        public string CarneUrl { get; set; }
        public int? BankBilletLayoutId { get; set; }
        public int? RemittanceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string PaidBank { get; set; }
        public string PaidAgency { get; set; }
    }
}