using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    class Driver : User
    {
        public bool IsAvailable { get; set; }
        public double Earnings { get; set; }

        public Driver(string name, string email, string password) : base(name, email, password, "Driver")
        {
            IsAvailable = true;
            Earnings = 0;
        }
    }
}
