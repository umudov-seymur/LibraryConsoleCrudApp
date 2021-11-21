using LibraryConsoleCrudApp.Business.Abstract;
using LibraryConsoleCrudApp.Controllers;
using LibraryConsoleCrudApp.Enums;
using Utilities.Utils;
using System;

namespace LibraryConsoleCrudApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PrintConsoleHeader();
                GetMenus();
                if (IsClearConsole()) break;
            }
        }

        /// <summary>
        /// Print menu navigation and call controller dynamically
        /// </summary>
        static void GetMenus()
        {
            string[] menuNames = Enum.GetNames(typeof(MenuEnums));
            IController[] menuControllers = {
                    new BookController(),
                    new GenreController(),
                    new AuthorController(),
                    new PublisherController()
                };

            MenuBuilder menuBuilder = new MenuBuilder(menuNames);
            menuBuilder.PrintNavigation();

            int choicedMenu = menuBuilder.ChoiceMenu();
            int actionMenu = menuBuilder.ChoiceOperationMenu(choicedMenu);
            var menuController = menuControllers[choicedMenu - 1];

            RunMenuController(actionMenu, menuController);
        }

        /// <summary>
        /// It cleans the console at the end of the process.
        /// </summary>
        /// <returns></returns>
        static bool IsClearConsole()
        {
            string clearStatus = UserConsole.Read("\nDo you want to clean the console? Y/N ", ConsoleColor.DarkCyan);

            if (clearStatus.Trim().ToLower() == "y")
            {
                Console.Clear();
                return false;
            }

            UserConsole.WriteLine("\nGood Bye :)", ConsoleColor.Magenta);
            return true;
        }

        /// <summary>
        /// Call the menu controller dynamically
        /// </summary>
        /// <param name="actionMenu">Menus index</param>
        /// <param name="menuController">Menus controller reference</param>
        static void RunMenuController(int actionMenu, IController menuController)
        {
            switch ((CrudOperationEnum)actionMenu)
            {
                case CrudOperationEnum.Read:
                    menuController.Index();
                    break;
                case CrudOperationEnum.Create:
                    menuController.Store();
                    break;
                case CrudOperationEnum.Update:
                    menuController.Update();
                    break;
                case CrudOperationEnum.Delete:
                    menuController.Destroy();
                    break;
            }
        }

        /// <summary>
        /// Print console book icon ASCII
        /// </summary>
        static void PrintConsoleHeader()
        {
            Console.WriteLine(@"
                            .--.                   .---.
                        .---|__|           .-.     |~~~|
                    .--|===|--|_          |_|     |~~~|--.
                    |  |===|  |'\     .---!~|  .--|   |--|
                    |%%|   |  |.'\    |===| |--|%%|   |  |
                    |%%|   |  |\.'\   |   | |__|  |   |  |
                    |  |   |  | \  \  |===| |==|  |   |  |
                    |  |   |__|  \.'\ |   |_|__|  |~~~|__|
                    |  |===|--|   \.'\|===|~|--|%%|~~~|--|
                    ^--^---'--^    `-'`---^-^--^--^---'--' Library Management System (LMS)
            ");
        }
    }
}
