using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ToDoListManager.Models;


namespace ToDoListManager
{
    class AuthService
    {
        public static void SignUp(ApplicationDbContext _context)
        {
            while (true)
            {
                Console.WriteLine("Enter Your Username:");
                string UsernameSignUp = Console.ReadLine()!;
                bool IsDuplicateUsername = _context.UserInfos.Any(m => m.Username == UsernameSignUp);

                if (IsDuplicateUsername)
                {
                    Console.WriteLine("Username Already Taken, Please try again");
                }
                else if (string.IsNullOrWhiteSpace(UsernameSignUp) || string.IsNullOrEmpty(UsernameSignUp))
                {
                    Console.WriteLine("Username is Empty, Please provide Username");
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("Enter Your Password:");
                        string PasswordSignUp = Console.ReadLine()!;
                        if (string.IsNullOrWhiteSpace(PasswordSignUp) || string.IsNullOrEmpty(PasswordSignUp))
                        {
                            Console.WriteLine("Password is Empty, Please provide Password");
                        }
                        else
                        {
                            string Hashed = PasswordService.HashPassword(PasswordSignUp);
                            var NewUser = new UserInfo { Username = UsernameSignUp, PasswordHash = Hashed };
                            _context.UserInfos.Add(NewUser);
                            _context.SaveChanges();
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public static (string Username, bool IsLoggedIn) SignIn(ApplicationDbContext _context)
        {
            bool IsLoggedIn = false;
            while (!IsLoggedIn)
            {
                while (true)
                {
                    Console.WriteLine("Enter Your Username:");
                    string UsernameSignIn = Console.ReadLine()!;
                    if (string.IsNullOrEmpty(UsernameSignIn) || string.IsNullOrWhiteSpace(UsernameSignIn))
                    {
                        ErrorMessage.PlankText();
                    }
                    else
                    {
                        Console.WriteLine("Enter Your Password:");
                        string PasswordSignIn = Console.ReadLine()!;
                        bool IsUsernameValid = _context.UserInfos.Any(m => m.Username == UsernameSignIn);
                        var UserPassword = _context.UserInfos.FirstOrDefault(m => m.Username == UsernameSignIn);
                        bool IsPasswordValid = UserPassword != null && PasswordService.VerifyPassword(PasswordSignIn, UserPassword.PasswordHash);
                        if (!IsUsernameValid || !IsPasswordValid)
                        {
                            Console.WriteLine("Incorrect Username or Password !");
                        }
                        else
                        {
                            Console.WriteLine($"You are logged in {UsernameSignIn}");
                            IsLoggedIn = true;
                            return (UsernameSignIn, IsLoggedIn);
                        }
                        break;
                    }
                }                
            }
            // In case the loop is somehow exited without logging in, return default values
            return (string.Empty, false);
        }
    }
}