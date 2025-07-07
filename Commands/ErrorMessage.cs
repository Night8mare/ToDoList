using System;
using System.Net;
using Microsoft.VisualBasic;

namespace ToDoListManager
{
    class ErrorMessage
    {
        public static void IntError()
        {
            Console.WriteLine("Please provide an Interger");
        }
        public static void PlankText()
        {
            Console.WriteLine("Please provide your text");
        }
        public static void InvalidID()
        {
            Console.WriteLine("Id not Found");
        }
    }
}