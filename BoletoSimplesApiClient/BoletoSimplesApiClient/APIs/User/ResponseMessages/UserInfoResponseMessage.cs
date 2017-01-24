using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Auth.ResponseMessages
{
    public class UserInfoResponseMessage
    {
        public int Id { get; set; }
        public string LoginUrl { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public int AccountLevel { get; set; }
        public string PhoneNumber { get; set; }
        public object AddressStreetName { get; set; }
        public object AddressNumber { get; set; }
        public object AddressComplement { get; set; }
        public object AddressNeighborhood { get; set; }
        public object AddressPostalCode { get; set; }
        public object AddressCityName { get; set; }
        public object AddressState { get; set; }
        public object BusinessName { get; set; }
        public object BusinessCnpj { get; set; }
        public object BusinessLegalName { get; set; }
    }
}
