using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.Serialization;
using CyberByte.ArmaAdmin.Launcher.Models;

namespace CyberByte.ArmaAdmin.Launcher.Services
{
    class Requests
    {
        public static JObject Get(string url)
        {
            try
            {
                var client = new RestClient(Util.FetchAppSettingsValue("apiBaseURL"));//Fetch the configured API URL from config
                var request = new RestRequest(url, Method.GET);

                IRestResponse response = client.Execute(request);

                return JObject.Parse(response.Content.ToString());
            }
            catch
            {
                throw new RequestException("Unable To Process Get Request to " + Util.FetchAppSettingsValue("apiBaseURL") + url);
            }
        }

        public static IRestResponse Post(string url, List<String[]> values)
        {
            try
            {
                var client = new RestClient(Util.FetchAppSettingsValue("apiBaseURL"));//Fetch the configured API URL from config
                var request = new RestRequest(url, Method.POST);

                foreach (string[] value in values)
                {
                    request.AddParameter(value[0], value[1]);
                }

                IRestResponse response = client.Execute(request);

                return response;
            } 
            catch
            {
                throw new RequestException("Unable To Process Post Request to " + Util.FetchAppSettingsValue("apiBaseURL") + url);
            }
        }
    }
}
