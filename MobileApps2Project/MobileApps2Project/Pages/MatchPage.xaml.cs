using MobileApps2Project.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApps2Project
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchPage : ContentPage
	{
        public MatchSettings matchSettings;
        public Entry score;
        public int p1Score;
        public int p2Score;
        public int p1Total;
        public int p2Total;
        public int p1Turns;
        public int p2Turns;
        public int p1Darts =0;
        public int p2Darts =0;
        public int playerTurn = 1;
        public const string CONVERTED = "converted.txt";

        Player player1;
        Player player2;
        List<KeyValuePair<string, string>> checkoutKeyPair;
        List<Checkout> checkoutList;
        List<CheckoutStrings> convertJSON;

        public MatchPage()
        {
            InitializeComponent();

        }

        public MatchPage(MatchSettings ms,object cs)
        {

            InitializeComponent();
            checkoutList = cs as List<Checkout>;
            matchSettings = ms;

            player1 = new Player(matchSettings.p1Name);
            player2 = new Player(matchSettings.p2Name);
            UpdateUI();
            UISetup();
            SetupCheckouts();

        }

        private void UpdateUI()
        {
            if(playerTurn == 1)
            {
                p1.TextColor = Color.Orange;
                p2.TextColor = Color.Black;
            }
            else if(playerTurn == 2)
            {
                p1.TextColor = Color.Black;
                p2.TextColor = Color.Orange;
            }

            p1.Text = player1.name +" | Sets: "+ player1.setsWon+ " Legs: "+ player1.legsWon;
            p2.Text = player2.name +" | Sets: "+ player2.setsWon+ " Legs: "+ player2.legsWon;

            stats1.Text = "AVG: "+player1.average+" | Last Score: "+ player1.lastScore;
            stats2.Text = "AVG: "+player2.average+" | Last Score: "+ player2.lastScore;
        }

        public void SetupCheckouts()
        {

            Task task1 = Task.Factory.StartNew(() =>
            {
                if (checkoutList == null)
                {
                    
                    checkoutKeyPair = new List<KeyValuePair<string, string>>();
                    string key;
                    string line;
                    string checkout;
                    const string NAME = "MobileApps2Project.checkouts.txt";
                    convertJSON = new List<CheckoutStrings>();

                    Assembly assembly = Assembly.GetExecutingAssembly();

                    using (Stream stream = assembly.GetManifestResourceStream(NAME))
                    {
                        StreamReader file = new StreamReader(stream);

                        while ((line = file.ReadLine()) != null)
                        {
                            key = line.Substring(0, 3);
                            key = Regex.Replace(key, @"[^0-9a-zA-Z]+", "");
                            checkout = line.Substring(3);
                            checkout = Regex.Replace(checkout, @"[^0-9a-zA-Z]+", " ");
                            checkout = checkout.Trim();

                            checkoutKeyPair.Add(new KeyValuePair<string, string>(key, checkout));

                            CheckoutStrings c = new CheckoutStrings(key, checkout);
                            convertJSON.Add(c);
                            Debug.WriteLine("NON-JSON");
                        }
                    }
                }




            });
        }

        //Method I used once to convert checkouts.txt to a json for a mongoDB
        private void ConvertToJson()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string filename = Path.Combine(path, CONVERTED);

            Debug.WriteLine(filename);

            using (var w = new StreamWriter(filename, false))
            {

                foreach (var x in convertJSON)
                {
                    string json = JsonConvert.SerializeObject(x);
                    w.WriteLine(json + ",");
                }


            }
        }

        private void Enter_Clicked(object sender, EventArgs e)
        {
            string checkout;
            

            if (playerTurn == 1)
            {
                if (Convert.ToInt32(score.Text) <= 180)
                {
                    player1.dartNumber += 3;
                    p1Turns++;
                    player1.lastScore = Convert.ToInt32(score.Text);
                    p1Total += Convert.ToInt32(score.Text);
                    player1.average = p1Total / p1Turns;

                    if ((Convert.ToInt32(l1.Text) - Convert.ToInt32(score.Text)) > 0)
                    {
                        p1Score = Convert.ToInt32(l1.Text) - Convert.ToInt32(score.Text);
                        
                    }
                    else if ((Convert.ToInt32(l1.Text) - Convert.ToInt32(score.Text)) == 0)
                    {

                        player1.legsWon++;
                        p1Score = Convert.ToInt32(matchSettings.startScore);
                        p2Score = Convert.ToInt32(matchSettings.startScore);


                        l1.Text = p1Score.ToString();
                        l2.Text = p2Score.ToString();
                        check1.Text = "No Checkout";
                        check2.Text = "No Checkout";

                        if (player1.legsWon == 3)
                        {
                            player1.legsWon = 0;
                            player1.setsWon++;

                        }

                        if (player1.setsWon.ToString() == matchSettings.setNumber)
                        {
                            p1Score = Convert.ToInt32(l1.Text) - Convert.ToInt32(score.Text);
                            DisplayAlert("Winner", matchSettings.p1Name + " Wins", "OK");

                            GameStats gs = new GameStats(matchSettings.p1Name, matchSettings.p2Name, player1.setsWon.ToString(), player1.legsWon.ToString(), player2.setsWon.ToString(), player2.legsWon.ToString()
                                , player1.average.ToString(), player2.average.ToString(), player1.dartNumber.ToString(), player2.dartNumber.ToString(), matchSettings.p1Name);
                  
                            writeStats(gs);
                            Navigation.PopAsync();
                        }

                    }

                    l1.Text = p1Score.ToString();
                    checkout = searchCheckouts(l1.Text);
                    check1.Text = checkout;
                    score.Text = "";
                    playerTurn = 2;
                    UpdateUI();
                }
                else
                {
                    DisplayAlert("ERROR", "Entered value greater than 180", "OK");
                }

            }
            else
            {
                
                if (Convert.ToInt32(score.Text) <= 180)
                {
                    player2.dartNumber += 3;
                    p2Turns++;
                    player2.lastScore = Convert.ToInt32(score.Text);
                    p2Total += Convert.ToInt32(score.Text);
                    player2.average = p2Total / p2Turns;

                    if ((Convert.ToInt32(l2.Text) - Convert.ToInt32(score.Text)) > 0)
                    {
                        p2Score = Convert.ToInt32(l2.Text) - Convert.ToInt32(score.Text);
                        player2.lastScore = Convert.ToInt32(score.Text);
                    }
                    else if ((Convert.ToInt32(l2.Text) - Convert.ToInt32(score.Text)) == 0)
                    {
                        player2.legsWon++;
                        p1Score = Convert.ToInt32(matchSettings.startScore);
                        p2Score = Convert.ToInt32(matchSettings.startScore);

                        l1.Text = p1Score.ToString();
                        l2.Text = p2Score.ToString();
                        check1.Text = "No Checkout";
                        check2.Text = "No Checkout";

                        if (player2.legsWon == 3)
                        {
                            player2.legsWon = 0;
                            player2.setsWon++;
                            
                        }

                        if (player2.setsWon.ToString() == matchSettings.setNumber)
                        {
                            p2Score = Convert.ToInt32(l2.Text) - Convert.ToInt32(score.Text);
                            DisplayAlert("Winner", matchSettings.p2Name + " Wins", "OK");

                            GameStats gs1 = new GameStats(matchSettings.p1Name, matchSettings.p2Name, player1.setsWon.ToString(), player1.legsWon.ToString(), player2.setsWon.ToString(), player2.legsWon.ToString()
                                , player1.average.ToString(), player2.average.ToString(), player1.dartNumber.ToString(), player2.dartNumber.ToString(), matchSettings.p2Name);

                            writeStats(gs1);
                            Navigation.PopAsync();
                        }

                    }

                    l2.Text = p2Score.ToString();
                    checkout = searchCheckouts(l2.Text);
                    check2.Text = checkout;
                    score.Text = "";
                    playerTurn = 1;
                    UpdateUI();
                }
                else
                {
                    DisplayAlert("ERROR", "Entered value greater than 180", "OK");
                }
                
            }

        }

        private void writeStats(GameStats g)
        {
            Task setMongoData = Task.Factory.StartNew(() =>
            {
                try
                {
                    MongoService mongoService = new MongoService();
                    mongoService.saveStatsData(g);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }


            });


        }

        private string searchCheckouts(string score)
        {
            string checkout = "No checkout";

            try
            {
                foreach (var x in checkoutList)
                {
                    if (x.score.Equals(score))
                    {
                        checkout = x.checkoutString;
                        Debug.WriteLine(checkout);
                        return checkout;
                    }
                }
            }
            catch (Exception)
            {

                foreach (var x in checkoutKeyPair)
                {
                    if (x.Key.Equals(score))
                    {
                        checkout = x.Value;
                        Debug.WriteLine(checkout);
                        return checkout;
                    }
                }
            }
            

            return checkout;
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            score.Text += btn.StyleId;
        }

        private void BtnRemove_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string s = score.Text;

            if(s.Length > 0) s = s.Remove(s.Length - 1);

            score.Text = s;
        }


        private void UISetup()
        {

            NavigationPage.SetHasNavigationBar(this, false);

            toolbarStk.BackgroundColor = Color.FromHex("#3f7247");
            toolbarStk.HeightRequest = 55;
            toolbarStk.HorizontalOptions = LayoutOptions.FillAndExpand;

            bar.Text = "Match";
            bar.HorizontalTextAlignment = TextAlignment.Start;
            bar.Margin = 15;
            bar.TextColor = Color.White;
            bar.FontSize = 18;
            bar.FontAttributes = FontAttributes.Bold;

            //textSettings.Text = matchSettings.p1Name + "askdlfhalskdjfh";

            l1.Text = matchSettings.startScore;
            l1.FontSize = 30;
            l1.FontAttributes = FontAttributes.Bold;
            
            l2.Text = matchSettings.startScore;
            l2.FontSize = 30;
            l2.FontAttributes = FontAttributes.Bold;

            Debug.WriteLine(matchSettings.p1Name);
            Debug.WriteLine(matchSettings.p2Name);
            Debug.WriteLine(matchSettings.startScore);

            score = new Entry()
            {
                Placeholder = "SCORE",
                WidthRequest = 250,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                
            };

            buttons.Children.Add(score, 1, 0);
            int counter = 0;

            Button enter = new Button()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 50,
                WidthRequest = 100,
                BackgroundColor = Color.Blue,
                StyleId = "0",
                Text = "Enter",
                TextColor = Color.White,
                CornerRadius = 10
        };

            enter.Clicked += Enter_Clicked;

            buttons.Children.Add(enter, 2, 0);

            //ROWS
            for (int i = 1; i < 5; i++)
            {
                //COLS
                for (int j = 0; j < 3; j++)
                {
                    counter++;

                    if (i < 4)
                    {
                        Button btn = new Button()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.DarkSlateGray,
                            Text = counter.ToString(),
                            StyleId = counter.ToString(),
                            TextColor = Color.White,
                            CornerRadius = 10
                        };

                        btn.Clicked += BtnAdd_Clicked;

                        buttons.Children.Add(btn, j, i);
                    }
                    else
                    {
                        if (j == 1)
                        {

                            Button btn = new Button()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.DarkSlateGray,
                                StyleId = "0",
                                Text = "0",
                                TextColor = Color.White,
                                CornerRadius = 10
                    };

                            btn.Clicked += BtnAdd_Clicked;

                            buttons.Children.Add(btn, j, i);
                        }
                        else if (j == 2)
                        {

                            Button btn = new Button()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.DarkSlateGray,
                                Text = "DEL",
                                TextColor = Color.White,
                                CornerRadius = 10,

                            };

                            btn.Clicked += BtnRemove_Clicked;

                            buttons.Children.Add(btn, j, i);
                        }

                    }
                }
            }
        }
    }
}