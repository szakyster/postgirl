using System.Net.Http;

namespace Postgirl.Models
{
    public class RequestModel
    {
        public string Url { get; set; } = string.Empty;
        public HttpMethod Method { get; set; } = HttpMethod.Get;
    }
}
