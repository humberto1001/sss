using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleShell_0._1._0
{
    // Adds commands based on functions rather than objects
    // TODO: Go for an object heirarchy for this setup
    public class Shell
    {
        public static int RUN_CODE = 1;
        public static int EXIT_CODE = 0;

        private static char[] delimeters = {'\n', '\t', ' '};
        // Holds all of the functions for the shell
        private Dictionary<string, Func<string[], int>> Aliases = new Dictionary<string, Func<string[], int>>();

        // Add a new function to the shell
        public void AddCommand(string alias, Func<string[], int> function)
        {
            try
            {
                Aliases.Add(alias, function);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        // Initialize shell
        public void Init()
        {
            int status = RUN_CODE;
            string line;
            string[] arguments;

            // Set the console color setting
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Welcome to the Super Simple Shell!\n");
            Console.Write("Enter 'help' to see list of commands.\n\n");

            do
            {
                Console.Write("~$ ");
                line = Console.ReadLine();
                arguments = line.Split(delimeters);
                status = Execute(arguments);

            } while (status != EXIT_CODE);
        }

        // Execution code for functions
        private int Execute(string[] arguments)
        {
            int status = RUN_CODE;

            for (int i = 0; i < arguments.Length; i++)
            {
                if (Aliases.ContainsKey(arguments[i]))
                {
                    if (arguments.Length == 0)
                    {
                        status = Aliases[arguments[i]](null);
                    }
                    else
                    {
                        string[] args = arguments.Skip(1).ToArray();
                        status = Aliases[arguments[i]](args);
                    }
                }
            }

            return status;
        }
    }
}
