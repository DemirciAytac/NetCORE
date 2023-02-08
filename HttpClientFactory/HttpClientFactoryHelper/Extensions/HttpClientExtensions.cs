using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace HttpClientFactoryHelper.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TOut> GetRequest<TIn, TOut>(this IHttpClientFactory httpClientFactory, string namedClient, string uri, TIn content )
        {
            using (var client = httpClientFactory.CreateClient(namedClient))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                uri = await uri.ToUrlQuery(content);

                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
        }

        public static async Task<TOut> PostRequest<TIn, TOut>(this IHttpClientFactory httpClientFactory, string namedClient, string uri, TIn content)
        {
            using (var client = httpClientFactory.CreateClient(namedClient))
            {
                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, MediaTypeNames.Application.Json);

                using (HttpResponseMessage response = await client.PostAsync(uri, serialized))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
        }

        public static async Task<TOut> PutRequest<TIn, TOut>(this IHttpClientFactory httpClientFactory, string namedClient, string uri, TIn content)
        {
            using (var client = httpClientFactory.CreateClient(namedClient))
            {
                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, MediaTypeNames.Application.Json);

                using (HttpResponseMessage response = await client.PutAsync(uri, serialized))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
        }
    }
}
