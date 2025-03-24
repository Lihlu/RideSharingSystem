using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    class Wallet
    {
        public double Balance { get; set; }

        public Wallet(double initialBalance)
        {
            Balance = initialBalance;
        }

        public bool Deduct(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public void AddFunds(double amount)
        {
            Balance += amount;
        }
    }
}
