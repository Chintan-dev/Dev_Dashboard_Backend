namespace Dev_Dashboard.DTO
{
    public class CommonResponseModel
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = string.Empty;
        public object? Data { get; set; } = null;
        public CommonResponseModel(int StatusCode, bool Success, string? Message, object? Data)
        {
            this.StatusCode = StatusCode;
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
