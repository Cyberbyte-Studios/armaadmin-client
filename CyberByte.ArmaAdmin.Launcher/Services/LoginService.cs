using Extensions.Data;
using System.IO;
using AltoHttp;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using CyberByte.ArmaAdmin.Launcher.Models;
using System.Collections.Generic;
using RestSharp;
using System.Net;

namespace CyberByte.ArmaAdmin.Launcher.Services
{
    class LoginService
    {
        public User Login(User user)
        {
            Task<User> loginTask = Task<User>.Factory.StartNew(() =>
            {
                List<String[]> data = new List<String[]>();

                string[] username = { "username", user.Username};
                string[] password = { "password", user.Password };

                data.Add(username);
                data.Add(password);

                IRestResponse response = Requests.Post("api-token-auth/", data);
                dynamic content = JObject.Parse(response.Content.ToString());

                if (response.StatusCode != HttpStatusCode.BadRequest)
                {
                    user.Token = content.token;
                    Debug.WriteLine("User :: " + user.Token);
                }

                return user;
            });

            return loginTask.Result;
        }
    }
}
