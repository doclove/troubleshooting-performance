using Microsoft.EntityFrameworkCore;
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

        //Fix 1
        //public List<Bet> GetBets()
        //{
        //    var context = new SportsContext();
        //    var bets = context.Bets.Include("Team").ToList();

        //    return bets;
        //}

        //Fix 2
        //public IQueryable<Bet> GetBets()
        //{
        //    var context = new SportsContext();
        //    var bets = context.Bets.Include("Team");

        //    return bets;
        //}

        public void AddBet(Bet bet)
        {
            var context = new SportsContext();
            context.Bets.Add(bet);
            context.SaveChanges();
        }

        public string GetTeamName(int teamId)
        {
            var context = new SportsContext();
            var team = context.Teams.FirstOrDefault(t => t.TeamId == teamId);
            if (team != null)
                return team.Name;

            return string.Empty;
        }
    }
}
