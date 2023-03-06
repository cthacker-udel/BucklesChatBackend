namespace BucklesChatBackend.API
{
    public class ApiResponse
    {

        public ApiResponse(string id, object data, ApiErrorInfo info)
        {
            Id = id;
            Data = data;
            ApiError = info;
        }

        public ApiResponse(object data)
        {
            Data = data;
        }

        public ApiResponse(string id, object data)
        {
            Id = id;
            Data = data;
        }

        public ApiResponse(string id, ApiErrorInfo info)
        {
            Id = id;
            ApiError = info;
        }

        public ApiResponse(string id, Exception exception)
        {
            Id = id;
            ApiError = new ApiErrorInfo(exception);
        }


        public object? Data { get; set; }

        public ApiErrorInfo? ApiError { get; set; }

        public string? Id { get; set; }
    }
}
