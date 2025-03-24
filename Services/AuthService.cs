using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RideSharingSystem.Interfaces;
using RideSharingSystem.Models;

namespace RideSharingSystem.Services
{
    class AuthService : IAuth
    {
        private List<User> _users = new List<User>();
        private List<Passenger> _passengers = new List<Passenger>();
        private List<Driver> _drivers = new List<Driver>();
        private static User? _currentUser;
        private string _usersFileName = "Users.json";
        private string _passengersFileName = "passengers.json";
        private string _driversFileName = "drivers.json";


        private static AuthService _instance = new AuthService();
        private AuthService()
        {
            LoadData();
        }

        public static AuthService GetInstance()
        {
            return _instance;
        }

        public static User? GetCurrentUser()
        {
            return _currentUser;
        }

        public void RegisterPassenger(Passenger passenger)
        {
            LoadData();
            if (_users.Any(u => u.Email == passenger.Email))
            {
                Console.WriteLine("The email is already in use.");
                return;
            }

            _users.Add(passenger);
            _passengers.Add(passenger);
            SaveData();
            Console.WriteLine("Your account was successfully created!");
        }

        public void RegisterDriver(Driver driver)
        {
            LoadData();
            if (_users.Any(u => u.Email == driver.Email))
            {
                Console.WriteLine("The email is already in use.");
                return;
            }

            _users.Add(driver);
            _drivers.Add(driver);
            SaveData();
            Console.WriteLine("Your account was successfully created!");
        }

        public User Login(string email, string password)
        {
            LoadData();
            User? user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Invalid email or password.");
                return null;
            }

            if (user.Role == "Passenger")
            {
                Passenger? passenger = _passengers.FirstOrDefault(p => p.Email == email && p.Password == password);
                return passenger;
            }
            else
            {
                Driver? driver = _drivers.FirstOrDefault(d => d.Email == email && d.Password == password);
                return driver;
            }
        }

        public void Logout()
        {
            _currentUser = null;
            Console.WriteLine("Logged out successfully");
        }

        public void SaveData()
        {
            string usersString = JsonSerializer.Serialize(_users);
            string passengersString = JsonSerializer.Serialize(_passengers);
            string driversString = JsonSerializer.Serialize(_drivers);

            File.WriteAllText(_usersFileName, usersString);
            File.WriteAllText(_passengersFileName, passengersString);
            File.WriteAllText(_driversFileName, driversString);
        }

        public void LoadData()
        {
            try
            {

                if (File.Exists(_usersFileName))
                {
                    string userData = File.ReadAllText(_usersFileName);
                    _users = JsonSerializer.Deserialize<List<User>>(userData);
                }



                if (File.Exists(_passengersFileName))
                {
                    string data = File.ReadAllText(_passengersFileName);
                    _passengers = JsonSerializer.Deserialize<List<Passenger>>(data);
                }

                if (File.Exists(_driversFileName))
                {
                    string data = File.ReadAllText(_driversFileName);
                    _drivers = JsonSerializer.Deserialize<List<Driver>>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
