using System.Net.Http.Headers;
using System.Net;
using HttpClientFactoryHelper.Constant;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace HttpClientFactoryHelper.Config
{
    public static class HttpClientConfig
    {
        public static void AddHttpClientConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Retry policy with Proxy.
             #region RetryAndProxy
             services
              .AddHttpClient(NamedHttpClients.ExternalFakeAPIClient, c =>
              {
                  c.BaseAddress = new Uri(configuration.GetSection("HttpClientFakeAPI").Value);
                  c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              })
               .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
               {
                   Proxy = new WebProxy
                   {
                       Address = new Uri(configuration.GetSection("RestServiceProxyAddress").Value),
                       BypassProxyOnLocal = false,
                       UseDefaultCredentials = false,
                   }
               })
               .AddPolicyHandler(GetRetryPolicy());
            #endregion


            // Timeout ve Retry policy
            /*           
            // requeste 1sn.den fazla cevap gelmezse fail kabul edilir. 2 nin katları şeklinde 5 kez tekrar denenir.
            #region TimeoutAndRetry
            services
                .AddHttpClient(NamedHttpClients.ExternalFakeAPIClient, c =>
                {
                    c.BaseAddress = new Uri(configuration.GetSection("HttpClientFakeAPI").Value);
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
                .AddTransientHttpErrorPolicy(b => b.Or<TimeoutRejectedException>().WaitAndRetryAsync(5, c => TimeSpan.FromSeconds(Math.Pow(2, c))))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
            #endregion


            // CircuitBreaker
            // 3 kez üst üste hata alırsa devreyi kesecek ve 15 saniye bekledikten sonra tekrar istek gönderecek.
            #region CircuitBreaker
            services
                .AddHttpClient(NamedHttpClients.ExternalFakeAPIClient, c =>
                {
                    c.BaseAddress = new Uri(configuration.GetSection("HttpClientFakeAPI").Value);
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddTransientHttpErrorPolicy(b => b.Or<TimeoutRejectedException>().WaitAndRetryAsync(5,c => TimeSpan.FromSeconds(Math.Pow(2, c))))
                .AddTransientHttpErrorPolicy(b => b.Or<TimeoutRejectedException>().CircuitBreakerAsync(3,TimeSpan.FromSeconds(15)))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
            #endregion

*/

        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromMilliseconds(1000 * retryAttempt));
        }
    }
}
