using System;
using ToDoListManager.Models;

namespace ToDoListManager
{
    class OperationOption
    {
        public static void Add(ApplicationDbContext _context, int UserId)
        {
            Console.WriteLine("Input your task:");
            while (true)
            {
                string taskinput = Console.ReadLine()!;
                if (string.IsNullOrEmpty(taskinput) || string.IsNullOrWhiteSpace(taskinput))
                {
                    ErrorMessage.PlankText();
                }
                else
                {
                    var task = new TasksInfo { Tasks = taskinput, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, UserInfoId = UserId, IsCompleted = false };
                    _context.TasksInfos.Add(task);
                    _context.SaveChanges();
                    break;
                }
            }
        }
        public static void Edit(ApplicationDbContext _context)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Input your task Id:");
                    string taskInput = Console.ReadLine()!;
                    if (!int.TryParse(taskInput, out int taskId))
                    {
                        ErrorMessage.IntError();
                    }
                    else
                    {
                        var task = _context.TasksInfos.Find(taskId);
                        if (task == null)
                        {
                            ErrorMessage.InvalidID();
                        }
                        else
                        {
                            Console.WriteLine($"You Selected this task: [{task.Tasks}]");
                            while (true)
                            {
                                Console.WriteLine("Write your edited task.");
                                string taskEdition = Console.ReadLine()!;
                                if (string.IsNullOrWhiteSpace(taskEdition) || string.IsNullOrEmpty(taskEdition))
                                {
                                    ErrorMessage.PlankText();
                                }
                                else
                                {
                                    task.Tasks = taskEdition;
                                    _context.SaveChanges();
                                    break;
                                }
                            }
                            break;
                        }                                                
                    }                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
            }
        }
        public static void Remove(ApplicationDbContext _context)
        {
            try
            {
                Console.WriteLine("Input your task Id:");
                while (true)
                {
                    string taskInput = Console.ReadLine()!;
                    if (!int.TryParse(taskInput, out int taskId))
                    {
                        ErrorMessage.IntError();
                    }
                    else
                    {
                        var task = _context.TasksInfos.Find(taskId);
                        if (task == null)
                        {
                            ErrorMessage.InvalidID();
                        }
                        else
                        {
                            _context.TasksInfos.Remove(task);
                            _context.SaveChanges();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
            }
        }
        public static void MarkCompleted(ApplicationDbContext _context)
        {
            Console.WriteLine("Input your task Id:");
            while (true)
            {
                string taskInput = Console.ReadLine()!;
                if (!int.TryParse(taskInput, out int taskId))
                {
                    ErrorMessage.IntError();
                }
                else
                {
                    var taskCompleted = _context.TasksInfos.Find(taskId);
                    if (taskCompleted == null)
                    {
                        ErrorMessage.InvalidID();
                    }
                    else
                    {
                        taskCompleted.IsCompleted = true;
                        taskCompleted.UpdatedAt = DateTime.Now;
                        _context.SaveChanges();
                        Console.WriteLine($"Task {taskInput} marked as completed.");
                        break;
                    }
                }
            }
        }
    }
}