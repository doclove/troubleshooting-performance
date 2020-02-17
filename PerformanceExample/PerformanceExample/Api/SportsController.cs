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

        // PUT api/<controller>/5
        [HttpPut("placeSanFranBet/")]
        public BookInfo BetSanFran([FromBody]decimal amount)
        {
            var info = new BookInfo();
            var betService = new BetService();
            betService.AddBet(new Bet {Amount = amount, BetDate = DateTime.Now, CurrencyType = 1, Team = 2});
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
            betService.AddBet(new Bet {Amount = amount, BetDate = DateTime.Now, CurrencyType = 1, Team = 1});
            var bets = betService.GetBets();
            info = GetBookInfo(bets);

            return info;
        }

        private BookInfo GetBookInfo(List<Bet> bets)
        {
            var bookInfo = new BookInfo();
            decimal kc = 0;
            foreach (var bet in bets)
            {
                bookInfo.AmountBet += bet.Amount;
                if (bet.Team == 1)
                {
                    kc -= .5m;
                }
                else
                {
                    kc += .5m;
                }
            }

            bookInfo.KansasOdds = kc > 0 ? "+" + kc : kc.ToString();
            return bookInfo;
        }
    }
}
