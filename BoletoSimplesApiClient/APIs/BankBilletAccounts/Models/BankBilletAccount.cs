using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels
{
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
        public object Extra1Digit { get; set; }
        public object Extra2 { get; set; }
        public object Extra2Digit { get; set; }
        public object Extra3 { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryCnpjCpf { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
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