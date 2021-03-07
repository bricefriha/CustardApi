
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustardApi.Objects
{
    public class Service
    {
        private string _host;
        private int _port;
        private string _baseUrl;
        private bool _sslCertificate;
        private string _lastController;
        private string _lastAction;

        

        public string Host { get => _host; set => _host = value; }
        public int Port { get => _port; set => _port = value; }
        public bool SslCertificate { get => _sslCertificate; set => _sslCertificate = value; }
        public string LastController { get => _lastController; set => _lastController = value; }
        public string LastAction { get => _lastAction; set => _lastAction = value; }
        public string BaseUrl { get => _baseUrl; }

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

            // Set the base url up then
            _baseUrl = (_sslCertificate ? "https" : "http") + "://"+ _host + (_port == 80 ? "/" : ":" + _port + "/"); 
        }
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="callbackError">Call back if there is an error</param>
        /// <returns></returns>
        public async Task<T> ExecutePost<T>( string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError= null)
        {

            return await Process<T>(controller, jsonBody, action, headers, parameters, HttpMethod.Post, callbackError);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<T> ExecuteGet<T>( string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {

            return await Process<T>(controller, jsonBody, action, headers, parameters, HttpMethod.Get, callbackError);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<T> ExecutePut<T>( string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {


            return await Process<T>(controller, jsonBody, action, headers, parameters, HttpMethod.Put, callbackError);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<T> ExecuteDelete<T>( string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {

            // Get the reponse
            return await Process<T>(controller, jsonBody, action, headers, parameters, HttpMethod.Delete, callbackError);
        }
        /// <summary>
        /// Execute a post method without header and return a string
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<string> ExecutePost (string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {

            return await Process(controller, jsonBody, action, headers, parameters, HttpMethod.Post, callbackError);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<string> ExecuteGet (string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {

            return await Process (controller, jsonBody, action, headers, parameters, HttpMethod.Get, callbackError);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<string> ExecutePut (string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {


            return await Process (controller, jsonBody, action, headers, parameters, HttpMethod.Put, callbackError);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <returns></returns>
        public async Task<string> ExecuteDelete (string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null, Action callbackError = null)
        {

            // Get the reponse
            return await Process (controller, jsonBody, action, headers, parameters, HttpMethod.Delete, callbackError);
        }
        /// <summary>
        /// Get get a response
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller"></param>
        /// <param name="jsonBody"></param>
        /// <param name="action"></param>
        /// <param name="headers"></param>
        /// <param name="httpMethod"></param>
        /// <returns>response of the method in the form of a model</returns>
        private async Task<T> Process<T>(string controller, string jsonBody, string action, IDictionary<string, string> headers, string[] parameters, HttpMethod httpMethod, Action<HttpStatusCode> callbackError)
        {
            var result = default(T);
            // Build the url
            string methodUrl = _baseUrl + controller +  (string.IsNullOrEmpty(action) ? "" : "/" + action);

            // If there are some parameters
            if (parameters != null)
            {
                // For each parameters
                foreach (string parameter in parameters)
                {
                    // Add the parameter to the url
                    methodUrl += "/" + parameter;

                }
            }

            // Build the request
            using (var request = new HttpRequestMessage(httpMethod, methodUrl))
            {
                // Content of the request
                if (jsonBody != null)
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }
                // Headers of the request
                if (headers != null)
                {
                    foreach (var h in headers)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }
                // Handler
                try
                {
                    using (var handler = new HttpClientHandler())
                    {
                        using (var client = new HttpClient(handler))
                        {
                            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                            {
                                var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                                // if this is a success
                                if (response.IsSuccessStatusCode)
                                {
                                    result = JsonConvert.DeserializeObject<T>(content);
                                }
                                else
                                    // Run the callback
                                    callbackError(response.StatusCode);
                            }
                        }
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
        private async Task<string> Process (string controller, string jsonBody, string action, IDictionary<string, string> headers, string[] parameters, HttpMethod httpMethod , Action callbackError)
        {
            string result = string.Empty;

            // Build the url
            string methodUrl = _baseUrl + controller +  (string.IsNullOrEmpty(action) ? "" : "/" + action);

            // If there are some parameters
            if (parameters != null)
            {
                // For each parameters
                foreach (string parameter in parameters)
                {
                    // Add the parameter to the url
                    methodUrl += "/" + parameter;

                }
            }

            // Build the request
            using (var request = new HttpRequestMessage(httpMethod, methodUrl))
            {
                // Content of the request
                if (jsonBody != null)
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }
                // Headers of the request
                if (headers != null)
                {
                    foreach (var h in headers)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }
                // Handler
                try
                {
                    using (var handler = new HttpClientHandler())
                    {
                        using (var client = new HttpClient(handler))
                        {
                            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                            {
                                var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                                // if this is a success
                                if (response.IsSuccessStatusCode)
                                {
                                    // Set the result from the response
                                    result = content;
                                }
                                else
                                    // Run the callback
                                    callbackError();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("[Issue Handler]: " + ex.Message);
                }

            }

            return result;
        }
    }
}
