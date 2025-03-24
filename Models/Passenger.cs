using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    class Passenger : User
    {
        public double Wallet { get; set; }
        public Passenger(string name, string email, string password, double wallet) : base(name, email, password, "Passenger")
        {
            Wallet = wallet;
        }
    }
}
