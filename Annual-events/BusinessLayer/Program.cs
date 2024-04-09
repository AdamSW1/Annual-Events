using System;
using BusinessLayer;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();
        Console.WriteLine("Enter description:");
        string description = Console.ReadLine();
        Console.WriteLine("Enter age:");
        int age = Convert.ToInt32(Console.ReadLine());
        User newUser = new User(username, password, description, age);

        


    }
}
