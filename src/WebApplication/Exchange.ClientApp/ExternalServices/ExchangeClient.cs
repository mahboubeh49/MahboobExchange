using Exchange.ClientApp.ExternalServices.Interfaces;
using Exchange.ClientApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Exchange.ClientApp.ExternalServices
{
    public class ExchangeClient : IExchangeClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private IAccessToken _accessToken;
        
        public ExchangeClient(IHttpClientFactory httpClientFactory, IAccessToken accessToken)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ExchangeApi");
            _accessToken = accessToken;

        }
        public async Task<ResponseModel> ConvertAsync(ConvertModel convertModel)
        {
            if (!_accessToken.IsAccessTokenExist())
            {
                return new ResponseModel() { IsSuccess = false, ErrorCode = (int)HttpStatusCode.Unauthorized, ErrorMessage = "You are not authorized." };
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken.GetCurrentAccessToken());

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(convertModel)
                , Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/exchange", httpContent).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResponseModel() { IsSuccess = false, ErrorCode = (int)HttpStatusCode.Unauthorized, ErrorMessage = "You are not authorized." };
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel() { IsSuccess = false, ErrorCode = (int)response.StatusCode, ErrorMessage = content };
            }
            else
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
            }
            return new ResponseModel() { IsSuccess = true };
        }

        public async Task<HistoryResponse> GetHistoryAsync()
        {
            if (!_accessToken.IsAccessTokenExist())
            {
                return new HistoryResponse() { IsSuccess = false, ErrorCode = (int)HttpStatusCode.Unauthorized, ErrorMessage = "you are not authorized." };
            }

            var httpClient = _httpClientFactory.CreateClient("ExchangeApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken.GetCurrentAccessToken());

            var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/exchange");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new HistoryResponse() { IsSuccess = false, ErrorCode = (int)HttpStatusCode.Unauthorized, ErrorMessage = "you are not authorized." };
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return new HistoryResponse() { IsSuccess = false, ErrorCode = (int)response.StatusCode, ErrorMessage = content };
            }
            else
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var resultList = JsonConvert.DeserializeObject<List<ConversionHistoryResponse>>(content);

                return new HistoryResponse() { IsSuccess = true, Histories = resultList };
            }
        }
    }
}
