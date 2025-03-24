using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Models;
using RideSharingSystem.Services;

namespace RideSharingSystem.Pages
{
    class PassengerPages
    {
        public static void MainMenu(Passenger passenger)
        {
            string[] menuOptions = { "Request a Ride (Enter pickup & drop-off location)", "View Wallet Balance", "Add Funds to Wallet", "View Ride History", "Rate a Driver", "Logout" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();

                Console.WriteLine();

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($" {menuOptions[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (selectedIndex == 0)
                    {
                        RequestRideScreen(passenger.Email);

                    }
                    else if (selectedIndex == 1)
                    {
                        ViewWalletScreen(passenger);
                    }
                    else if (selectedIndex == 3)
                    {
                        ViewRideHistoryScreen(passenger.Email);
                    }
                    else if (selectedIndex == 6)
                    {
                        AuthService.GetInstance().Logout();
                    }
                    Console.Write("Goodbye");
                    break;
                }
            }
        }

        public static void RequestRideScreen(string passengerEmail)
        {
            Console.Write("Please enter your pick up location: ");
            string? pickUpLocation = Console.ReadLine();

            Console.Write("Please enter your destination: ");
            string? dropOffLocation = Console.ReadLine();

            RideService.GetInstance().RequestRide(passengerEmail, pickUpLocation, dropOffLocation);
        }

        public static void ViewWalletScreen(Passenger passenger)
        {
            Console.WriteLine($"Current Balance: R{passenger.Wallet}");

            Console.WriteLine("Would you like to make a deposit? \n1. Yes \n2. No");
        }

        public static void ViewRideHistoryScreen(string passengerEmail)
        {
            List<Ride> rideHistory = RideService.GetInstance().GetRideHistory(passengerEmail);

            foreach (Ride ride in rideHistory)
            {
                Console.WriteLine($"From: {ride.PickUpLocation}, To: {ride.DropOffLocation} ");
            }
        }
    }
}
