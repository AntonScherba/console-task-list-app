using System;
using System.Collections.Generic;
using System.Linq;

namespace todo_list
{

    public class UI
    {
        public static string st = "\t\t ";
        public static string xst = "\t\t";
        //UI   
        public static void Header()
        {
            Console.Clear();
            DateTime currentDate = DateTime.Now;
            Console.WriteLine("\n\n\n\t\t\t\t\t" + currentDate.ToString("dd-MM-yyyy"));
            Console.WriteLine("\t\t========================================");
            Console.WriteLine("\t\t\t Task List");
            Console.WriteLine("\t\t========================================");
        }

        public static void Footer()
        {
            Console.WriteLine("\t\t========================================");
        }

        public static void Message(String msg)
        {
            Header();
            Console.WriteLine("\n\n" + st + msg + "\n\n");
            Footer();
            Console.Write(xst + "Press <any> key to continue:");
            Console.ReadKey();
        }
    }

    public class Program
    {

        public static void Main()
        {
            var task = new TaskList();
            while (true)
            {
                UI.Header();
                Console.WriteLine(UI.xst + "1.New Task." + UI.xst + "4.Delete Task.\n");
                Console.WriteLine(UI.xst + "2.View All." + UI.xst + "5.Sort.\n");
                Console.WriteLine(UI.xst + "3.Update Task." + UI.xst + "6.Exit");
                UI.Footer();
                Console.Write(UI.xst + "Enter your option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        task.CreateTask();
                        break;
                    case "2":
                        task.DisplayList();
                        break;
                    case "3":
                        task.UpdateTask();
                        break;
                    case "4":
                        task.DeleteTask();
                        break;
                    case "5":
                        task.SortList();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        UI.Message("Invalid option!");
                        break;
                }
            }
        }
    }

    public class TaskItem
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public TaskItem(int id, string title, DateTime date)
        {
            TaskId = id;
            Title = title;
            Date = date;
        }
    }

    public class TaskList : List<TaskItem>
    {
        bool isEmpty = true;
        DateTime currentDate = DateTime.Today;
        public void Add(string title, DateTime date)
        {
            int id = this.Count() + 1;
            this.Add(new TaskItem(id, title, date));
        }

        public void DisplayList()
        {
            UI.Header();
            Console.WriteLine("\t\tID\tTask\tDate\t\tExpired");
            foreach (var task in this)
            {
                isEmpty = false;
                bool isExpired = currentDate > task.Date;

                Console.WriteLine("\t\t{0}\t{1}\t{2}\t{3}\t\t", task.TaskId, task.Title, task.Date.ToString("dd-MM-yyyy"), isExpired);
            }
            if (isEmpty)
            {
                Console.WriteLine("\n\n\t\t\tNo Records Found!\n\n");
            }
            UI.Footer();
            Console.Write(UI.xst + "Press <any> key to continue:");
            Console.ReadKey();
        }

        public void CreateTask()
        {

            UI.Header();
            Console.Write("\t\tEnter the Date.\t[dd-mm-yyyy]\n\t\t");

            string enteredDate = Console.ReadLine();
            DateTime Date;


            while (!DateTime.TryParse(enteredDate, out Date))
            {
                UI.Message("Error: This is not valid input.");
                Console.Write("\t\tPlease enter the date in [dd-mm-yyyy] format:\n\t\t");
                enteredDate = Console.ReadLine();
            }

            Console.Write("\t\tEnter Task Title: ");

            string title = Console.ReadLine().Trim();

            while (title == "")
            {
                UI.Message("Error: Task title is empty");
                Console.Write("\t\tPlease enter not empty task title:\n\t\t");
                title = Console.ReadLine().Trim();
            }

            this.Add(title, DateTime.Parse(enteredDate));
            UI.Message("New Task Created");
        }

        public void DeleteTask()
        {
            UI.Header();
            Console.Write("\t\tEnter the Task ID.\n\t\t");

            string todoNumber = Console.ReadLine();
            int number;

            while (!int.TryParse(todoNumber, out number))
            {
                UI.Message("Error: This is not valid input.");
                Console.Write("\t\tPlease enter an integer value:\n\t\t");
                todoNumber = Console.ReadLine();
            }

            var item = this.SingleOrDefault(todo => todo.TaskId == number);
            if (item != null)
            {
                this.Remove(item);
                UI.Message("Task Deleted!");
            }
            else
            {
                UI.Message("Task ID not found!");
            }
        }
        public void UpdateTask()
        {
            UI.Header();
            Console.Write("\t\tEnter the Task ID for Update.\n\t\t");

            string todoNumber = Console.ReadLine();
            int number;

            while (!int.TryParse(todoNumber, out number))
            {
                UI.Message("Error: This is not valid input.");
                Console.Write("\t\tPlease enter an integer value:\n\t\t");
                todoNumber = Console.ReadLine();
            }

            var item = this.SingleOrDefault(todo => todo.TaskId == number);
            if (item != null)
            {
                string title = "";

                Console.Write("\t\tEnter new task Title: ");
                title = Console.ReadLine().Trim();
                Console.WriteLine("{0}\n", title);
                item.Title = title;
                UI.Message("Task Updated!");

            }
            else
            {
                UI.Message("Task ID not found!");
            }
        }

        public void SortList()
        {
            bool isSort = true;
            while (isSort)
            {
                UI.Header();
                Console.WriteLine("\t\t1.Sort By ID.");
                Console.WriteLine("\t\t2.Sort By DATE.");
                Console.WriteLine("\t\t3.Exit.");
                UI.Footer();
                Console.Write(UI.st + "Enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {

                    case "1":
                        this.Sort((x, y) => x.TaskId.CompareTo(y.TaskId));
                        UI.Message("Sorting by ID completed!");
                        break;

                    case "2":
                        this.Sort((x, y) => x.Date.CompareTo(y.Date));
                        UI.Message("Sorting by Date completed!");
                        break;

                    case "3":
                        isSort = false;
                        break;
                    default:
                        UI.Message("Invalid choice!");
                        break;
                }

            }
        }
    }
}