namespace Exchange.ClientApp.ExternalServices.Interfaces
{
    public interface IAccessToken
    {
        Task<string> GetNewAccessTokenAsync(string username, string password);

        bool IsAccessTokenExist();
        string GetCurrentAccessToken();
        void ForgetAccessToken();
    }
}
