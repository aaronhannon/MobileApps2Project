using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApps2Project.Classes
{
    class GameStats
    {
        public ObjectId Id { get; set; }
        public string player1Name { get; set; }
        public string player2Name { get; set; }
        public string player1Sets { get; set; }
        public string player1Legs { get; set; }
        public string player2Sets { get; set; }
        public string player2Legs { get; set; }
        public string player1Average { get; set; }
        public string player2Average { get; set; }
        public string player1Darts { get; set; }
        public string player2Darts { get; set; }
        public string winner { get; set; }

        public GameStats(string p1n, string p2n, string p1s,string p1l, string p2s,string p2l,string p1a,string p2a,string p1d,string p2d, string win)
        {
            player1Name = p1n;
            player2Name = p2n;
            player1Sets = p1s;
            player1Legs = p1l;
            player2Sets = p2s;
            player2Legs = p2l;
            player1Average = p1a;
            player2Average = p2a;
            player1Darts = p1d;
            player2Darts = p2d;
            winner = win;
        }


    }
}
