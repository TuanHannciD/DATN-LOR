namespace AuthDemo.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(string message, T? data = default)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> FailResponse(string code, string message, T? data = default)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Code = code,
                Message = message,
                Data = data
            };
        }
    }
}
