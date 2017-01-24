using BoletoSimplesApiClient.APIs.Auth.ResponseMessages;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Auth
{
    public class AuthApi
    {
        private const string tokenApiUrl = "";
        private const string oauthApiUrl = "";
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;

        public AuthApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /* Needs Implements OAuth workflow 
        public async Task<ApiResponse<UserInfoResponseMessage>> GetOAuthToken()
        {
            var request = _requestBuilder.To(new Uri("http://locahost"))
                                         .WithMethod(HttpMethod.Get)
                                         .AndOptionalContent(new StringContent(""))
                                         .Build();

            return await _client.SendAsync<UserInfoResponseMessage>(request);
        }
        */


        public async Task<ApiResponse<UserInfoResponseMessage>> GetUserInfoAsync()
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), "/userinfo")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<UserInfoResponseMessage>(request);
        }

    }
}
