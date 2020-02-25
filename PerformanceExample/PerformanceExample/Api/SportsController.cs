using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PerformanceExample.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PerformanceExample.Api
{
    [Route("api/[controller]")]
    public class SportsController : Controller
    {
        // GET: api/<controller>
        [HttpGet("getLatest")]
        public BookInfo GetLatest()
        {
            var info = new BookInfo();
            var betService = new BetService();
            var bets = betService.GetBets();
            info = GetBookInfo(bets);

            return info;
        }

        [HttpGet("getBets")]
        public List<BetModel> GetBets()
        {
            var betService = new BetService();
            var bets = betService.GetBets();
            var betModels = new List<BetModel>();

            foreach(var bet in bets)
            {
                var model = new BetModel();
                model.Amount = bet.Amount;
                model.TeamName = betService.GetTeamName(bet.TeamId);
                betModels.Add(model);
            }

            return betModels;
        }

        //Fix 1
        //[HttpGet("getBets")]
        //public List<BetModel> GetBets()
        //{
        //    var betService = new BetService();
        //    var bets = betService.GetBets();
        //    var betModels = new List<BetModel>();

        //    foreach (var bet in bets)
        //    {
        //        var model = new BetModel();
        //        model.Amount = bet.Amount;
        //        model.TeamName = bet.Team?.Name;
        //        betModels.Add(model);
        //    }

        //    return betModels;
        //}    

        //Fix 2
        //[HttpGet("getBets")]
        //public List<BetModel> GetBets()
        //{
        //    var betService = new BetService();
        //    var bets = betService.GetBets().Take(10);
        //    var betModels = new List<BetModel>();

        //    foreach (var bet in bets)
        //    {
        //        var model = new BetModel();
        //        model.Amount = bet.Amount;
        //        model.TeamName = bet.Team?.Name;
        //        betModels.Add(model);
        //    }

        //    return betModels;
        //}  

        // PUT api/<controller>/5
        [HttpPut("placeSanFranBet/")]
        public BookInfo BetSanFran([FromBody]decimal amount)
        {
            var info = new BookInfo();
            var betService = new BetService();
            betService.AddBet(new Bet {Amount = amount, BetDate = DateTime.Now, CurrencyType = 1, TeamId = 2});
            var bets = betService.GetBets();
            info = GetBookInfo(bets);

            return info;
        }

        // PUT api/<controller>/5
        [HttpPut("placeKCBet")]
        public BookInfo BetKC([FromBody]decimal amount)
        {
            var info = new BookInfo();
            var betService = new BetService();
            betService.AddBet(new Bet {Amount = amount, BetDate = DateTime.Now, CurrencyType = 1, TeamId = 1});
            var bets = betService.GetBets();
            info = GetBookInfo(bets);

            return info;
        }

        [HttpGet("setupProd")]
        public void SetupProd()
        {
            var info = new BookInfo();
            var betService = new BetService();
            for (int i = 0; i < 100000; i++)
            {
                var randomAmount = new Random(DateTime.Now.Millisecond).Next(0, 9999);
                var randomTeam = DateTime.Now.Millisecond % 2 == 0 ? 1 : 2;
                var randomCurrency = DateTime.Now.Millisecond % 5 == 0 ? 2 : 1;
                betService.AddBet(new Bet {Amount = randomAmount, BetDate = DateTime.Now, CurrencyType = randomCurrency, TeamId = randomTeam });
            }
        }

        private BookInfo GetBookInfo(List<Bet> bets)
        {
            var bookInfo = new BookInfo();
            double kc = 0;
            foreach (var bet in bets)
            {
                bookInfo.AmountBet += bet.Amount;
                if (bet.TeamId == 1)
                {
                    kc -= .5;
                }
                else
                {
                    kc += .5;
                }
            }
            bookInfo.KansasOdds = kc > 0 ? "+" + kc : kc.ToString();

            //var info = bets.GroupBy(b => b.TeamId)
            //    .Select(b => new
            //    {
            //        TeamId = b.Key,
            //        AmountBet = b.Sum(i => i.Amount),
            //        BetCount = b.Count()
            //    }).ToList();

            //bookInfo.AmountBet = info.Sum(i => i.AmountBet);
            //kc = (info.First(i => i.TeamId == 1).BetCount - info.First(i => i.TeamId != 1).BetCount) * .5;
            //bookInfo.KansasOdds = kc > 0 ? "+" + kc : kc.ToString();

            return bookInfo;
        }
    }
}
