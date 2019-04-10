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

        public MainPage()
        {
            InitializeComponent();

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


            //getMongoData();

        }

        public void getMongoData()
        {
            MongoService ms = new MongoService();
            var x = ms.GetAllStats();
        }

        private async void NewGameBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MatchSettingsPage());

        }

        private async void StatsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Stats());
        }
    }
}