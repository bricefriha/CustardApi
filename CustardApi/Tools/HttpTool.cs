using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CustardApi.Tools
{
    public static class HttpTool
    {
        /// <summary>
        /// Send an HTTP request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">Request to send</param>
        /// <param name="unSuccessCallback">Action to execute if the request doesn't work</param>
        /// <returns></returns>
        //public static async Task<T> SendRequest<T>(HttpRequestMessage request, Action<HttpResponseMessage> unSuccessCallback)
        //{
        //    // Set client
        //    InstanciateClient(out HttpClient client);

        //    //Get a response
        //    using var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

        //    // Extract the result
        //    return await GetRequestResult<T>(response, unSuccessCallback);
        //}
        /// <summary>
        /// Send an HTTP request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">Request to send</param>
        /// <param name="unSuccessCallback">Action to execute if the request doesn't work</param>
        /// <param name="cancToken">Action to execute if the request doesn't work</param>
        /// <returns></returns>
        public static async Task<T> SendRequest<T>(HttpRequestMessage request, Action<HttpResponseMessage> unSuccessCallback, CancellationToken cancToken)
        {
            // Set client
            using var handler = new HttpClientHandler();
            
            // Get the clietn
            using var client = new HttpClient(handler);

            //Get a response
            using HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancToken);

            // Extract the result
            return await GetRequestResult<T>(response, unSuccessCallback);
        }

        /// <summary>
        ///  Get the result of a request
        /// </summary>
        /// <typeparam name="T">Type wanted as a return</typeparam>
        /// <param name="response">Response of the request</param>
        /// <param name="unSuccessCallback">Action to execute if the request doesn't work</param>
        /// <returns></returns>
        private static async Task<T> GetRequestResult<T>(HttpResponseMessage response, Action<HttpResponseMessage> unSuccessCallback)
        {
            var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                unSuccessCallback?.Invoke(response);

            if (typeof(T) == typeof(string))
                return (T)(object)(response.Content == null ? null : await response.Content.ReadAsStringAsync());
            else if (content != null)
                return JsonConvert.DeserializeObject<T>(content);
            else
                return default;
        }
        /// <summary>
        /// Set HttpClient value
        /// </summary>
        /// <param name="client"></param>
        private static void InstanciateHandler(out HttpClientHandler handler)
        {
            handler = new HttpClientHandler();
        }
    }
}
