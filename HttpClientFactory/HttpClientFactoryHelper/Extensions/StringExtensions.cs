using Newtonsoft.Json.Linq;
using System.Globalization;

namespace HttpClientFactoryHelper.Extensions
{
    public static  class StringExtensions
    {
        public static async Task<string> ToUrlQuery(this string url, object metaToken)
        {
            if(metaToken == null)
            {
                return url;
            }

            var keyValueContent = metaToken.ToKeyValue();
            var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
            var urlEncodedString = await formUrlEncodedContent.ReadAsStringAsync();
            return $"{url}?{urlEncodedString}";
        }

        private static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                                                 .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                            jValue?.ToString("o", CultureInfo.InvariantCulture) :
                            jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }


    }
}
