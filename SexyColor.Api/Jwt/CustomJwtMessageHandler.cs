using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SexyColor.Api.Jwt
{
    public class CustomJwtMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(task =>
            {
                HttpResponseMessage response = task.Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Content = new StringContent(JsonConvert.SerializeObject(new { code = 401, msg = "Unauthorized", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                    response.ReasonPhrase = "Unauthorized";
                }

                return response;
            });
        }
 
    }
}
