using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Models;
using RideSharingSystem.Services;

namespace RideSharingSystem.Pages
{
    static class PassengerPages
    {
        public static void MainMenu(Passenger passenger)
        {
            string[] menuOptions = { "Request a Ride (Enter pickup & drop-off location)", "View Wallet Balance", "View Ride History", "Logout" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Hello, {passenger.Name}");
                Console.WriteLine("Please use the up and down arrow keys to navigate, the 'Enter' to select\n");

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
                        RequestRideScreen(passenger);

                    }
                    else if (selectedIndex == 1)
                    {
                        ViewWalletScreen(passenger);
                    }
                    else if (selectedIndex == 2)
                    {
                        ViewRideHistoryScreen(passenger);
                    }
                    else if (selectedIndex == 3)
                    {
                        AuthService.GetInstance().Logout();
                    }
                    break;
                }
            }
        }

        public static void RequestRideScreen(Passenger passenger)
        {
            Console.WriteLine("Request a ride\n");
            try
            {
                Console.Write("Please enter your pick up location: ");
                string? pickUpLocation = Console.ReadLine();

                Console.Write("Please enter your destination: ");
                string? dropOffLocation = Console.ReadLine();

                // Simulating the distance between the 2 locations
                Random random = new Random();
                int kms = random.Next(0, 10);
                double price = 5.50 * kms;

                if (passenger.Wallet >= price)
                {
                    RideService.GetInstance().RequestRide(passenger.Email, pickUpLocation!, dropOffLocation!, kms);
                    AuthService.GetInstance().DeductFunds(passenger.Email, price);
                    Console.WriteLine("Your ride has been requested");
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Please make a deposit");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Viewing Passengers wallet
        // Passenger has the option to make a deposit
        public static void ViewWalletScreen(Passenger passenger)
        {
            Console.WriteLine($"Viewing {passenger.Name}\'s Wallet\n");

            try
            {

                Console.WriteLine($"Current Balance: R{passenger.Wallet}");

                Console.WriteLine("Would you like to make a deposit? \n1. Yes \n2. No");
                string? response = Console.ReadLine();

                if (response != null && response == "1")
                {
                    Console.Write("Please enter the amount you wish to deposit: ");
                    double depositAmount = Convert.ToDouble(Console.ReadLine());

                    AuthService.GetInstance().Deposit(depositAmount, passenger.Email);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Displaying passenger ride history
        public static void ViewRideHistoryScreen(Passenger passenger)
        {
            List<Ride> rideHistory = RideService.GetInstance().GetRideHistory(passenger.Email);

            Console.WriteLine($"Viewing {passenger.Name}\'s ride history");

            foreach (Ride ride in rideHistory)
            {
                Console.WriteLine($"From: {ride.PickUpLocation}, To: {ride.DropOffLocation} ");
            }

            Console.WriteLine("End of List");
        }
    }
}
