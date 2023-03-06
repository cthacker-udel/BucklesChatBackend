namespace BucklesChatBackend.API
{
    public class ApiErrorInfo
    {
        public ApiErrorInfo(long id, ApiErrorCodes code, Exception ex)
        {
            Id = id;
            Message = ex.Message;
            Stack = ex.StackTrace;
            Code = code;
        }

        public ApiErrorInfo(long id, Exception ex)
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

        public ApiErrorInfo(long id)
        {
            Id = id;
        }

        public ApiErrorInfo(ApiErrorCodes code)
        {
            Code = code;
        }

        public long Id { get; set; }
        public string? Message { get; set; }
        public ApiErrorCodes? Code { get; set; }
        public string? Stack { get; set; }
    }
}
