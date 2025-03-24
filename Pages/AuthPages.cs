using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Models;
using RideSharingSystem.Services;

namespace RideSharingSystem.Pages
{
    class AuthPages
    {
        public static void LoginPage()
        {
            Console.Clear();
            Console.Write("Please enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();

            if (email != null && password != null)
            {
                User user = AuthService.GetInstance().Login(email, password);

                if (user != null)
                {
                    if (user.Role == "Passenger")
                    {
                        PassengerPages.MainMenu(user as Passenger);
                    }
                    else
                    {
                        DriverPages.MainMenu(user as Driver);
                    }
                }

            }
        }

        public static void RegistrationPage(string role)
        {
            Console.Clear();
            Console.WriteLine($"Register as a {role}\n");
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Please enter you email address: ");
            string email = Console.ReadLine();
            Console.Write("Please create a password: ");
            string password = Console.ReadLine();
            Console.Write("Please confirm your password: ");
            string confirmPassword = Console.ReadLine();


            if (password == confirmPassword)
            {

                if (role == "Passenger")
                {
                    Passenger passenger = new Passenger(name, email, password, 100.00);
                    AuthService.GetInstance().RegisterPassenger(passenger);

                }
                else if (role == "Driver")
                {
                    Driver driver = new Driver(name, email, password);
                    AuthService.GetInstance().RegisterDriver(driver);
                }
            }
            else
            {
                Console.WriteLine("Password do not match");
            }
        }
    }
}
