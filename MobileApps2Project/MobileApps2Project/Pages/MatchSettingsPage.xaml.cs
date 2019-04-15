using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MobileApps2Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchSettingsPage : INotifyPropertyChanged
    {
        Entry player1;
        Entry player2;
        Entry setNumber;
        Entry testStartScore;
        List<Checkout> checkouts;

        //Sets up Match Setting UI
        public MatchSettingsPage()
        {

            InitializeComponent();

            //Gets checkout list before it is needed in the matchpage so the user can use the calculator as soon as they hit start match
            Task getMongoData = Task.Factory.StartNew(() =>
            {
                try
                {
                    MongoService mongoservice = new MongoService();
                    checkouts = mongoservice.GetAllData();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
              

            });

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

            settingsGrd.Padding = 10;

            Label l1 = new Label()
            {
                Text = "Player 1 Name: ",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold
            };

            Label l2 = new Label()
            {
                Text = "Player 2 Name: ",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold
            };

            Label l3 = new Label()
            {
                Text = "Start Score: ",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold
            };

            Label l4 = new Label()
            {
                Text = "Number of Sets: ",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold
            };

            player1 = new Entry()
            {
                Placeholder = "Player 1",
                WidthRequest = 200,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            player2 = new Entry()
            {
                Placeholder = "Player 2",
                WidthRequest = 200,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            testStartScore = new Entry()
            {
                Placeholder = "301 OR 501 OR 701",
                WidthRequest = 200,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            setNumber = new Entry()
            {
                Placeholder = "(3 legs in a set)",
                WidthRequest = 200,
                HeightRequest = 40,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            Button startBtn = new Button()
            {
                Text = "Start new Game",
                HeightRequest = 50,
                BackgroundColor = Color.FromHex("#e2e2e2")

            };

            startBtn.Clicked += StartBtn_Clicked;

            settingsGrd.Children.Add(l1, 0, 0);
            settingsGrd.Children.Add(l2, 0, 1);
            settingsGrd.Children.Add(l3, 0, 2);
            settingsGrd.Children.Add(l4, 0, 3);
            settingsGrd.Children.Add(player1, 0, 0);
            settingsGrd.Children.Add(player2, 0, 1);
            settingsGrd.Children.Add(testStartScore, 0, 2);
            settingsGrd.Children.Add(setNumber, 0, 3);
            settingsGrd.Children.Add(startBtn, 0, 4);



        }


        //Pushed to the match page if all the entry boxes are filled
        private async void StartBtn_Clicked(object sender, EventArgs e)
        {
            if ((testStartScore.Text == "301" || testStartScore.Text == "501" || testStartScore.Text == "701")
                    && player1.Text != null && player2.Text != null && setNumber.Text != null)
            {
                MatchSettings ms = new MatchSettings(player1.Text, player2.Text, testStartScore.Text,setNumber.Text);
                await Navigation.PushAsync(new MatchPage(ms, checkouts));

            }
            else
            {
                DisplayAlert("ERROR", "Please Enter all Values", "OK");
            }
        }
    }
}