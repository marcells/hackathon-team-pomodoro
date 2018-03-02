using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PomodoroApp2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

	    private async void OnLogin (object sender, EventArgs e)
	    {
	        var page = new MainPage {Name = nameText.Text};
	        await Navigation.PushAsync (page);
	    } 
	}
}