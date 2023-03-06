namespace BucklesChatBackend.API
{
    public class ApiErrorInfo
    {
        public ApiErrorInfo(string id, ApiErrorCodes code, Exception ex)
        {
            Id = id;
            Message = ex.Message;
            Stack = ex.StackTrace;
            Code = code;
        }

        public ApiErrorInfo(string id, Exception ex)
        {
            Id = id;
            Message = ex.Message;
            Stack = ex.StackTrace;
        }

        public ApiErrorInfo(Exception ex)
        {
            Message = ex.Message;
            Stack = ex.StackTrace;
        }

        public ApiErrorInfo(string id)
        {
            Id = id;
        }

        public ApiErrorInfo(ApiErrorCodes code)
        {
            Code = code;
        }

        public string Id { get; set; }
        public string? Message { get; set; }
        public ApiErrorCodes? Code { get; set; }
        public string? Stack { get; set; }
    }
}
