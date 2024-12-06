using System;
using ToDoListApp;
using System.Collections.Generic;
using System.Data.SQLite;




//To do list namespace

namespace ToDoListApp
{



    //class for Database

    public static class DatabaseContainer
    {

        public static SQLiteConnection CreateDatabaseConnection()
        {
            //create or use the available db
            string connectionString = "Data Source=MyDatabase.db;";
            var connection = new SQLiteConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connected to database");
                
            }

            catch (Exception error)
            {
                Console.WriteLine($"Couldn't connect to database:{error.Message}");
            }

            return connection;
        }
    }

    

    //class for userData
    public static class UserManager
    {

        //dictionary to store UserName and Passwords
        private static  Dictionary<string,string> userData = new Dictionary<string,string>();


        //display Main menu
        public static void MainMenu()
        {
            Console.WriteLine("Welcome to Z - To Do List");
            Console.WriteLine();
            Console.WriteLine("Main menu Tab");
            Console.WriteLine("- - - - - - -");
            Console.WriteLine("0.Main Menu");
            Console.WriteLine("1.Register");
            Console.WriteLine("2.Login");
            Console.WriteLine("3.Exit");

            //prompt user
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                string? mainMenuOption = Console.ReadLine();

                switch (mainMenuOption)
                {
                    case "0":
                        Console.WriteLine();
                        Console.WriteLine("- - - - -");
                        Console.WriteLine("Currently in the Main menu tab");
                        break;
                    case "1":
                        RegisterUser();
                        break;

                    case "2":
                        LoginUser();
                        return;

                    case "3":
                        ExitApp();
                        return;

                    default:
                        Console.WriteLine("- - - - ");
                        Console.WriteLine("Invalid option!\nChoose one from the Main menu tab");
                        break;



                }

            }
            
        }

        //register User
        private static void RegisterUser()
        {

            //ask for username


            Console.WriteLine();
            Console.WriteLine("Register account");
            Console.WriteLine("- - - - - ");

            //keep looping
            while (true)
            {

                
                Console.WriteLine("Input username:");
                string? userName = Console.ReadLine();
                

                if (!string.IsNullOrWhiteSpace(userName))
                {

                    //Main menu 

                    if (userName == "0")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Returned to Main Menu");
                        Console.WriteLine("- - - - - -");
                        //call main menu function
                        Console.WriteLine();
                        MainMenu();
                        break;

                    }

                    if (userData.ContainsKey(userName))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Username taken!");
                        Console.WriteLine("Try again");
                        Console.WriteLine();
                        
                    }

                    else
                    {

                        Console.WriteLine();
                        Console.WriteLine("Input password:");

                        //ask for password if username is valid
                        string? password = Console.ReadLine();

                        //validate password
                        if (!string.IsNullOrWhiteSpace(password) && password.Length > 6)
                        {

                            //assign key and value to dictonary
                            userData[userName] = password;
                            
                            Console.WriteLine();
                            Console.WriteLine($"Welcome {userName}");
                            Console.WriteLine("- - - - - -");
                            
                            Console.WriteLine($"Navigate to the login section to continue");
                            

                            break;
                            
                            
                        }

                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("- - - - -");
                            Console.WriteLine("Invalid password");
                            Console.WriteLine("characters should be above six! and No whitespaces allowed!");
                            Console.WriteLine();
                        }
                        
                        
                    }

                }

                else
                {
                    
                    Console.WriteLine("Input a valid username no empty spaces allowed");
                    Console.WriteLine();
                    
                    
                }

            }


        }

        
        //login user - - - - - - 

        private static void LoginUser()
        {
            while (true)
            {
                //prompt for user name
                Console.WriteLine();
                Console.WriteLine("Input login account username:");
                string? userNameOnLogin = Console.ReadLine();


                if (!string.IsNullOrWhiteSpace(userNameOnLogin))
                {

                    //back to MainMenu

                    if (userNameOnLogin == "0")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Returned to Main Menu");
                        Console.WriteLine("- - - - - -");
                        Console.WriteLine();
                        //call Main - Menu
                        MainMenu();
                        break;

                    }


                    //search key and validate passwordr
                    if (userData.ContainsKey(userNameOnLogin))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Username data available!");
                        Console.WriteLine();

                        while (true)
                        {
                            
                            Console.WriteLine("Input account password:");
                            string? userLoginPassword = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(userLoginPassword))
                            {

                                string? userPassword = userLoginPassword;
                                if (userPassword == userData[userNameOnLogin])
                                {
                                    
                                    Console.WriteLine();
                                    Console.WriteLine($"Login status");
                                    Console.WriteLine("- - - - -");
                                    Console.WriteLine("Success!");
                                    Console.WriteLine("- - - - -");
                                    Console.WriteLine($"Welcome {userNameOnLogin}");
                                    
                                    Console.WriteLine();
                                    ToDoListManger.WelcomeUser();
                                    
                                    break;
                                }

                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Password Incorrect!");
                                    Console.WriteLine("Try again!");
                                    Console.WriteLine();

                                }
                            }

                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Username cannot be empty");
                            }

                           

                        }
                     
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Username could not be found!");
                        Console.WriteLine("Try again!");
                    }
                    
                }

                else
                {
                    
                    Console.WriteLine("Username cannot be empty");
                }
            }
        }

        //looking for user - - - - from Dictonary keys
        /*
        private static void SearchUser()
        {


            string searchUser = "Steve Aboll";

            if (userData.ContainsKey(searchUser))
            {
                Console.WriteLine();
                Console.WriteLine("Searching user info");
                Console.WriteLine("- - - - -");
                Console.WriteLine($"{searchUser} found!");
                Console.WriteLine($"username: {searchUser} password: {userData[searchUser]}");
            }

            else
            {

                Console.WriteLine();
                Console.WriteLine($"{searchUser} not found!");
            }


        }
        */

        //exit application Method
        private static void ExitApp()
        {
            Console.WriteLine();
            Console.WriteLine("Exiting application");
            Console.WriteLine("- - - - - ");
            
            Environment.Exit(0);

        }


    }

    //class to manage tasks
    public static class ToDoListManger
    {
        //private list 
        private static  List<string> tasks = new List<string>();

        public static string? addedTask;

        //welcome user

        public static void WelcomeUser()
        {

            Console.WriteLine("Welcome to Z - To Do List");
            Console.WriteLine();
            
            //call mainMenu after - - - welcome
            TaskManagerMenu();
        }


        //method to display Main Menu

        private static void TaskManagerMenu()
        {

            //Main menu instructions
            Console.WriteLine("Task Manager Instructions");
            Console.WriteLine("- - - - - - -");
            

            Console.WriteLine("1.Add tasks");
            Console.WriteLine("2.Display tasks");
            Console.WriteLine("3.Delete task");
            Console.WriteLine("4.Exit application");

            Console.WriteLine();

            //loop - - till break
            while (true)
            {

                //collect user Input
                
                
                //Console.WriteLine("- - - - - - -");
                Console.WriteLine("Choose an option from the Task Manager: ");
                string? userInput = Console.ReadLine();


                //switch for various options

                switch (userInput)
                {
                    case "1":
                        AddTask();
                        break;

                    case "2":
                        DisplayTasks();
                        break;

                    case "3":
                        DeleteTask();
                        break;

                    case "4":
                        ExitApp();
                        break;


                    default:
                        
                        Console.WriteLine("Invalid option!");
                        Console.WriteLine();
                        break;

                }



            }


        }


        //method to add task
        private static void AddTask()
        {

            // excecute - - - Ask task input
            do
            {

                Console.WriteLine();

                
                Console.WriteLine("- - - - - -");
                Console.WriteLine($"Input task to be added:");
                string? addedTask = Console.ReadLine();




                //valid if input is empty
                if (!string.IsNullOrWhiteSpace(addedTask))
                {

                    tasks.Add(addedTask);
                    Console.WriteLine();
                    
                    Console.WriteLine($"Task added successfully.");
                    
                    Console.WriteLine();
                    break;


                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }




                //keep looping if empty
            } while (true);



        }   


        //method to display Tasks

        private static void DisplayTasks()
        {
            Console.WriteLine();
            Console.WriteLine("List of added tasks");
            Console.WriteLine("- - - -  - -");

            //loop and display

            if (tasks.Count <= 0)
            {
                Console.WriteLine("No tasks available");
                Console.WriteLine();
            }

            else
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i+1}.{tasks[i]}");
                }


                Console.WriteLine();
            }


            
        } 

        //method to exit application 

        private static void ExitApp()
        {
            Console.WriteLine();
            Console.WriteLine("Existing application...");
            Environment.Exit(0);

        }

        private static void DeleteTask()
        {

            

            while (true)
            {
                

                //if no tasks present
                if (tasks.Count == 0)
                {


                    Console.WriteLine();
                    Console.WriteLine("Failed to delete tasks");
                    
                    Console.WriteLine("No tasks present!");
                    Console.WriteLine();
                    return;
                }


                //show tasks to user

                DisplayTasks();
                Console.WriteLine();
                Console.WriteLine("Input the task number of the task to be delete: ");
                string? taskToBeDel = Console.ReadLine();


              

                  
                //chcek if task is int and task Number availabe in  tasks
                    
                
                if (int.TryParse(taskToBeDel, out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
                 {
                    //to start from zero index
                    string deletedTask = tasks[taskNumber - 1];
                    tasks.RemoveAt(taskNumber - 1);

                        
                    //display deleted message
                    Console.WriteLine();
                    Console.WriteLine($"{deletedTask} successfully deleted");
                    Console.WriteLine();
                    break;
                    
                }

                    
                else
                {
                        
                    Console.WriteLine();
                    Console.WriteLine("Input a valid task number!");
                    Console.WriteLine();
                        
                    
                }
                    




                }



            }
            

            
            
        }


        }


    //excetubale Program
    public static  class Program
    {
        static void Main(string[] args)
        {


        //initate Code
        //UserManager.MainMenu();
        DatabaseContainer.CreateDatabaseConnection();
        
        


        

        
        
        
            

            
        }
    }

