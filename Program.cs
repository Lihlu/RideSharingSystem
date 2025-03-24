using RideSharingSystem.Pages;

string[] menuOptions = { "Register as Passenger", "Register as Driver", "Login", "Exit" };
int selectedIndex = 0;

while (true)
{
    Console.Clear();

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
            AuthPages.RegistrationPage("Passenger");

        }
        else if (selectedIndex == 1)
        {
            AuthPages.RegistrationPage("Driver");
        }
        else if (selectedIndex == 2)
        {
            AuthPages.LoginPage();
        }
        Console.Write("Goodbye");
        break;
    }
}
