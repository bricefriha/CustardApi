using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustardApi.Tools
{
    public static class UrlTool
    {
        /// <summary>
        /// Build the url using parameters and initial url using a Path patern
        /// </summary>
        /// <param name="parameters">parameters you want in the URL</param>
        /// <param name="initialUrl">url you already have</param>
        /// <returns>new url with all the parameters</returns>
        public static string BuildPathUrl(string[] parameters, string initialUrl)
        {
            // For each parameters
            for (int i = 0; i < parameters.Count(); i++)
                // Add the parameter to the url
                initialUrl += $"/{parameters[i]}";
            return initialUrl;
        }
        /// <summary>
        /// Build the url using parameters and initial url using a Query patern
        /// </summary>
        /// <param name="parameters">parameters you want in the URL</param>
        /// <param name="initialUrl">url you already have</param>
        /// <returns>new url with all the parameters</returns>
        public static string BuildQueryUrl(IDictionary<string, string> parameters, string initialUrl)
        {
            // Add the initiator
            initialUrl += "?";

            // For each parameters
            for (int i = 0; i < parameters.Count(); ++i)
            {
                // Get the parameter 
                KeyValuePair<string, string> param = parameters.ElementAt(i);

                if (i == parameters.Count() - 1)
                    // Add the last parameter 
                    initialUrl += $"{param.Key}={param.Value}";
                else
                    // Add the parameter to the url while keeping in mind that another parameter will follow
                    initialUrl += $"{param.Key}={param.Value}&";
            }

            return initialUrl;
        }
    }
}
