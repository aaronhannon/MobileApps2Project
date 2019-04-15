using MobileApps2Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApps2Project
{
    public partial class MainPage : ContentPage
    {
        public MatchPage mp;
        Stats stats;

        //Setting up the page
        public MainPage()
        {
            InitializeComponent();
            //Downloads Game Stats before going to that page
            try
            {
                stats = new Stats();
                stats.getMongoData();
            }
            catch (Exception)
            {

                DisplayAlert("ERROR", "Network Connection Required To View Stats", "OK");
            }


            NavigationPage.SetHasNavigationBar(this, false);

            toolbarStk.BackgroundColor = Color.FromHex("#3f7247");
            toolbarStk.HeightRequest = 55;
            toolbarStk.HorizontalOptions = LayoutOptions.FillAndExpand;

            bar.Text = "Main Page";
            bar.HorizontalTextAlignment = TextAlignment.Start;
            bar.Margin = 15;
            bar.TextColor = Color.White;
            bar.FontSize = 18;
            bar.FontAttributes = FontAttributes.Bold;

            newGameBtn.Text = "Create New Game";
            newGameBtn.HeightRequest = 50;
            newGameBtn.BackgroundColor = Color.FromHex("#e2e2e2");

            statsBtn.Text = "View Game Statistics";
            statsBtn.HeightRequest = 50;
            statsBtn.BackgroundColor = Color.FromHex("#e2e2e2");

        }

        //Gets Game Stats from Mongo
        public void getMongoData()
        {
            MongoService ms = new MongoService();
            var x = ms.GetAllStats();
        }

        //Navigates to the match settings page
        private async void NewGameBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MatchSettingsPage());

        }

        //Navigates to the Gamestats page
        private async void StatsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(stats);
        }
    }
}