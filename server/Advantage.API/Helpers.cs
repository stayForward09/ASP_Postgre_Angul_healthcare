using System;
using System.Collections.Generic;
using Advantage.API;
using System.Linq;

namespace Advantage.API
{
    public class Helpers
    {
        private static Random _rand = new Random();


        // get random items
        internal static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }//end GetRandom



        // generate unique customer nae
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = bizPrefix.Count + bizSuffix.Count;

            if(names.Count >= maxNames){
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }

            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            var bizName = prefix + suffix;

            if(names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
                
            }
            return bizName;
        }//end MakeUniqueCustomerName



        // generate customer email
        internal static string MakeCustomerEmail(string name)
        {
            return $"contact@{name.ToLower()}.com";
        }//end MakeCustomerEmail



        // list of states
        internal static readonly List<string> useStates = new List<string>()
        {
            "AK", "AL","AZ",  "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };



        // list of prefix
        private static readonly List<string> bizPrefix = new List<string>()
        {
            "ABC",
            "XYZ",
            "Acme",
            "MainSt",
            "Ready",
            "Magic",
            "Fluent",
            "Peak",
            "Forward",
            "Enterprise",
            "Sales"
        };//end prefix



        // list of suffix
        private static readonly List<string> bizSuffix = new List<string>()
        {
            "Co",
            "Corp",
            "Holdings",
            "Corporation",
            "Movers",
            "Cleaners",
            "Bakery",
            "Apparel",
            "Rentals",
            "Storage",
            "Transit",
            "Logistics"
        };//end suffix



        // generate random datetime for placed
        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }//end GetRandomOrderPlaced



        // generate random datetime for completed
        public static DateTime? GetRandomOrderCompleted(DateTime placed)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - placed;

            if (timePassed < minLeadTime)
            {
                return null;
            }

            return placed.AddHours(_rand.Next(10, 90));
        }//end GetRandomOrderCompleted



        // select random customers
        public static Customer GetRandomCustomer(ApiContext ctx)
        {
            var randomId = _rand.Next(1, ctx.Customers.Count());
            return ctx.Customers.First(c => c.Id == randomId);
        }//end GetRandomCustomer



        // generate random Total
        public static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 1000);
        }// end GetRandomOrderTotal



        // select random state
        public static string GetRandomState()
        {
            return GetRandom(useStates);
        }//end GetRandomState


    }//end class

}//end namespace