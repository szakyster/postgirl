using System;
using System.Net.Http;
using System.Threading.Tasks;
using Postgirl.Models;

namespace Postgirl.Services
{
    public class RequestService(IHttpClientFactory httpClientFactory)
    {
        public async Task SendRequest(RequestModel request)
        {
            var httpRequest = new HttpRequestMessage();
            try
            {
                httpRequest.RequestUri = new Uri(request.Url);
            }
            catch (UriFormatException)
            {
                return;
            }

            httpRequest.Method = request.Method;
            
            var client = httpClientFactory.CreateClient();
            var response = await client.SendAsync(httpRequest);
            
            var x = response.Content;
        }
    }
}
