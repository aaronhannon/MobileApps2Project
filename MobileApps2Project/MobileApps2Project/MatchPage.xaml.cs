using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApps2Project
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchPage : ContentPage
	{
        public MatchSettings matchSettings;

        public MatchPage(MatchSettings ms)
        {

            InitializeComponent();

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


            matchSettings = ms;

            //textSettings.Text = matchSettings.p1Name + "askdlfhalskdjfh";

            l1.Text = matchSettings.startScore;
            l2.Text = matchSettings.startScore;

            Debug.WriteLine(matchSettings.p1Name);
            Debug.WriteLine(matchSettings.p2Name);
            Debug.WriteLine(matchSettings.startScore);

            int counter = 0;

            //ROWS
            for (int i = 1; i < 5; i++)
            {
                //COLS
                for (int j = 0; j < 3; j++)
                {
                    counter++;

                    if (i < 4)
                    {
                        buttons.Children.Add(
                        new Button()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.DarkSlateGray,
                            Text = counter.ToString(),
                            TextColor = Color.White
                        }, j, i);

                    }
                    else {
                        if(j == 1)
                        {
                            buttons.Children.Add(
                            new Button()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.DarkSlateGray,
                                Text = "0",
                                TextColor = Color.White
                            }, j, i);
                        }
                        else if (j == 2)
                        {
                            buttons.Children.Add(
                            new Button()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.DarkSlateGray,
                                Text = "DEL",
                                TextColor = Color.White
                            }, j, i);
                        }

                    }
                }
            }

            



        }
	}
}