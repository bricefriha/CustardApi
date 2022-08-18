
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustardApi.Objects
{
    public class Service : IDisposable
    {
        private string _host;
        private int _port;
        private string _baseUrl;
        private bool _sslCertificate;
        private string _lastController;
        private string _lastAction;
        private Dictionary<string, string> _requestHeaders;



        public string Host { get => _host; set => _host = value; }
        public int Port { get => _port; set => _port = value; }
        public bool SslCertificate { get => _sslCertificate; set => _sslCertificate = value; }
        public string LastController { get => _lastController; set => _lastController = value; }
        public string LastAction { get => _lastAction; set => _lastAction = value; }
        public string BaseUrl { get => _baseUrl; }
        public Dictionary<string, string> RequestHeaders { get => _requestHeaders; /*set => _requestHeaders = value;*/ }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">IP address or servername</param>
        /// <param name="port">port</param>
        /// <param name="sslCertificate">Is there an ssl certificate applied?</param>

        public Service(string host, int port = 80, bool sslCertificate = false)
        {
            _host = host;
            _port = port;
            _sslCertificate = sslCertificate;
            _requestHeaders = new Dictionary<string, string>();

            // Set the base url up then
            _baseUrl = $"{ (_sslCertificate ? "https" : "http")}://{ _host}{ (_port == 80 ? "/" : ":" + _port + "/")}";
        }
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public  Task<T> Post<T>(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<T>(controller, jsonBody, action, parameters, HttpMethod.Post, callbackError, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<T> Get<T>(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<T>(controller, jsonBody, action, parameters, HttpMethod.Get, callbackError, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<T> Put<T>(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {


            return Process<T>(controller, jsonBody, action, parameters, HttpMethod.Put, callbackError, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<T> Delete<T>(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<T>(controller, jsonBody, action, parameters, HttpMethod.Delete, callbackError, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<string> Get(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<string>(controller, jsonBody, action, parameters, HttpMethod.Get, callbackError, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<string> Put(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {
            return Process<string>(controller, jsonBody, action, parameters, HttpMethod.Put, callbackError, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<string> Post(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<string>(controller, jsonBody, action, parameters, HttpMethod.Post, callbackError, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <returns></returns>
        public Task<string> Delete(string controller, string action = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<string>(controller, jsonBody, action, parameters, HttpMethod.Delete, callbackError, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Get get a response
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller"></param>
        /// <param name="jsonBody"></param>
        /// <param name="action"></param>
        /// <param name="headers"></param>
        /// <param name="headers"></param>
        /// <param name="httpMethod"></param>
        /// <returns>response of the method in the form of a model</returns>
        private async Task<T> Process<T>(string controller, string jsonBody, string action, string[] parameters, HttpMethod httpMethod, Action<HttpStatusCode?> callbackError = null, IDictionary<string, string> singleUseHeaders = null, IDictionary<string, string> headers = null)
        {
            var result = default(T);
            // Build the url
            string methodUrl = _baseUrl + controller + (string.IsNullOrEmpty(action) ? "" : "/" + action);

            // If there are some parameters
            methodUrl = CreateUrl(parameters, methodUrl);

            // Build the request
            using (var request = new HttpRequestMessage(httpMethod, methodUrl))
            {
                // Content of the request
                if (jsonBody != null)
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }

                // These are the headers we'll use in the request 
                Dictionary<string, string> reqHeaders = new Dictionary<string, string>();

                // Merge single use headers with actual headers
                if (singleUseHeaders != null)
                    reqHeaders = this._requestHeaders.Concat(singleUseHeaders)
                                                     .ToLookup(x => x.Key, x => x.Value)
                                                     .ToDictionary(x => x.Key, g => g.First());
                else
                    reqHeaders = this._requestHeaders;
                // Headers of the request
                if (reqHeaders != null)
                    foreach (var h in reqHeaders)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }

                // Handler
                try
                {
                    using var handler = new HttpClientHandler();
                    using var client = new HttpClient(handler);
                    using var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                    var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        if (typeof(T) == typeof(string))
                            result = (T)(object)(response.Content == null ? null : await response.Content.ReadAsStringAsync());
                        else
                            result = JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        if (callbackError != null)
                            // Run the callback
                            callbackError(response.StatusCode);

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("[Issue Handler]: " + ex.Message);
                }

            }

            return result;


        }
        /// <summary>
        /// Get get a string response
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="jsonBody"></param>
        /// <param name="action"></param>
        /// <param name="headers"></param>
        /// <param name="httpMethod"></param>
        /// <returns>response of the method in the form of a string</returns>
        private async Task<string> Process(string controller, string jsonBody, string action, string[] parameters, HttpMethod httpMethod, Action<HttpStatusCode?, IDictionary<string, string>> callbackError = null, IDictionary<string, string> headers = null, IDictionary<string, string> singleUseHeaders = null)
        {
            string result = string.Empty;

            // Build the url
            string methodUrl = _baseUrl + controller + (string.IsNullOrEmpty(action) ? string.Empty : "/" + action);

            // If there are some parameters
            methodUrl = CreateUrl(parameters, methodUrl);

            Dictionary<string, string> reqHeaders = new Dictionary<string, string>();

            // Merge single use headers with actual headers
            if (singleUseHeaders != null)
                reqHeaders = this._requestHeaders.Concat(singleUseHeaders)
                                                 .ToLookup(x => x.Key, x => x.Value)
                                                 .ToDictionary(x => x.Key, g => g.First());
            else
                reqHeaders = this._requestHeaders;
            Debug.WriteLine(reqHeaders);
            // Build the request
            using (var request = new HttpRequestMessage(httpMethod, methodUrl))
            {
                // Content of the request
                if (jsonBody != null)
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }
                // Headers of the request

                // Headers of the request
                if (singleUseHeaders == null && this._requestHeaders != null)
                {
                    foreach (var h in this._requestHeaders)
                    {

                        request.Headers.Add(h.Key, h.Value);
                    }
                }
                else if (reqHeaders != null)
                {
                    foreach (var h in reqHeaders)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }
                // Handler
                try
                {
                    using var handler = new HttpClientHandler();
                    using var client = new HttpClient(handler);
                    using var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                    if (response.IsSuccessStatusCode)
                    {

                        // Set the result from the response
                        result = response.Content == null ? null : await response.Content.ReadAsStringAsync();
                    }
                    else
                        // Run the callback
                        callbackError(response.StatusCode, headers);
                }
                catch (Exception ex)
                {
                    throw new Exception("[Issue Handler]: " + ex.Message);
                }

            }

            return result;
        }
        /// <summary>
        ///  Method to create a url with the given parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <param name="initialUrl">The base url</param>
        /// <returns>The url with all the parameters</returns>
        private static string CreateUrl(string[] parameters, string initialUrl)
        {
            if (parameters != null)
            {
                // For each parameters
                foreach (string parameter in parameters)
                {
                    // Add the parameter to the url
                    initialUrl += $"/{parameter}";

                }
            }

            return initialUrl;
        }


        #region Deprecated methods
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="callbackError">Call back if there is an error</param>
        /// <returns></returns>
        [Obsolete("Please use Post() instead and use the Service.RequestHeaders")]
        public async Task<T> ExecutePost<T>(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {

            return await Process<T>(controller, jsonBody, action, parameters, HttpMethod.Post, headers: headers);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        [Obsolete("Please use Get() instead and use the Service.RequestHeaders")]
        public async Task<T> ExecuteGet<T>(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {

            return await Process<T>(controller, jsonBody, action, parameters, HttpMethod.Get, headers: headers);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        [Obsolete("Please use Put() instead and use the Service.RequestHeaders")]
        public async Task<T> ExecutePut<T>(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {


            return await Process<T>(controller, jsonBody, action, parameters, HttpMethod.Put, headers: headers);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        [Obsolete("Please use Delete() instead and use the Service.RequestHeaders")]
        public async Task<T> ExecuteDelete<T>(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {

            // Get the reponse
            return await Process<T>(controller, jsonBody, action, parameters, HttpMethod.Delete, headers: headers);
        }
        /// <summary>
        /// Execute a post method without header and return a string
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        [Obsolete("Please use Post() instead and use the Service.RequestHeaders")]
        public async Task<string> ExecutePost(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {

            return await Process(controller, jsonBody, action, parameters, HttpMethod.Post, headers: headers);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<string> ExecuteGet(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action<HttpStatusCode?> callbackError = null)
        {

            return await Process(controller, jsonBody, action, parameters, HttpMethod.Get, headers: headers);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        [Obsolete("Please use Put() instead and use the Service.RequestHeaders")]
        public async Task<string> ExecutePut(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {


            return await Process(controller, jsonBody, action, parameters, HttpMethod.Put, headers: headers);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        [Obsolete("Please use Delete() instead and use the Service.RequestHeaders")]
        public async Task<string> ExecuteDelete(string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
        {

            // Get the reponse
            return await Process(controller, jsonBody, action, parameters, HttpMethod.Delete, headers: headers);
        }


        #endregion
        public void Dispose()
        {
            // Remove all the headers
            this._requestHeaders.Clear();
        }
    }
}
