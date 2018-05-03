using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimpleEchoBot.Dialogs
{
    public class Api
    {
        private readonly IWebRequester _webRequester;

        public string BaseUrl { get; set; } = @"https://team-pomodoro.azurewebsites.net/api/";


        public Api(IWebRequester webRequester)
        {
            _webRequester = webRequester;
        }

        public static string Humanize(TimeSpan timeSpan)
        {
            double days = timeSpan.Days;
            var hours = timeSpan.Hours + days * 24;
            var minutes = timeSpan.Minutes + hours * 60;
            if (minutes <= 1)
                return "Just Now";

            var years = Math.Floor(timeSpan.TotalDays / 365);
            if (years >= 1)
                return $"{years} year{(years >= 2 ? "s" : null)} ago";

            var weeks = Math.Floor(timeSpan.TotalDays / 7);
            var result = string.Empty;
            if (weeks >= 1)
            {
                var partOfWeek = days - weeks * 7;
                if (partOfWeek > 0)
                    result = $", {partOfWeek} day{(partOfWeek > 1 ? "s" : null)}";

                return $"{weeks} week{(weeks >= 2 ? "s" : null)}{result} ago";
            }

            if (days >= 1)
            {
                var partOfDay = hours - days * 24;
                if (partOfDay > 0)
                    result = $", {partOfDay} hour{(partOfDay > 1 ? "s" : null)}";

                return $"{days} day{(days >= 2 ? "s" : null)}{result} ago";
            }

            if (hours >= 1)
            {
                var partOfHour = minutes - hours * 60;
                if (partOfHour > 0)
                    result = $", {partOfHour} minute{(partOfHour > 1 ? "s" : null)}";

                return $"{hours} hour{(hours >= 2 ? "s" : null)}{result} ago";
            }

            // Only condition left is minutes > 1
            return minutes.ToString("{0} minutes ago");
        }

        public void Stop(string name)
        {
            var requestBody = new PutName { name = name };
            var json = JsonConvert.SerializeObject(requestBody);
            ExecuteRequest("pomodoro", "DELETE", json);
        }

        public void Start(string name)
        {
            var requestBody = new PutName { name = name };
            var json = JsonConvert.SerializeObject(requestBody);
            ExecuteRequest("pomodoro", "PUT", json);
        }

        public List<PomodoroEntry> GetAll()
        {
            var response = ExecuteRequest("pomodoros", "GET");
            var entries = JsonConvert.DeserializeObject<List<PomodoroEntry>>(response);
            return entries;
        }

        public void Clear()
        {
            var entries = GetAll();
            foreach (var entry in entries)
                Stop(entry.Name);
        }

        private string ExecuteRequest(string requestName, string requestMethod, string body = null)
        {
            return _webRequester.ExecuteRequest(BaseUrl + requestName, requestMethod, body);
        }

        public interface IWebRequester
        {
            string ExecuteRequest(string url, string method, string body = null);
        }

        public class PomodoroEntry
        {
            public string Name { get; set; }
            public DateTime Time { get; set; }
        }

        private class PutName
        {
            public string name { get; set; }
        }
    }
}