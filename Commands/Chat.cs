using System;
using System.Net;
using Microsoft.VisualBasic;
using ToDoListManager.Models;

namespace ToDoListManager
{
    class Chat
    {        
        public static string Greet()
        {
            Console.WriteLine("Welcome to your Personal Task Manager!");
            Console.WriteLine("Do you have an Account ? \"Y/N\"");
            
            while (true)
            {
                string Sign = Console.ReadLine()!;
                if (Sign.ToLower() == "y" || Sign.ToLower() == "yes")
                {
                    return "signin";
                }
                else if (Sign.ToLower() == "n" || Sign.ToLower() == "no")
                {
                    return "signup";
                }
                else
                {
                    Console.WriteLine("Answer with Y/N!");
                }
            }
        }
    }
}