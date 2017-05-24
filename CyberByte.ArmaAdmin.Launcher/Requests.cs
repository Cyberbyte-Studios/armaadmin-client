using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CyberByte.ArmaAdmin.Launcher
{
    class Requests
    {
        public static JObject Get(string url)
        {
            try
            {
                var client = new RestClient(Util.FetchConfigValue("apiBaseURL"));//Fetch the configured API URL from config
                var request = new RestRequest(url, Method.GET);

                IRestResponse response = client.Execute(request);

                return JObject.Parse(response.Content.ToString());
            }
            catch
            {
                throw new RequestException("Unable To Process Get Request to " + Util.FetchConfigValue("apiBaseURL") + url);
            }
        }

        public static JObject Post(string url, string[][] values)
        {
            try
            {
                var client = new RestClient(Util.FetchConfigValue("apiBaseURL"));//Fetch the configured API URL from config
                var request = new RestRequest(url, Method.POST);

                foreach (string[] value in values)
                {
                    request.AddParameter(value[0], value[1]);
                }

                IRestResponse response = client.Execute(request);

                return JObject.Parse(response.Content.ToString());
            } 
            catch
            {
                throw new RequestException("Unable To Process Post Request to " + Util.FetchConfigValue("apiBaseURL") + url);
            }
        }
    }

    class RequestException: Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestException() : base()

        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public RequestException(String message) : base(message)

        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public RequestException(String message, Exception innerException) : base(message, innerException)

        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
