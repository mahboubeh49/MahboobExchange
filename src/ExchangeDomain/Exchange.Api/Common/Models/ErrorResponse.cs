namespace Exchange.Api.Common.Models
{
    public class ErrorResponse
    {
        public string[] GeneralMessages { get; set; }

        public string DeveloperMessage { get; set; }
    }
}
