﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApps2Project
{
    public class MatchSettings
    {
        public string p1Name;
        public string p2Name;
        public string startScore;


        public MatchSettings(string p1,string p2, string ss)
        {
            p1Name = p1;
            p2Name = p2;
            startScore = ss;
        }
    }
}