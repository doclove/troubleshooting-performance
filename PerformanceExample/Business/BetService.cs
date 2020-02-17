using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class BetService
    {
        public List<Bet> GetBets()
        {
            var context = new SportsContext();
            var bets = context.Bets.ToList();

            return bets;
        }

        public void AddBet(Bet bet)
        {
            var context = new SportsContext();
            context.Bets.Add(bet);
            context.SaveChanges();
        }
    }
}
