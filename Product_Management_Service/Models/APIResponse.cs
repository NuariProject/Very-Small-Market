using System.Net;

namespace Product_Management_Service.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> Messages { get; set; }
        public object Result { get; set; }
    }
}
