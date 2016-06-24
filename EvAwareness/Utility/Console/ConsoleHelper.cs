namespace EvAwareness.Utility.Console
{
    using System;

    public class ConsoleHelper
    {
        public static void OnLoad()
        {
            Console.WriteLine("[EvAwareness#] Console loaded!");
        }

        public static void Print(ConsoleItem consoleItem)
        {
            Console.Write("[EvAwareness#] ");

            Console.WriteLine(consoleItem.GetLoggingString());
        }
    }
}