using System;
using Utilities.Validations;

namespace Utilities.Utils
{
    public class MenuBuilder
    {
        /// <summary>
        /// Crud menus storage
        /// </summary>
        public string[] Menus;

        public MenuBuilder(string[] menus)
        {
            Menus = menus;
        }

        /// <summary>
        /// Print all menus
        /// </summary>
        public void PrintNavigation()
        {
            for (int i = 0; i < Menus.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {Menus[i]} Menu");
            }
        }

        /// <summary>
        /// This method select our menu from the console with auto validaton
        /// </summary>
        /// <returns></returns>
        public int ChoiceMenu()
        {
            int choicedMenu = int.Parse(
                InputValidation.Validate("menu number", new Predicate<string>[] {
                    Validation.IsRequired,
                    Validation.IsPositiveInteger,
                    IsCorrectMenuChoice
                })
            );

            return choicedMenu;
        }

        /// <summary>
        /// This method select our crud operation menu from the console
        /// </summary>
        /// <returns></returns>
        public int ChoiceOperationMenu(int menu)
        {
            UserConsole.Clear();
            UserConsole.Write($"You choiced menu name ");
            UserConsole.Write($"{Menus[menu - 1]}\n", ConsoleColor.Green);

            PrintSubMenuNavigation(menu);

            return int.Parse(
                InputValidation.Validate("operation number", new Predicate<string>[] {
                    Validation.IsRequired,
                    Validation.IsPositiveInteger,
                    IsCorrectActionChoice
                })
            );
        }

        /// <summary>
        /// Print crud menus 
        /// </summary>
        /// <returns></returns>
        private void PrintSubMenuNavigation(int menuIndex)
        {
            string menuName = Menus[menuIndex - 1];

            string[] menuItems = {
                    $"Fetch All {menuName}s.",
                    $"Create New {menuName}.",
                    $"Update A {menuName}.",
                    $"Delete A {menuName}.",
                };

            for (int i = 0; i < menuItems.Length; i++)
            {
                UserConsole.WriteLine($"[{i + 1}] {menuItems[i]}");
            }
        }

        /// <summary>
        /// Check menus that it is choiced correctly 
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public bool IsCorrectMenuChoice(string choice)
        {
            if (Menus.Length < int.Parse(choice))
            {
                throw new Exception($"Choose one of the above.");
            }

            return true;
        }

        /// <summary>
        /// Checks crud menus that it is choiced correctly 
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public bool IsCorrectActionChoice(string choice)
        {
            if (Menus.Length < int.Parse(choice))
            {
                throw new Exception($"Choose one of the above.");
            }

            return true;
        }
    }
}
