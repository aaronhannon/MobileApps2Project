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
        public List<Checkout> checkouts { get; set; }

        public Checkout(object i, List<Checkout> s)
        {
            id = i;
            checkouts = s;
            //score = s;
            //checkoutString = c;
        }

        public Checkout(string s, string c)
        {
            score = s;
            checkoutString = c;
        }

    }
}
