
using Domain.Enums;

namespace Presentation.ViewModels.ResponseVM
{
    public class ResponseViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public ErrorCodeEnum StatusCode { get; set; }
        public string Message { get; set; }

        public static ResponseViewModel<T> Success(T data, string massage = "")
        {
            return new ResponseViewModel<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = ErrorCodeEnum.Success,
                Message = massage
            };
        }
        public static ResponseViewModel<T> Failure(T data, string massage = "", ErrorCodeEnum statusCode = ErrorCodeEnum.None)
        {
            return new ResponseViewModel<T>
            {
                IsSuccess = false,
                Data = default,
                StatusCode = statusCode,
                Message = massage
            };
        }
    }
}