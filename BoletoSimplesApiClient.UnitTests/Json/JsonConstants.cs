namespace BoletoSimplesApiClient.UnitTests.Json
{
    public static class JsonConstants
    {
        public const string BankBilletAccount = @"{'bank_contract_slug':'sicoob-02', 'next_our_number':'1','agency_number': '4327','agency_digit': '3','account_number': '3666','account_digit': '8',
                                                       'extra1':'1234567','extra1_digit':'222','extra2':'1234567','extra2_digit':'222','extra3':'30 digits','beneficiary_name':'boleto simples cobranças ltda.',
                                                       'beneficiary_cnpj_cpf':'05.813.794/0001-26','beneficiary_address':'av. presidente vargas, 633 sala 1716. rio de janeiro - rj', 'name':'bancoob/sicoob 02 - cc 00003666-8',
                                                       'status':'', 'homologated_at':'2017-01-31 00:00:00', 'next_remittance_number':'1', 'configuration':'Any Configuration', 'bank_contract':{ 'bank':{'code':'sicoob',
                                                       'name':'bancoob/sicoob','number':'756'},'slug':'sicoob-02', 'code':'02','sufix':'02','variation':null,'name':'1/02 - simples sem registro'}}";

        public const string BankBillet = @"{'amount': '1.345,56', 'expire_at': '2030-01-31 00:00:00','description': 'Any Desc','customer_person_name': 'Test Billet', 'customer_cnpj_cpf': '45082419480','customer_zipcode': 12345678,
                                            'customer_email': 'anyemmail@gmail.com','customer_address': 'Any Street','customer_city_name': 'Rio de Janeiro','customer_state':'RJ','customer_neighborhood':'Glória',
                                            'customer_address_number': '100','customer_address_complement': 'Sl 1001','customer_phone_number': '21999999999','meta':'{ pedido: 10 }', 'status': 'Possible State', 
                                            'paid_at': '2030-01-31 00:00:00','paid_amount': '10.00', 'shorten_url': 'http://test', 'url': 'http://test', 'carne_url': 'http://test', 'formats': '','created_via_api': true,
                                            'fine_for_delay': 1.0,'late_payment_interest': 2.0,'guarantor_name': 'Name','guarantor_cnpj_cpf':'450.824.194-80','payment_place': 'Local','instructions': 'Instruções para o Caixa',
                                            'document_date':'2030-01-31 00:00:00','document_type': '','document_number': 99999999,'document_amount': '1.345,56','acceptance':'N','remittance_id': 999,'notes': 'Anotações','paid_bank':'Banco de Pagamento',
                                            'paid_agency': 'Agência de Pagamento'}";

        public const string Discharge = @"{'id':132,'filename':'arquivo-test.ret','processed_at':null,'created_via_api':true,'status':'unprocessed','bank_billet_account_id':null,'created_via_integration':null}";

        public const string Remittance = @"{ 'filename' : '1605061.REM', 'created_via_api' : true, 'status' : 'processed','bank_billet_account_id' : 1, 'created_at' : '2016-05-06', 'processed_at' : '2016-05-06', 'url' : 'https://sandbox.boletosimples.com.br/remessas/06tt1bcc3f6132720866b53a57c76de4/download', 'id' : 1, 'bank_billet_ids' : [1] }";

        public const string Installment = @"{'id':1,'amount':1120.4,'cycle':'monthly','start_at':'2016-09-15','end_at':'2016-11-16','instructions':null,'customer_id':11,'description':'Hospedagem','created_at':'2016-08-15','updated_at':'2016-08-15','created_via_api':true,'total':3,'bank_billet_account_id':12,'status':'created','fine_for_delay': 0.0,'late_payment_interest':0.0}";

        public const string CurstomerSubscription = @"{'id':1,'amount':1120.4,'cycle':'monthly','next_billing':'2017-01-01','end_at':'2016-11-16','instructions':null,'customer_id':11,'description':'Hospedagem','bank_billet_account_id':12,'fine_for_delay': 0.0,'late_payment_interest':0.0}";

        public const string Event = @"{'id': 224,'code': 'customer.updated','data': {  'object': {'id': 67,'city_name': 'Rio de Janeiro','person_name': 'Joao da Silva','address': 'Rua quinhentos',
                                       'address_complement': 'Sala 4','address_number': '111','mobile_number': '','cnpj_cpf': '782.661.177-64','email': 'novo@example.com','neighborhood': 'bairro',
                                       'person_type': 'individual','phone_number': '2112123434','zipcode': '12312-123','mobile_local_code': '','state': 'RJ','created_via_api': true  }, 
                                       'changes': {'email': [  'antigo@example.com',  'novo@example.com'],'mobile_local_code': [  null,  ''],'mobile_number': [  null,  ''],
                                       'updated_at': [  '2015-03-08 19:27:36 -0300',  '2015-03-17 21:37:53 -0300']  }},'occurred_at': '2015-03-17T21:37:53.000-03:00' }";

        public const string Customer = @"{'person_name':'Nome do Cliente', 'cnpj_cpf': '125.812.717-28', 'zipcode': '20071004', 'address': 'Rua quinhentos', 'city_name': 'Rio de Janeiro', 'state': 'RJ', 'neighborhood': 'bairro'}";
    }
}
