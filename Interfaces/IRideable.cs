using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Models;

namespace RideSharingSystem.Interfaces
{
    interface IRideable
    {
        void RequestRide(string passengerEmail, string pickUpLocation, string dropOffLocation);
        void AcceptRide(Driver driver, int rideId);
        void CompleteRide(int rideId);
    }
}
