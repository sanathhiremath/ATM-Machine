using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Machine.Models
{
    public class Transaction
    {
        public int IsDeposit { get; set; }
        public int DepositAmt { get; set; }
        public int WithdrawalAmt { get; set; }
        public int Balance { get; set; }
        public long CardNo { get; set; }
    }
}