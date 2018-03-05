using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PomodoroViewModel : MonoBehaviour {

    public Button StartStopButton;
    public Text StartStopButtonText;    

    public Text PomodoroTimeLabel;
    public Text PomodoroList;
    public Text UserLabel;
    public string LoggedInUser;
    
    public int MaximalPomodoroTime;
    public int ServerRequestTime;

    private bool _isPomodoro;
    private Stopwatch _stopwatch;
    private Stopwatch _serverRequeStopwatch;
    private StringBuilder stringBuilder;
    private PomodoroAPI pomodoroApi = new PomodoroAPI();

    public List<PomodoroItem> PomodoroItems = new List<PomodoroItem>();

    // Use this for initialization
    void Start ()
    {
        StartStopButtonText.text = "Start";
        StartStopButton.onClick.AddListener(ChangePomodoroStatus);

        _serverRequeStopwatch = new Stopwatch();
        RefreshPomodoros();
        
        LoggedInUser = PlayerPrefs.GetString("UserName");
        if (LoggedInUser == "") UnityEngine.Debug.LogError("error no user logged in ");
        UserLabel.text = LoggedInUser;
    }


    void ChangePomodoroStatus()
    {
         if (_isPomodoro)
            StopPomodoro();
         else
            StartPomodoro();         
    }

    void StartPomodoro()
    {
        ColorBlock cb = StartStopButton.colors;        
        cb.highlightedColor = new Color(1.0f, 0, 0);
        StartStopButton.colors = cb;

        StartStopButtonText.text = "Stop";

        _isPomodoro = true;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();

        
        pomodoroApi.StartPomodoro(LoggedInUser);
        RefreshPomodoros();
    }

    void StopPomodoro()
    {
        ColorBlock cb = StartStopButton.colors;
        cb.highlightedColor = new Color(0, 1.0f, 0);
        StartStopButton.colors = cb;


        StartStopButtonText.text = "Start";
        _isPomodoro = false;
        _stopwatch.Stop();

        pomodoroApi.StopPomodoro(LoggedInUser);
        RefreshPomodoros();
    }

    void Update()
    {
        if (_serverRequeStopwatch.Elapsed.Minutes >= ServerRequestTime)
        {
            RefreshPomodoros();
            _serverRequeStopwatch = new Stopwatch();
        }

        if (_stopwatch == null || _stopwatch.IsRunning == false) return;
        RefreshStopWatch();        
    }

    public void AddPomodoro(string name, TimeSpan time)
    {
        PomodoroItems.Add(new PomodoroItem(){ Name = name, Time = time});
    }

    private void MapModels(PomodoroEntry pomodoroEntry)
    {
        if (pomodoroEntry.Name.Length > 255 || string.IsNullOrEmpty(pomodoroEntry.Name )) return;

        PomodoroItems.Add(new PomodoroItem()
        {
           Name = pomodoroEntry.Name,
           Time = DateTime.Now.TimeOfDay - pomodoroEntry.Time.TimeOfDay
        });
    }

    public void RefreshPomodoros()
    {
        pomodoroApi.GetPomodoros().ForEach(MapModels);        
        
        if (PomodoroItems.Count != 0)
            stringBuilder = new StringBuilder();

            PomodoroItems.ForEach(x => AddPomodoroItemDescription(x.Name,x.Time));
    }

    private void AddPomodoroItemDescription(string name, TimeSpan time)
    {
        stringBuilder.AppendLine(name + " - "+ String.Format("{0:00}:{1:00}", time.Minutes, time.Seconds));
        PomodoroList.text = stringBuilder.ToString();
    }
    
    private void RefreshStopWatch()
    {        
        if (_stopwatch.Elapsed.Minutes == MaximalPomodoroTime) StopPomodoro();

        PomodoroTimeLabel.text = String.Format("{0:00}:{1:00}", _stopwatch.Elapsed.Minutes, _stopwatch.Elapsed.Seconds);
    }
}
