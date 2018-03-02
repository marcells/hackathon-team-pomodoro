using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PomodoroApp2
{
    public class PomodoroAPI
    {
        public class PutName
        {
            public string name { get; set; }
        }

        public class PomodoroEntry
        {
            public string Name { get; set; }
            public DateTime Time { get; set; }
        }

        public void StopPomodoro(string name)
        {
            var requestBody = new PutName {name = name};
            var json = JsonConvert.SerializeObject(requestBody);
            DoGetRequest("pomodoro", "DELETE", json);
        }

        public Task StartPomodoroAsync(string name)
        {
            return Task.Run(() => StartPomodoro(name));
        }

        public void StartPomodoro(string name)
        {
            var requestBody = new PutName {name = name};
            var json = JsonConvert.SerializeObject(requestBody);
            DoGetRequest("pomodoro", "PUT", json);
        }

        public Task<List<PomodoroEntry>> GetPomodorosAsync()
        {
            return Task.Run(() => GetPomodoros());
        }

        public  List<PomodoroEntry> GetPomodoros()
        {
            var response = DoGetRequest("pomodoros", "GET");
            var entries = JsonConvert.DeserializeObject<List<PomodoroEntry>>(response);
            return entries;
        }

        public void Clear()
        {
            var entries = GetPomodoros();
            foreach (var entry in entries)
                StopPomodoro(entry.Name);
        }

        private string DoGetRequest(string requestName, string requestMethod, string body = null)
        {
            var url = @"http://vc024.vescon.com:3000/api/";
            var webrequest = WebRequest.CreateHttp(url + requestName);
            webrequest.Method = requestMethod;

            if (body != null)
            {
                var bytes = Encoding.UTF8.GetBytes (body);
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