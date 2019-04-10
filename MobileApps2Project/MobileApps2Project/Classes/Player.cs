using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApps2Project.Classes
{
    class Player
    {
        public string name { get; set; }
        public int dartNumber { get; set; }
        public double average { get; set; }
        public int setsWon { get; set; }
        public int legsWon { get; set; }
        public int lastScore { get; set; }

        public Player(string n)
        {
            name = n;
            dartNumber = 0;
            average = 0.00;
            setsWon = 0;
            legsWon = 0;
            lastScore = 0;
        }
    }
}
