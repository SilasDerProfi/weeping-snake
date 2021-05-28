using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.ConsoleClient.IO;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public abstract class UserInterface<T>
    {
        protected UserInterface(T data, IOHandler ioHandler)
        {
            Data = data;
            InOut = ioHandler;
        }

        protected readonly T Data;

        protected readonly IOHandler InOut;

        internal abstract void OpenAndPrintPage();

        protected abstract Action ProcessInput();

        protected void PrintErrorAndNavigateTo(UserInterface<T> page)
        {
            InOut.WriteLine("There was an error. Press any key.");
            InOut.ReadKey();
            page.OpenAndPrintPage();
        }
    }
}
