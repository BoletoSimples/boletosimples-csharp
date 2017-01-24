using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Auth.ResponseMessages
{
    public class UserInfoResponseMessage
    {
        public int id { get; set; }
        public string login_url { get; set; }
        public string email { get; set; }
        public object account_type { get; set; }
        public object first_name { get; set; }
        public object middle_name { get; set; }
        public object last_name { get; set; }
        public object full_name { get; set; }
        public object cpf { get; set; }
        public object date_of_birth { get; set; }
        public object mother_name { get; set; }
        public object father_name { get; set; }
        public int account_level { get; set; }
        public object phone_number { get; set; }
        public object address_street_name { get; set; }
        public object address_number { get; set; }
        public object address_complement { get; set; }
        public object address_neighborhood { get; set; }
        public object address_postal_code { get; set; }
        public object address_city_name { get; set; }
        public object address_state { get; set; }
        public object business_name { get; set; }
        public object business_cnpj { get; set; }
        public object business_legal_name { get; set; }
    }
}
