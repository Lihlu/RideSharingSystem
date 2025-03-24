using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RideSharingSystem.Interfaces;
using RideSharingSystem.Models;

namespace RideSharingSystem.Services
{
    class RideService : IRideable
    {
        private List<Ride> _rides = new List<Ride>();
        private readonly string _fileName = "rides.json";
        private static RideService _instance = new RideService();

        private RideService()
        {
            LoadData();
        }

        public static RideService GetInstance()
        {
            return _instance;
        }

        public void RequestRide(string passengerEmail, string pickUpLocation, string dropOffLocation, int kms)
        {
            LoadData();
            double price = 5.50 * kms;
            Ride newRide = new Ride(_rides.Count + 1, passengerEmail, pickUpLocation, dropOffLocation, price);
            _rides.Add(newRide);
            SaveData();
        }

        public void AcceptRide(Driver driver, int rideId)
        {
            LoadData();
            Ride? ride = _rides.FirstOrDefault(ride => ride.Id == rideId && !ride.IsCompleted);
            if (ride != null && driver.IsAvailable)
            {
                ride.Driver = driver;
                driver.IsAvailable = false;
                Console.WriteLine($"Ride accepted by {driver.Name}");
                SaveData();
            }
            else
            {
                Console.WriteLine("Sorry, we could not complete your request");
            }
        }

        public void CompleteRide(int rideId)
        {
            LoadData();
            Ride? ride = _rides.FirstOrDefault(ride => ride.Id == rideId);

            if (ride != null && ride.Driver != null)
            {
                ride.IsCompleted = true;
                ride.Driver.IsAvailable = true;
                ride.Driver.Earnings += ride.Price;
                AuthService.GetInstance().UpdateEarnings(ride.Driver.Email, ride.Price);
                Console.WriteLine("Ride completed");
                SaveData();
            }
        }

        public List<Ride> GetRequests()
        {
            List<Ride> requests = (from ride in _rides
                                   where !ride.IsCompleted && ride.Driver == null
                                   select ride).ToList();

            return requests;
        }

        public List<Ride> GetOngoingRides()
        {
            List<Ride> ongoing = (from ride in _rides
                                  where !ride.IsCompleted && ride != null
                                  select ride).ToList();
            return ongoing;
        }

        public List<Ride> GetRideHistory(string email)
        {
            List<Ride> rideHistory = (from ride in _rides
                                      where ride.PassengerEmail == email
                                      select ride).ToList();

            return rideHistory;
        }

        public void SaveData()
        {
            string jsonString = JsonSerializer.Serialize(_rides);

            File.WriteAllText(_fileName, jsonString);
        }

        public void LoadData()
        {
            try
            {

                if (File.Exists(_fileName))
                {
                    string data = File.ReadAllText(_fileName);

                    _rides = JsonSerializer.Deserialize<List<Ride>>(data)!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
