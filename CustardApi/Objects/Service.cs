
using CustardApi.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
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
        public string LastCall { get => _lastAction; set => _lastAction = value; }
        public string BaseUrl { get => _baseUrl; }
        public Dictionary<string, string> RequestHeaders { get => _requestHeaders; /*set => _requestHeaders = value;*/ }
        public Dictionary<string, string> LastCallRequestHeaders { get => _requestHeaders; private set => _requestHeaders = value; }

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
        #region Path parameters requests
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// 
        /// <returns>Result of the request</returns>
        public Task<T> Post<T>(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Get<T>(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Put<T>(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {


            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Delete<T>(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Get(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Put(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {
            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Post(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Delete(string controller, string jsonBody, string action = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="httpContent">httpContent</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Post<T>(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            return Process<T>(controller,contentType, httpContent, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="httpContent">httpContent</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Get<T>(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Put<T>(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {


            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Delete<T>(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            // Get the reponse
            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Get(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Put(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {
            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Post(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            // Get the reponse
            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Delete(string controller, string action = null, HttpContent httpContent = null, string[] parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            // Get the reponse
            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        #endregion

        #region Query parameters requests
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// 
        /// <returns>Result of the request</returns>
        public Task<T> Post<T>(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Get<T>(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Put<T>(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {


            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Delete<T>(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<T>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Get(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Put(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {
            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Post(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Delete(string controller, string jsonBody, string action = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {

            // Get the reponse
            return Process<string>(controller, "application/json", jsonBody, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method without header and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="httpContent">httpContent</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Post<T>(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="httpContent">httpContent</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Get<T>(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Put<T>(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {


            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<T> Delete<T>(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            // Get the reponse
            return Process<T>(controller, contentType, httpContent, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        /// <summary>
        /// Execute a get method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Get(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Get, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a put method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Put(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {
            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Put, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a post method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Post(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            // Get the reponse
            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Post, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }
        /// <summary>
        /// Execute a delete method and return a model
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller">name of the controller</param>
        /// <param name="action">name of the action</param>
        /// <param name="jsonBody">body in json</param>
        /// <param name="singleUseHeaders">headers that will only be used in this request</param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>Result of the request</returns>
        public Task<string> Delete(string controller, string action = null, HttpContent httpContent = null, IDictionary<string, string> parameters = null, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null, string contentType = "application/json")
        {

            // Get the reponse
            return Process<string>(controller, contentType, httpContent, action, parameters, HttpMethod.Delete, unSuccessCallback, singleUseHeaders: singleUseHeaders);
        }

        #endregion

        /// <summary>
        /// Get get a response of a path request using a string content
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller"></param>
        /// <param name="jsonBody"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>response of the method in the form of a model</returns>
        private async Task<T> Process<T>(string controller, string contentType, string jsonBody, string action, string[] parameters, HttpMethod httpMethod, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {
            try
            {
                string methodUrl = BuildUrl(controller, action, parameters);

                // Build the request
                return await ProcessRequest<T>(contentType, jsonBody, httpMethod, unSuccessCallback: unSuccessCallback, singleUseHeaders, methodUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("[Issue Handler]: " + ex.Message);
            }
            


        }

        /// <summary>
        /// Get get a response of a path request using a HttpContent
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller"></param>
        /// <param name="httpContent"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <returns>response of the method in the form of a model</returns>
        private async Task<T> Process<T>(string controller, string contentType, HttpContent httpContent, string action, string[] parameters, HttpMethod httpMethod, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {
            try
            {
                string methodUrl = BuildUrl(controller, action, parameters);

                return await ProcessRequest<T>(contentType: contentType, httpContent: httpContent, httpMethod: httpMethod, unSuccessCallback: unSuccessCallback, singleUseHeaders: singleUseHeaders, methodUrl: methodUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("[Issue Handler]: " + ex.Message);
            }
        }
        /// <summary>
        /// Get get a response of a query request using a string content
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller"></param>
        /// <param name="jsonBody"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <param name="unSuccessCallback">Action excecuted in when the call returns an unsuccessful status</param>
        /// <returns>response of the method in the form of a model</returns>
        private async Task<T> Process<T>(string controller, string contentType, string jsonBody, string action, IDictionary<string, string> parameters, HttpMethod httpMethod, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {
            try
            {
                string methodUrl = BuildUrl(controller, action, parameters);

                // Build the request
                return await ProcessRequest<T>(contentType, jsonBody, httpMethod, unSuccessCallback: unSuccessCallback, singleUseHeaders, methodUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("[Issue Handler]: " + ex.Message);
            }
            


        }

        /// <summary>
        /// Get get a response of a query request using a HttpContent
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="controller"></param>
        /// <param name="httpContent"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <returns>response of the method in the form of a model</returns>
        private async Task<T> Process<T>(string controller, string contentType, HttpContent httpContent, string action, IDictionary<string, string> parameters, HttpMethod httpMethod, Action<HttpResponseMessage> unSuccessCallback = null, IDictionary<string, string> singleUseHeaders = null)
        {
            try
            {
                string methodUrl = BuildUrl(controller, action, parameters);

                return await ProcessRequest<T>(contentType: contentType, httpContent: httpContent, httpMethod: httpMethod, unSuccessCallback: unSuccessCallback, singleUseHeaders: singleUseHeaders, methodUrl: methodUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("[Issue Handler]: " + ex.Message);
            }
        }
        /// <summary>
        /// Build a complete url with all the data needed for path parameters
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string BuildUrl(string controller, string action, string[] parameters)
        {
            // Build the url
            string methodUrl = _baseUrl + controller + (string.IsNullOrEmpty(action) ? "" : "/" + action);

            // If there are some parameters
            LastCall = methodUrl = CreateUrl(parameters, methodUrl);
            return methodUrl;
        }
        /// <summary>
        /// Build a complete url with all the data needed for query parameters
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string BuildUrl(string controller, string action, IDictionary<string, string> parameters)
        {
            // Build the url
            string methodUrl = _baseUrl + controller + (string.IsNullOrEmpty(action) ? "" : "/" + action);

            // If there are some parameters
            LastCall = methodUrl = CreateUrl(parameters, methodUrl);
            return methodUrl;
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="jsonBody"></param>
        /// <param name="httpMethod"></param>
        /// <param name="unSuccessCallback"></param>
        /// <param name="singleUseHeaders"></param>
        /// <param name="result"></param>
        /// <param name="methodUrl"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<T> ProcessRequest<T>(string contentType, string jsonBody, HttpMethod httpMethod, Action<HttpResponseMessage> unSuccessCallback, IDictionary<string, string> singleUseHeaders, string methodUrl)
        {
            var result = default(T);
            using (var request = new HttpRequestMessage(httpMethod, methodUrl))
            {
                // Content of the request
                request.Content = SetContentRequestFromString(contentType, jsonBody);

                // Send the request
                result = await SendRequest<T>(unSuccessCallback, singleUseHeaders, request);

            }

            return result;
        }

        /// <summary>
        /// Send the request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentType"></param>
        /// <param name="jsonBody"></param>
        /// <param name="httpMethod"></param>
        /// <param name="unSuccessCallback"></param>
        /// <param name="singleUseHeaders"></param>
        /// <param name="result"></param>
        /// <param name="methodUrl"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<T> ProcessRequest<T>(string contentType, HttpContent httpContent, HttpMethod httpMethod, Action<HttpResponseMessage> unSuccessCallback, IDictionary<string, string> singleUseHeaders, string methodUrl)
        {
            var result = default(T);
            using (var request = new HttpRequestMessage(httpMethod, methodUrl))
            {
                // Content of the request
                request.Content = httpContent;

                // Send the request
                result = await SendRequest<T>(unSuccessCallback, singleUseHeaders, request);

            }

            return result;
        }

        /// <summary>
        /// Build the content of a request
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        private static HttpContent SetContentRequestFromString(string contentType, string jsonBody)
        {
            if (jsonBody != null)
            {
                return new StringContent(jsonBody, Encoding.UTF8, contentType);
            }
            return null;

        }
        /// <summary>
        /// Send the http request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unSuccessCallback"></param>
        /// <param name="singleUseHeaders"></param>
        /// <param name="result"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<T> SendRequest<T>(Action<HttpResponseMessage> unSuccessCallback, IDictionary<string, string> singleUseHeaders, HttpRequestMessage request)
        {
            var result = default(T);
            // These are the headers we'll use in the request 
            Dictionary<string, string> reqHeaders = new Dictionary<string, string>();

            // Merge single use headers with actual headers
            if (singleUseHeaders != null)
                reqHeaders = this._requestHeaders.Concat(singleUseHeaders)
                                                 .ToLookup(x => x.Key, x => x.Value)
                                                 .ToDictionary(x => x.Key, g => g.First());
            else
                reqHeaders = this._requestHeaders;

            LastCallRequestHeaders = reqHeaders;

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

                if (!response.IsSuccessStatusCode)
                    unSuccessCallback?.Invoke(response);

                if (typeof(T) == typeof(string))
                    result = (T)(object)(response.Content == null ? null : await response.Content.ReadAsStringAsync());
                else if (content != null)
                    result = JsonConvert.DeserializeObject<T>(content);


            }
            catch (Exception ex)
            {

                throw new Exception("[Issue Handler]: " + ex.Message);
            }

            return result;
        }
        /// <summary>
        ///  Method to create a url with the given path parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <param name="initialUrl">The base url</param>
        /// <returns>The url with all the parameters</returns>
        private static string CreateUrl(string[] parameters, string initialUrl)
        {
            if (parameters != null)
            {
                initialUrl = UrlTool.BuildPathUrl(parameters, initialUrl);
            }

            return initialUrl;
        }

        /// <summary>
        ///  Method to create a url with the given query parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <param name="initialUrl">The base url</param>
        /// <returns>The url with all the parameters</returns>
        private static string CreateUrl(IDictionary<string,string> parameters, string initialUrl)
        {
            if (parameters != null)
            {
                initialUrl = UrlTool.BuildQueryUrl(parameters, initialUrl);
            }

            return initialUrl;
        }
        public void Dispose()
        {
            // Remove all the headers
            this._requestHeaders.Clear();
        }
    }
}
