using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SessionNetLib
{
    public static class Url {
        public static string Load(string url) {
            WebClient client = new WebClient();
            
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            
           return s;
        }

        public static string Gist(string url) {
            //var gistID = int.Parse(url.Split('/').Last();)
            //var apiUrl = string.Format("https://api.github.com/gists/{0}", gistID);
            //var result = Load(apiUrl);
            //return JObject.Parse(result).Value<string>("content");
            return "jgujbn.";
        }
    }
}
    