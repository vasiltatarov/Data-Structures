using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var bag = new OrderedBag<int>();

            foreach (var cookie in cookies)
            {
                bag.Add(cookie);
            }

            var currentMinSweetnes = bag.GetFirst();
            var steps = 0;

            while (currentMinSweetnes < k && bag.Count > 1)
            {
                var leastSweetCookie = bag.RemoveFirst();
                var secondLeastSweetCookie = bag.RemoveFirst();

                var combined = leastSweetCookie + (2 * secondLeastSweetCookie);

                bag.Add(combined);
                currentMinSweetnes = bag.GetFirst();
                steps++;
            }

            return currentMinSweetnes < k ? -1 : steps;
        }
    }
}
