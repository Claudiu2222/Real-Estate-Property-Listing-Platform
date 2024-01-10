using System.Net.Http.Headers;

namespace RealEstatePlatform.API.IntegrationTests.Base
{
    public static class HttpClientExtensions
    {
        public static HttpClient WithJwtBearerToken(this HttpClient client, Action<JwtTokenBuilder> configure)
        {
            var token = JwtTokenBuilder.Create();
            configure(token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Build());
            return client;
        }
    }
}
