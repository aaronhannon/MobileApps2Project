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
            
            getMongoData();
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

    }



}