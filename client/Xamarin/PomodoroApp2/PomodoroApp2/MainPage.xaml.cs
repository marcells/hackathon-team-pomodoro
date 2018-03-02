using System;
using Xamarin.Forms;

namespace PomodoroApp2
{
	public partial class MainPage : ContentPage
	{
        private readonly PomodoroAPI _api = new PomodoroAPI();

	    public MainPage ()
	    {
	        InitializeComponent ();
            
            pomodoroList.ItemTapped += PomodoroList_ItemTapped;
	    }

	    public string Name { get; set; }

	    private void PomodoroList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Name = ((PomodoroAPI.PomodoroEntry) e.Item).Name;
            nameText.Text = Name;
        }

        protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        await _api.StartPomodoroAsync(Name);
	        pomodoroList.ItemsSource = await _api.GetPomodorosAsync();
	    }

	    public void RefreshPomodoros()
	    {
	        var entries = _api.GetPomodoros();
            
	        pomodoroList.ItemsSource = entries;
	    }
	    private void OnRefresh (object sender, EventArgs e)
	    {
	        RefreshPomodoros();
            pomodoroList.EndRefresh();
	    } 
	    
	    private void OnStart (object sender, EventArgs e)
	    {
	        _api.StartPomodoro(Name);
            RefreshPomodoros();
	    } 
	    
	    private void OnStop (object sender, EventArgs e)
	    {
	        _api.StopPomodoro(Name);
            RefreshPomodoros();
	    }

	    private void OnTextChanged(object sender, TextChangedEventArgs e)
	    {
	        Name = nameText.Text;
	    }
	}
}
