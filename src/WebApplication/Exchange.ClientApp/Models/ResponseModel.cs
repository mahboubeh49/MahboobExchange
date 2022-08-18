namespace Exchange.ClientApp.Models
{
    public class ResponseModel
    {
        public bool IsSuccess{ get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
