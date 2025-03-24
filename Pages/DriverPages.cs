using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Models;
using RideSharingSystem.Services;

namespace RideSharingSystem.Pages
{
    class DriverPages
    {
        public static void MainMenu(Driver driver)
        {
            string[] menuOptions = { "View Available Ride Requests", "Accept a Ride", "Complete a Ride", "View Earnings", "Logout" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Hello, {driver.Name}");
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
                        ViewAvailableRequestsPage(driver);

                    }
                    else if (selectedIndex == 2)
                    {
                        CompleteRidePage();
                    }
                    else if (selectedIndex == 3)
                    {
                        ViewEarningsPage(driver);
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

        public static void ViewAvailableRequestsPage(Driver driver)
        {
            List<Ride> requests = RideService.GetInstance().GetRequests();
            int selectedIndex = 0;



            while (true)
            {
                Console.Clear();

                for (int i = 0; i < requests.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"> From: {requests[i].PickUpLocation} To: {requests[i].DropOffLocation} Price: {requests[i].Price}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"> From: {requests[i].PickUpLocation} To: {requests[i].DropOffLocation} Price: {requests[i].Price}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? requests.Count - 1 : selectedIndex - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == requests.Count - 1) ? 0 : selectedIndex + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    RideService.GetInstance().AcceptRide(driver, requests[selectedIndex].Id);
                    break;
                }
            }

        }

        public static void CompleteRidePage()
        {
            List<Ride> ongoing = RideService.GetInstance().GetOngoingRides();
            int selectedIndex = 0;



            while (true)
            {
                Console.Clear();

                for (int i = 0; i < ongoing.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"> From: {ongoing[i].PickUpLocation} To: {ongoing[i].DropOffLocation} Price: {ongoing[i].Price}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"> From: {ongoing[i].PickUpLocation} To: {ongoing[i].DropOffLocation} Price: {ongoing[i].Price}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? ongoing.Count - 1 : selectedIndex - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == ongoing.Count - 1) ? 0 : selectedIndex + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    RideService.GetInstance().CompleteRide(ongoing[selectedIndex].Id);
                    break;
                }
            }

        }

        public static void ViewEarningsPage(Driver driver)
        {
            Console.WriteLine($"Earnings: R{driver.Earnings}");
        }
    }
}
