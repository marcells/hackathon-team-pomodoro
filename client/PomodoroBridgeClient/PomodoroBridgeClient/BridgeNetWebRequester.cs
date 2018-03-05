using System;
using Bridge.jQuery2;
using Pomodoro;

namespace PomodoroBridgeClient
{
    public class BridgeNetWebRequester : Api.IWebRequester
    {
        public Action<string> Logger { get; set; }

        public string ExecuteRequest(string url, string method, string body = null)
        {
            string error = null;
            
            var options = new AjaxOptions
            {
                Url = url,
                Type = method,
                ContentType = "application/json; charset=utf-8",
                Async = false,
                Error = (xhr, s, arg3) => { error = xhr.StatusText; },
                Success = (xhr, s, arg3) => { error = null; }
            };

            Logger?.Invoke("Sending request : " + options.Type + " " + options.Url);
            if (body != null)
            {
                options.Data = body;
                Logger?.Invoke("With body: " + body);
            }

            var request = jQuery.Ajax(options);
            if (error != null)
                throw new Exception($"Failed to send request '{url}'. Got error: " + error);

            Logger?.Invoke("ResponseText: " + request.ResponseText);
            return request.ResponseText;
        }
    }
}