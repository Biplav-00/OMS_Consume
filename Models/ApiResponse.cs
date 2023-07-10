namespace ConsumeOMS.Models
{
    public class ApiResponse<T>
    {
        public bool isSuccess { get; set; }
        public List<T> value { get; set; } = new List<T>();
        public string message { get; set; } = string.Empty;
        public int errorCode { get; set; } = 200;
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalCount { get; set; }
        public bool hasPrevious { get; set; }
        public bool hasNext { get; set; }
        public IEnumerable<T> data { get; set; } = Enumerable.Empty<T>();
        public static ApiResponse<T> AsError(int code)
        {
            ApiErrors apiErrors = new ApiErrors();

            return new ApiResponse<T>()
            {
                isSuccess = (code == 200),
                errorCode = code,
                message = apiErrors.GetStatusCodeMessage(code)
            };
        }
        public static ApiResponse<T> AsError(int code, string customMessage)
        {
            return new ApiResponse<T>()
            {
                isSuccess = (code == 200),
                errorCode = code,
                message = customMessage
            };
        }
        public static ApiResponse<T> AsValue(T data)
        {
            ApiResponse<T> response = new ApiResponse<T>();

            response.value.Add(data);
            response.isSuccess = true;
            return response;
        }
        public static ApiResponse<T> AsValue(T data, string customMessage)
        {
            ApiResponse<T> response = new ApiResponse<T>();

            response.value.Add(data);
            response.isSuccess = true;
            response.message = customMessage;

            return response;
        }
    }
}
