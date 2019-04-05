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
        public int playerTurn = 1;
        public const string CONVERTED = "converted.txt";

        List<KeyValuePair<string, string>> checkouts;
        List<Checkout> checkoutList;
        List<CheckoutStrings> convertJSON;

        public MatchPage()
        {
            InitializeComponent();

        }

        public MatchPage(MatchSettings ms)
        {

            InitializeComponent();
            matchSettings = ms;
            UISetup();
            SetupCheckouts();

        }

        public void SetupCheckouts()
        {

            Task task1 = Task.Factory.StartNew(() =>
            {
                //try
                //{
                    MongoService mg = new MongoService();
                    checkoutList = mg.GetAllData();

                
                foreach (var c in checkoutList)
                {
                    Debug.WriteLine("MATCHPAGE: " + c.score + ": " + c.checkoutString);
                }

                //}
                //catch (Exception e)
                //{
                //    Debug.Write(e.StackTrace);
                //    checkouts = new List<KeyValuePair<string, string>>();
                //    string key;
                //    string line;
                //    string checkout;
                //    const string NAME = "MobileApps2Project.checkouts.txt";
                //    convertJSON = new List<CheckoutStrings>();

                //    Assembly assembly = Assembly.GetExecutingAssembly();

                //    using (Stream stream = assembly.GetManifestResourceStream(NAME))
                //    {
                //        StreamReader file = new StreamReader(stream);

                //        while ((line = file.ReadLine()) != null)
                //        {
                //            key = line.Substring(0, 3);
                //            key = Regex.Replace(key, @"[^0-9a-zA-Z]+", "");
                //            checkout = line.Substring(3);
                //            checkout = Regex.Replace(checkout, @"[^0-9a-zA-Z]+", " ");
                //            checkout = checkout.Trim();

                //            checkouts.Add(new KeyValuePair<string, string>(key, checkout));

                //            CheckoutStrings c = new CheckoutStrings(key, checkout);
                //            convertJSON.Add(c);
                //            Debug.WriteLine("NON-JSON");
                //        }
                //    }
                //}


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

                p1Score = Convert.ToInt32(l1.Text) - Convert.ToInt32(score.Text);
                l1.Text = p1Score.ToString();
                checkout = searchCheckouts(l1.Text);
                check1.Text = checkout;
                score.Text = "";
                playerTurn = 2;

                

            }
            else
            {
                p2Score = Convert.ToInt32(l2.Text) - Convert.ToInt32(score.Text);
                l2.Text = p2Score.ToString();
                checkout = searchCheckouts(l2.Text);
                check2.Text = checkout;
                score.Text = "";
                playerTurn = 1;
            }

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

                foreach (var x in checkouts)
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
            //if(score.Text.Length < 4)
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
            l2.Text = matchSettings.startScore;

            Debug.WriteLine(matchSettings.p1Name);
            Debug.WriteLine(matchSettings.p2Name);
            Debug.WriteLine(matchSettings.startScore);

            score = new Entry()
            {
                Placeholder = "SCORE",
                WidthRequest = 150,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            buttons.Children.Add(score, 1, 0);
            int counter = 0;

            Button enter = new Button()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Blue,
                StyleId = "0",
                Text = "Enter",
                TextColor = Color.White
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
                            TextColor = Color.White
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
                                TextColor = Color.White
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
                                TextColor = Color.White
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