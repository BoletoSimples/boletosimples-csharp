using BoletoSimplesApiClient.Common;
using System;

namespace BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels
{
    [JsonRoot("bank_billet_account")]
    public class BankBilletAccount
    {
        public int Id { get; set; }
        public string BankContractSlug { get; set; }
        public string NextOurNumber { get; set; }
        public string AgencyNumber { get; set; }
        public string AgencyDigit { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDigit { get; set; }
        public string Extra1 { get; set; }
        public string Extra1Digit { get; set; }
        public string Extra2 { get; set; }
        public string Extra2Digit { get; set; }
        public string Extra3 { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryCnpjCpf { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? HomologatedAt { get; set; }
        public string NextRemittanceNumber { get; set; }
        public bool Default { get; set; }
        public string Configuration { get; set; }
        public BankContract BankContract { get; set; }
    }

    public class BankContract
    {
        public Bank Bank { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Sufix { get; set; }
        public object Variation { get; set; }
    }

    public class Bank
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}