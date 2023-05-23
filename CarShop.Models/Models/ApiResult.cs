using System.Collections.Generic;

namespace CarShop.Models.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public List<string> Messages { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(bool isSuccess, List<string> messages = null)
        {
            IsSuccess = isSuccess;
            Messages = messages;
        }
    }
}
