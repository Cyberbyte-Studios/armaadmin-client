﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace CyberByte.ArmaAdmin.Launcher
{
    class Requests
    {
        public static JObject Get(string url)
        {
            try
            {
                var client = new RestClient("");//Fetch the configured API URL from config
                var request = new RestRequest(url, Method.GET);

                IRestResponse response = client.Execute(request);

                return JObject.Parse(response.Content.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static JObject Post(string url, string[][] values)
        {
            try
            {
                var client = new RestClient("");//Fetch the configured API URL from config
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
                return null;
            }
        }
    }
}