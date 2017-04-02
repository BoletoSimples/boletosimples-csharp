using BoletoSimplesApiClient.Common;
using Newtonsoft.Json;

namespace BoletoSimplesApiClient.APIs.Customers.Models
{

    [JsonRoot("customer")]
    public class Customer
    {
        /// <summary>
        /// ID do cliente
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nome Completo ou Razão Social
        /// </summary>
        public string PersonName { get; private set; }

        /// <summary>
        /// Tipo de Cliente (Pessoa Física ou Pessoa Jurídica
        /// </summary>
        public string PersonType { get; set; }

        /// <summary>
        /// CNPJ/CPF(formato 999.999.999-99 ou 99.999.999/9999-99)
        /// </summary>
        public string CnpjCpf { get; set; }

        /// <summary>
        /// CEP(formato 99999999)
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// Endereço
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Estado(sigla do estado, Ex: RJ)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string Neighborhood { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public string AddressNumber { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string AddressComplement { get; set; }

        /// <summary>
        /// Telefone(formato 9988888888)
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// DDD do Celular
        /// </summary>
        public string MobileLocalCode { get; set; }

        /// <summary>
        /// Celular
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Anotações
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// E-mail alternativo
        /// </summary>
        public string EmailCc { get; set; }

        /// <summary>
        /// Enviado pela API
        /// </summary>
        public bool CreatedViaApi { get; set; }

        [JsonConstructor]
        public Customer(int id, string personName, string cnpjCpf, string zipcode, string address, string cityName, string state, string neighborhood) :
            this(personName, cnpjCpf, zipcode, address, cityName, state, neighborhood)
        {
            Id = id;
        }

        public Customer(string personName, string cnpjCpf, string zipcode, string address, string cityName, string state, string neighborhood)
        {
            PersonName = personName;
            CnpjCpf = cnpjCpf;
            Zipcode = zipcode;
            Address = address;
            CityName = cityName;
            State = state;
            Neighborhood = neighborhood;
        }
    }
}
