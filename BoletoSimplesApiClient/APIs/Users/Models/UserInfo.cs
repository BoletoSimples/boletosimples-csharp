using System;

namespace BoletoSimplesApiClient.APIs.Users.Models
{
    public class UserInfo
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
        public string AddressStreetName { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCityName { get; set; }
        public string AddressState { get; set; }
        public string BusinessName { get; set; }
        public string BusinessCnpj { get; set; }
        public string BusinessLegalName { get; set; }
    }
}
