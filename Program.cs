using System;
using System.Collections.Generic;
using ToDoListManager.Models;

namespace ToDoListManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();

            //Sign in and sign up Class
            string greetResult = Chat.Greet();

            string UsernameSign = null!;
            bool UserLoggedIn = false;
            if (greetResult == "signup")
            {
                AuthService.SignUp(context);
                var SignInInfo = AuthService.SignIn(context);
                UsernameSign = SignInInfo.Username;
                UserLoggedIn = SignInInfo.IsLoggedIn;
                Console.WriteLine($"[DEBUG] Username: {SignInInfo.Username}, IsLoggedIn: {SignInInfo.IsLoggedIn}");

            }
            else if (greetResult == "signin")
            {
                var SignInInfo = AuthService.SignIn(context);
                UsernameSign = SignInInfo.Username;
                UserLoggedIn = SignInInfo.IsLoggedIn;
                while (UserLoggedIn)
                {
                    var taskCountPending = context.TasksInfos
                                            .Where(t => t.UserInfo.Username == UsernameSign && t.IsCompleted == false)
                                            .Count();
                    var taskCountComplete = context.TasksInfos
                                            .Where(t => t.UserInfo.Username == UsernameSign && t.IsCompleted == true)
                                            .Count();
                    var tasksInfopendings = context.TasksInfos
                                            .Where(t => t.UserInfo.Username == UsernameSign && t.IsCompleted == false)
                                            .OrderBy(t => t.CreatedAt)
                                            .ToList();
                    var tasksInfoCompleted = context.TasksInfos
                                            .Where(t => t.UserInfo.Username == UsernameSign && t.IsCompleted == true)
                                            .OrderBy(t => t.CreatedAt)
                                            .ToList();
                    var UserId = context.UserInfos.SingleOrDefault(u => u.Username == UsernameSign);

                    Console.WriteLine($"--- Pending [{taskCountPending}] Tasks");
                    int PendingCount = 1;
                    foreach (var tasksInfopending in tasksInfopendings)
                    {
                        Console.WriteLine($"* Task[{PendingCount}]/Id[{tasksInfopending.Id}]: \"{tasksInfopending.Tasks}\"");
                        PendingCount++;
                    }

                    Console.WriteLine($"--- Completed [{taskCountComplete}] Tasks");
                    int CompletedCount = 1;
                    foreach (var taskInfopending in tasksInfoCompleted)
                    {
                        Console.WriteLine($"* Task[{CompletedCount}]/Id[{taskInfopending.Id}]: \"{taskInfopending.Tasks}\"");
                        CompletedCount++;
                    }

                    Console.WriteLine("1.Add new task.\n2.Edit task.\n3.Remove task.\n4.Mark task as completed.\n5.Logout.");
                    string UserOption = Console.ReadLine()!;
                    if (UserOption == "1")
                    {
                        OperationOption.Add(context, UserId!.Id);
                    }
                    else if (UserOption == "2")
                    {
                        OperationOption.Edit(context);
                    }
                    else if (UserOption == "3")
                    {
                        OperationOption.Remove(context);
                    }
                    else if (UserOption == "4")
                    {
                        OperationOption.MarkCompleted(context);
                    }
                    else if (UserOption == "5")
                    {
                        UserLoggedIn = false;
                        Console.WriteLine("You are Logged out");
                        break;
                    }
                }
            }                  
        }
    }
}
