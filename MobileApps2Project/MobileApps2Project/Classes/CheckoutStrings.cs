using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApps2Project
{
    class CheckoutStrings
    {
        public string score { get; set; }
        public string checkoutString { get; set; }

        public CheckoutStrings(string s, string c)
        {
            score = s;
            checkoutString = c;
        }
    }
}
