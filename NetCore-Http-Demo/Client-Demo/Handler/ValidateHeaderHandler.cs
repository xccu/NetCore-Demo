using System.Net;

namespace Client_Demo.Handler
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
        /// <summary>
        /// 检查请求中是否存在 X-API-KEY 标头。 如果缺失 X-API-KEY，则返回 BadRequest。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-API-KEY"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(
                        "The API key header X-API-KEY is required.")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
