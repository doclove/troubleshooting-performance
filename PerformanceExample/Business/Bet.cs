using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Business
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }
        public decimal Amount { get; set; }

        public int TeamId { get; set; } //1=KC, 2=SF
        public DateTime BetDate { get; set; }
        public int CurrencyType { get; set; } //1=USD, 2=EUR

        public Team Team { get; set; }
    }
}
