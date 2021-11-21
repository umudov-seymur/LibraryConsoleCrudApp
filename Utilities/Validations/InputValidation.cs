using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Utils;

namespace Utilities.Validations
{
    public static class InputValidation
    {
        /// <summary>
        /// It checks the predicate functions we pass into and, if true, allows the next action
        /// </summary>
        /// <param name="field">Any string</param>
        /// <param name="rules">Validation methods</param>
        /// <returns></returns>
        public static string Validate(string field, Predicate<string>[] rules)
        {
            string userInput = String.Empty;
            int ruleCheckedCount = 0;

            while (rules.Length > ruleCheckedCount)
            {
                userInput = UserConsole.Read(field);

                foreach (var rule in rules)
                {
                    try
                    {
                        rule(userInput.ToString());
                        ruleCheckedCount++;
                    }
                    catch (Exception e)
                    {
                        UserConsole.WriteLine(e.Message, ConsoleColor.Red);
                        ruleCheckedCount = 0;
                        break;
                    }
                }
            }

            return userInput;
        }

        /// <summary>
        /// It checks the functions I send into it, and returns us a clean dictinory if all are true.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Validate(Dictionary<string, Predicate<string>[]> fields)
        {
            var validatedData = new Dictionary<string, string>() { };

            foreach (var field in fields)
            {
                string data = Validate(field.Key, field.Value);
                validatedData.Add(field.Key, data);
            }

            return validatedData;
        }
    }
}
