using MobileApps2Project.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApps2Project.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Stats : ContentPage
	{
        ObservableCollection<GameStats> gameStats;

        public Stats ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);

            toolbarStk.BackgroundColor = Color.FromHex("#3f7247");
            toolbarStk.HeightRequest = 55;
            toolbarStk.HorizontalOptions = LayoutOptions.FillAndExpand;

            bar.Text = "Game Statistics";
            bar.HorizontalTextAlignment = TextAlignment.Start;
            bar.Margin = 15;
            bar.TextColor = Color.White;
            bar.FontSize = 18;
            bar.FontAttributes = FontAttributes.Bold;

            //refresh.HorizontalOptions = LayoutOptions.End;

            //getMongoData();
            //new Color(Color.HSBtoRGB((float)Math.random(), (float)Math.random, 0.5F + ((float)Math.random()) / 2F));
            
        }

        public void getMongoData()
        {
            gameStats = new ObservableCollection<GameStats>();
            MongoService ms = new MongoService();
            List<GameStats> listdata = ms.GetAllStats();

            foreach (var c in listdata)
            {
                gameStats.Add(c);
            }

            StatsView.ItemsSource = gameStats;  

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            getMongoData();
        }
    }



}