using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApps2Project
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchSettings : ContentPage
	{
		public MatchSettings ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);

            toolbarStk.BackgroundColor = Color.FromHex("#3f7247");
            toolbarStk.HeightRequest = 55;
            toolbarStk.HorizontalOptions = LayoutOptions.FillAndExpand;

            bar.Text = "Match Settings";
            bar.HorizontalTextAlignment = TextAlignment.Start;
            bar.Margin = 15;
            bar.TextColor = Color.White;
            bar.FontSize = 18;
            bar.FontAttributes = FontAttributes.Bold;

            settingStk.Padding = 10;

            Entry input1 = new Entry()
            {
                Placeholder = "Player 1",
                WidthRequest = 200,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Entry input2 = new Entry()
            {
                Placeholder = "Player 2",
                WidthRequest = 200,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            settingStk.Children.Add(input1);
            settingStk.Children.Add(input2);

            

        }
	}
}