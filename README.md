![Boleto Simples Logo](http://api.boletosimples.com.br/img/logo.png)

# BoletoSimples C# Client
Client de acesso escrito em C# para api rest do [www.boletosimples.com.br](http://api.boletosimples.com.br/)

 Build | Nuget | Coverage %
--------------|-------|-----------
|![Build status](https://ci.appveyor.com/api/projects/status/l333hif1lnf1d2lo/branch/master?svg=true) | [![NuGet version](https://badge.fury.io/nu/BoletoSimples-Client.svg)](https://badge.fury.io/nu/BoletoSimples-Client) | [![Coverage Status](https://coveralls.io/repos/github/BoletoSimples/boletosimples-csharp/badge.svg?branch=master)](https://coveralls.io/github/BoletoSimples/boletosimples-csharp?branch=master)

### Instalação

No visual Studio > Abrir o **Package Manager Console** e digitar o comando abaixo:

```powershell
> Install-Package BoletoSimples-Client
```

### Opções de Configuração

1. Configuração por arquivo de configuração - Em seu arquivo de configuração (.config), é necessário adicionar o seu **Token de Acesso** e as outras informações da API de acesso do BoletoSimples, encontrado no seu painel administrativo do BoletoSimples [nesse link](https://boletosimples.com.br/conta/api/tokens) ou em seu painel vá ao menu *Integrações* > *Aba Tokens de Acesso*.

```xml
<appSettings>
    <!--BoletoSimples informações básicas de acesso -->
    <add key="boletosimple-api-version" value="v1" />
    <!-- Poderia ser o ambiente de sandbox https://sandbox.boletosimples.com.br/api -->
    <add key="boletosimple-api-url" value="https://boletosimples.com.br/api" />
    <add key="boletosimple-useragent" value="Meu e-Commerce (meuecommerce@example.com)" />

    <!--BoletoSimples token de acesso (http://api.boletosimples.com.br/authentication/token/) -->
    <add key="boletosimple-api-token" value="Seu Token de acessos" />

    <!--BoletoSimples dados de acesso para Oauth2 (http://api.boletosimples.com.br/authentication/oauth2/)
        Não suportado na versão atual do client -->
    <add key="boletosimple-api-return-url" value="" />
    <add key="boletosimple-api-client-id" value="" />
    <add key="boletosimple-api-client-secret" value=""/>
 </appSettings>
```

2. Configuração ByCode - Essa configuração permite que seus dados venham de uma fonte diferente do seu arquivo de configuração da sua escolha, para maior segurança dos dados por exemplo.
```csharp
public void AnyMethod
{
    var myConnection = new ClientConnection("boletosimple-api-url",
                                            "boletosimple-api-version",
                                            "boletosimple-api-token",
                                            "boletosimple-useragent",
                                            "boletosimple-api-return-url",
                                            "boletosimple-api-client-id",
                                            "boletosimple-api-client-secret");

    var client = new BoletoSimplesClient(myConnection);
}

```

### Exemplos de Utilização

```csharp
public class AnyClass
{
    // Obter as carteiras de cobrança
    public async Task GetAllBankBilletAccounts()
    {
        using(var client = new BoletoSimplesClient())
        {
           // pagedResponse contém a resposta de erro e o conteudo no caso de sucesso
           var pagedResponse = await client.BankBilletAccounts.GetAsync(0, 250).ConfigureAwait(false);

           // Aqui é apenas a resposta de sucesso sem as informações adicionais da resposta anterior
           var pagedContent = await pagedResponse.GetSuccessResponseAsync().ConfigureAwait(false);
        }
    }

    // Obter as informação do usuário
    public async Task GetUserInfo()
    {
        var client = new BoletoSimplesClient();
        var response = await client.Auth.GetUserInfoAsync().ConfigureAwait(false);
        var successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);
        client.Dispose();
    }

    // Criar uma carteira de cobrança
    public async Task CreateBankBilletAccount()
    {
        using (var client = new BoletoSimplesClient())
        {
            var response = await client.BankBilletAccounts.PostAsync(new BankBilletAccount()).ConfigureAwait(false);
            var successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);
        }
    }

     // Criar um boleto
    public async Task CreateBankBilletAccount()
    {
        using (var client = new BoletoSimplesClient())
        {
            var response = await client.BankBillets.PostAsync(new BankBillet()).ConfigureAwait(false);
            var successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);
        }
    }
}
```

### Apis Suportadas até o momento
* **Informações do Usuário**
* **Carteiras de Clientes**
* **Boletos**
* **Arquivo CNAB - Remessa**
* **Arquivo CNAB - Retorno**

--------------------
### Outras Informações
* [Boleto Simples Documentação Oficial](http://api.boletosimples.com.br/)
* Para mais exemplos de utilização veja o **[projeto de testes integrados](https://github.com/BoletoSimples/boletosimples-csharp/tree/master/BoletoSimplesApiClient/BoletoSimplesApiClient.IntegratedTests)**


### Bugs, Dúvidas, Issues, Agradecimentos, etc

Através do [issue tracker do GitHub](http://github.com/BoletoSimples/boletosimples-csharp/issues)

### Autor

[**Rodrigo Couto**](http://github.com/rscouto) trabalhando na [Plataforma Edux](http://www.sistemaedux.com.br)

### PRs são muito bem-vindas! :)
