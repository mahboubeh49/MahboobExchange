using Exchange.ClientApp.ExternalServices.Interfaces;
using IdentityModel.Client;

namespace Exchange.ClientApp.ExternalServices
{
    public class AccessToken : IAccessToken
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccessToken(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException("accessor");
        }
        public async Task<string> GetNewAccessTokenAsync(string username, string password)
        {
            var httpClient = new HttpClient();
            var identityServerResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "https://localhost:7083/connect/token",

                ClientId = "ExchangeKnabAdmin",
                ClientSecret = "PasswordSecret",
                Scope = "WriteApis",

                UserName = username,
                Password = password
            });

            //all good?
            if (!identityServerResponse.IsError)
            {
                CookieOptions option = new()
                {
                    MaxAge = TimeSpan.FromDays(100),
                    Secure = true
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", identityServerResponse.AccessToken, option);
                return identityServerResponse.AccessToken;
            }
            else
            {
                return String.Empty;
            }

        }

        public bool IsAccessTokenExist()
        {
            string accessToken = _httpContextAccessor.HttpContext.Request.Cookies["access_token"];
            return !string.IsNullOrEmpty(accessToken);
        }

        public string GetCurrentAccessToken()
        {
            string accessToken = _httpContextAccessor.HttpContext.Request.Cookies["access_token"];
            return !string.IsNullOrWhiteSpace(accessToken) ? accessToken : string.Empty;
        }

        public void ForgetAccessToken()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["access_token"] != null)
            {
                var siteCookies = _httpContextAccessor.HttpContext.Request.Cookies.Where(c => c.Key.StartsWith("access_token"));
                foreach (var cookie in siteCookies)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
                }
            }
        }
    }
}
