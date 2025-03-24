using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    class Ride
    {
        public int Id { get; set; }
        public string PassengerEmail { get; set; }
        public Driver Driver { get; set; }
        public string? PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public double Price { get; set; }
        public bool IsCompleted { get; set; }

        public Ride(int id, string passengerEmail, string pickUpLocation, string dropOffLocation, double price)
        {
            Id = id;
            PassengerEmail = passengerEmail;
            PickUpLocation = pickUpLocation;
            DropOffLocation = dropOffLocation;
            Price = price;
            IsCompleted = false;
        }
    }
}
