using System;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ParxlabAVM.Helpers
{
    public static class WebApiCagirici
    {
        private static string sunucuIP()
        {
            Uri uri = HttpContext.Current.Request.Url;
            return uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
        }

        public static HttpResponseMessage getFonksiyonuCagir(string yol)
        {
            /* sunucuIP fonksiyonu ile localhost adresi bulunur (ör:http://localhost:3214)
             * girdi olarak web api fonksyonları çağrılırken /api/ sonrasında yazılacak bağlantı alır
             * sunucuIP/api/yol adresi çağrılarak gelen sonuç döndürülür
             */

            var client = new HttpClient { BaseAddress = new Uri(sunucuIP() + "/" + "api/") };

            HttpResponseMessage yanit = client.GetAsync(yol).Result;

            return yanit;
        }

        public static HttpResponseMessage postFonksiyonuCagir(string yol, string json)
        {
            /* sunucuIP fonksiyonu ile localhost adresi bulunur (ör:http://localhost:3214)
             * girdi olarak web api fonksyonları çağrılırken /api/ sonrasında yazılacak bağlantı alır
             * sunucuIP/api/yol adresi verilen json ile çağrılarak gelen sonuç döndürülür
             */

            var client = new HttpClient { BaseAddress = new Uri(sunucuIP() + "/" + "api/") };

            HttpResponseMessage yanit = client.PostAsync(yol, new StringContent(json, Encoding.UTF8, "application/json")).Result;

            return yanit;
        }

    }
}