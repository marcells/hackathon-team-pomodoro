using System.IO;
using System.Net;
using System.Text;
using Pomodoro;

namespace PomodoroForms
{
    internal class NetWebRequester : Api.IWebRequester
    {
        public string ExecuteRequest(string url, string method, string body = null)
        {
            var webrequest = WebRequest.CreateHttp(url);
            webrequest.Method = method;

            if (body != null)
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                webrequest.ContentType = "application/json";
                webrequest.ContentLength = bytes.Length;

                using (var outStream = webrequest.GetRequestStream())
                    outStream.Write(bytes, 0, bytes.Length);
            }

            using (var response = webrequest.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
    }
}