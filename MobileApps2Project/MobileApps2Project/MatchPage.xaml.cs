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

		public MatchPage (MatchSettings ms)
		{

			InitializeComponent ();

            matchSettings = ms;

            textSettings.Text = matchSettings.p1Name + "askdlfhalskdjfh";

            Debug.WriteLine(matchSettings.p1Name);
            Debug.WriteLine(matchSettings.p2Name);
            Debug.WriteLine(matchSettings.startScore);
        }
	}
}