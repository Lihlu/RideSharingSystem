using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Models;

namespace RideSharingSystem.Interfaces
{
    interface IAuth
    {
        User Login(string email, string password);
        void RegisterPassenger(Passenger passenger);
        void RegisterDriver(Driver driver);
        void Logout();
    }
}
