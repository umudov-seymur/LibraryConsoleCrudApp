using System;
using System.Collections.Generic;

namespace Utilities.Utils
{
    public static class UserConsole
    {
        /// <summary>
        /// Get user input for all C/U operation
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string Read(string field)
        {
            return Read($"\nEnter the {field.ToLower()}: ", ConsoleColor.Blue);
        }

        /// <summary>
        /// Get user input for all C/U operation with foreground
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string Read(string field, ConsoleColor foreground)
        {
            Write(field, foreground);
            return Console.ReadLine();
        }

        /// <summary>
        /// Writes something to the console
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static void Write(string buffer, ConsoleColor foreground = ConsoleColor.White)
        {
            Console.ForegroundColor = foreground;
            Console.Write(buffer);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes something to the console with foreground
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="foreground"></param>
        public static void WriteLine(string buffer, ConsoleColor foreground = ConsoleColor.White)
        {
            Console.ForegroundColor = foreground;
            Console.WriteLine(buffer);
            Console.ResetColor();
        }

        /// <summary>
        /// Clear user console
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="foreground"></param>
        public static void Clear()
        {
            Console.Clear();
        }
    }
}
