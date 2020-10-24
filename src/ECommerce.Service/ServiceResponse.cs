using System.Collections.Generic;

namespace ECommerce.Service
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}