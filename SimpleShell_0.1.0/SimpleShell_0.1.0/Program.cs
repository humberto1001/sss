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
        //Function for help command. ~Josh
        public static int lsh_Help(string[] arguments)
        {
            Console.Write("cd = change directory\nls = list directory\nmd = create new folder directory\n"+
                          "clear = clears the console\nexit = exits the shell\n");
            return 1;
        }
        //Function for creating new folder command. ~Josh
        public static int lsh_CreateFolder(string[] arguments)
        {
            var dir = @"C:\Downloads\TestSSS";
            Directory.CreateDirectory(dir);
            return 1;
        }

        static void Main(string[] args)
        {
            Shell shell = new Shell();
            shell.AddCommand("ls", lsh_listFiles);
            shell.AddCommand("exit", lsh_exit);
            shell.AddCommand("cd", lsh_CD);
            shell.AddCommand("clear", lsh_Clear);
            shell.AddCommand("help", lsh_Help);
            shell.AddCommand("md", lsh_CreateFolder);

            shell.Init();
        }
    }
}
