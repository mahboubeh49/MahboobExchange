using Exchange.ClientApp.Models;

namespace Exchange.ClientApp.ExternalServices.Interfaces
{
    public interface IExchangeClient
    {
        Task<HistoryResponse> GetHistoryAsync();
        Task<ResponseModel> ConvertAsync(ConvertModel searchModel);
    }
}
