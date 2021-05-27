using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public abstract class UserInterface<T>
    {
        protected UserInterface(T data)
        {
            Data = data;
        }

        protected readonly T Data;

        internal abstract void OpenAndPrintPage();

        protected abstract Action ProcessInput();

        protected void PrintErrorAndNavigateTo(UserInterface<T> page)
        {
            Console.WriteLine("There was an error. Press any key.");
            Console.ReadKey();
            page.OpenAndPrintPage();
        }
    }
}
