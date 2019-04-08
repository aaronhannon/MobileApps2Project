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
        public string player2Sets { get; set; }
        public string winner { get; set; }

        public GameStats(string p1n, string p2n, string p1s, string p2s, string win)
        {
            player1Name = p1n;
            player2Name = p2n;
            player1Sets = p1s;
            player2Sets = p2s;
            winner = win;
        }


    }
}
