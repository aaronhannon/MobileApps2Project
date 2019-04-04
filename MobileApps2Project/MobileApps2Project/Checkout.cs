using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApps2Project
{
    class Checkout
    {
        public object id { get; set; }
        public string score { get; set; }
        public string checkoutString { get; set; }

        public Checkout(object i,string s, string c)
        {
            id = i;
            score = s;
            checkoutString = c;
        }

    }
}
