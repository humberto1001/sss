using System;
using System.IO;

// I have some simple functions that works perfectly well, but what
// you will need to do is write a static function and then add it with
// the shell.AddCommand() and the first parameter is the function name
// in other words how it will be invoked or is recognized and the next parameter
// is the function which must take in a type of argument
//
// TODO: 
//
// Make commands objects so we can pass the ToString functions as the 
// description of it and that way a help command will just list all of them
// using ToString()
//
// Simplify the system of taking arguments
// Allow mulstiple kinds of armuents
namespace SimpleShell_0._1._0
{
    class Program
    {
        private static string _directory = Directory.GetCurrentDirectory();

        // Sample functions
        // If the function returns 0 then the program will end

        public static int lsh_listFiles(string[] arguments)
        {
            var files = Directory.GetFiles(_directory);

            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            return 1;
        }

        public static int lsh_exit(string[] arguments)
        {
            return 0;
        }

        public static int lsh_CD(string[] arguments)
        {
            if (Directory.Exists(arguments[0]))
            {
                _directory = arguments[0];
            }
            else
            {
                Console.WriteLine("invalid directory");
            }

            return 1;
        }

        public static int lsh_Clear(string[] arguments)
        {
            Console.Clear();
            return 1;
        }

        public static int lsh_Date(string[] arguments)
        {
            DateTime currentDate = DateTime.Today;
            Console.WriteLine(currentDate.ToString("D") + " " + DateTime.Now.ToShortTimeString());
            return 1;
        }

        public static int lsh_PWD(string[] arguments)
        {
            Console.WriteLine(_directory);
            return 1;
        }

        public static int lsh_Copy(string[] arguments)
        {
            if (arguments.Length != 2)
            {
                Console.WriteLine("Incorrect Syntax");
            }
            else
            {
                if (File.Exists(arguments[0]) == false)
                {
                    Console.WriteLine("File " + arguments[0] + " not found");
                }
                else if (Directory.Exists(arguments[1]) == false)
                {
                    Console.WriteLine("Directory " + arguments[1] + " not found");
                }
                else
                {
                    File.Copy(arguments[0], arguments[1] + "\\" + arguments[0]);
                }
            }
            return 1;
        }

        public static int lsh_RM(string[] arguments)
        {
            if (File.Exists(arguments[0]) == false)
            {
                Console.WriteLine("File " + arguments[0] + " not found");
            }
            else
            {
                Console.WriteLine("Are you sure: ");
                string Option = Console.ReadLine();
                if(Option == "y" || Option == "Y")
                {
                    File.Delete(arguments[0]);
                    Console.WriteLine("File " + arguments[0] + " removed");
                }
            }
            return 1;
        }

        public static int lsh_WhoAmI(string[] arguments)
        {
            Console.WriteLine(Environment.UserName);
            return 1;
        }

        public static int lsh_mv(string[] arguments)
        {
            if (File.Exists(arguments[0]) == false)
            {
                Console.WriteLine("File " + arguments[0] + " not found");
            }
            else if(Directory.Exists(arguments[1]) == false)
            {
                Console.WriteLine("Directory " + arguments[1] + " not found");
            }
            else
            {
                Console.WriteLine("Are you sure: ");
                string Option = Console.ReadLine();
                if (Option == "y" || Option == "Y")
                {
                    File.Move(arguments[0], arguments[1] + "\\" + arguments[0]);
                    Console.WriteLine("File " + arguments[0] + " moved to directory");
                }
            }
            return 1;
        }

        static void Main(string[] args)
        {
            Shell shell = new Shell();
            shell.AddCommand("ls", lsh_listFiles);
            shell.AddCommand("exit", lsh_exit);
            shell.AddCommand("cd", lsh_CD);
            shell.AddCommand("clear", lsh_Clear);
            shell.AddCommand("date", lsh_Date);
            shell.AddCommand("pwd", lsh_PWD);
            shell.AddCommand("copy", lsh_Copy);
            shell.AddCommand("rm", lsh_RM);
            shell.AddCommand("whoami", lsh_WhoAmI);
            shell.AddCommand("mv", lsh_mv);
            shell.Init();
        }
    }
}
