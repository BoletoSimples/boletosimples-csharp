using BoletoSimplesApiClient.Utils;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.Common
{
    /// <summary>
    /// Representa o resultado de uma API com suporte a paginação, contem a request de erro e a 
    /// resposta em caso de sucesso
    /// </summary>
    /// <typeparam name="TSuccessResponse">Tipo do retorno de sucesso</typeparam>
    public class PagedApiResponse<TSuccessResponse> where TSuccessResponse : new()
    {
        public readonly bool IsSuccess;
        public readonly HttpStatusCode StatusCode;
        public readonly HttpResponseMessage ErrorResponse;

        private readonly ApiResponse<List<TSuccessResponse>> _apiResponse;
        private readonly HttpResponseMessage _response;

        public PagedApiResponse(HttpResponseMessage response)
        {
            _response = response;
            _apiResponse = new ApiResponse<List<TSuccessResponse>>(response);
            IsSuccess = _apiResponse.IsSuccess;
            StatusCode = _apiResponse.StatusCode;

            if (!IsSuccess)
                ErrorResponse = response;
        }

        public async Task<Paged<TSuccessResponse>> GeResponseAsync()
        {
            var pagingValues = PagedHeaderValues.ParseFromHttpMessage(_response);

            var items = new List<TSuccessResponse>();

            if (IsSuccess)
            {
                items = await _apiResponse.GeResponseAsync().ConfigureAwait(false);
            }

            return new Paged<TSuccessResponse>(pagingValues.Total,
                                        pagingValues.TotalPages,
                                        pagingValues.CurrentPage,
                                        pagingValues.MaxPageSize,
                                        items);
        }
    }
}