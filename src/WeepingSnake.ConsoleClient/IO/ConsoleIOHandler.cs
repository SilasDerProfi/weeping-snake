using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.ConsoleClient.IO
{
    class ConsoleIOHandler : IOHandler
    {
        public ConsoleKey ReadKey() => Console.ReadKey().Key;

        public string ReadLine() => Console.ReadLine();

        public void WriteLine(string value) => Console.WriteLine(value);

        public void WriteLine() => Console.WriteLine();

        public void Write(string value) => Console.Write(value);

        public void Clear() => Console.Clear();

        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public (int left, int top) GetCursorPosition() => Console.GetCursorPosition();
    }
}
