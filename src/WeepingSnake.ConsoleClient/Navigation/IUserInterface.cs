using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public interface IUserInterface<T>
    {
        void Open(T data);

        Action ProcessInput();

        void PrintPage();
    }
}
