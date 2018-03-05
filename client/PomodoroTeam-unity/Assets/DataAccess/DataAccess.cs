using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public partial class PomodoroAPI
{
    public void StopPomodoro(string name)
    {
        var requestBody = new PutName { name = name };
        var json = JsonConvert.SerializeObject(requestBody);
        DoGetRequest("pomodoro", "DELETE", json);
    }

    public void StartPomodoro(string name)
    {
        var requestBody = new PutName { name = name };
        var json = JsonConvert.SerializeObject(requestBody);
        DoGetRequest("pomodoro", "PUT", json);
    }

    public List<PomodoroEntry> GetPomodoros()
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
        var webrequest = WebRequest.Create(url + requestName);
        webrequest.Method = requestMethod;

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
