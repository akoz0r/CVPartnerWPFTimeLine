using System.IO;
using System.Net;
using CVGuru.Domain;

namespace CVGuru
{
    public static class CVService
    {
        private const string baseURI = "http://localhost:8888";
        private const string usersURI = "/api/v1/users";

        private static string GetPage(string relativeURI)
        {
            var request = WebRequest.Create(baseURI + relativeURI);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Token token=\"f09f811d4e03d21fe802109e4575a864\"");

            string s;
            using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = httpWebResponse.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream);
                    var jsonStream = sr.ReadToEnd();
                    return jsonStream;
                }
            }
        }

        public static string GetUsersStream()
        {
            return GetPage(usersURI);
        }

        public static string GetCV(User user)
        {
            return GetPage("/api/v1/cvs/" + user.ID +"/" + user.DefaultCVID);
        }

        public static string GetDataFile(string uri)
        {
            var request = WebRequest.Create(uri);
            request.Method = "GET";


            string s;
            using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = httpWebResponse.GetResponseStream())
                {
                    var sr = new StreamReader(responseStream);
                    var jsonStream = sr.ReadToEnd();
                    return jsonStream;
                }
            }
        }
    }
}
