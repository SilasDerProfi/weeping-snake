using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeepingSnake.ConsoleClient.Tests.IO
{
    public class MockIOHandler : ConsoleClient.IO.IOHandler
    {
        public ConsoleKey ReadKey() => ReadKeyFunc?.Invoke() ?? ConsoleKey.Escape;

        public string ReadLine() => ReadLineFunc?.Invoke();

        public void WriteLine(string value) => WriteLineAction?.Invoke(value);

        public void WriteLine() => WriteLineAction?.Invoke(null);

        public void Write(string value) => WriteAction?.Invoke(value);

        public void Clear() => ClearAction?.Invoke();

        public void SetCursorPosition(int left, int top) => SetCursorPositionAction?.Invoke(left, top);

        public (int left, int top) GetCursorPosition() => GetCursorPositionFunc?.Invoke() ?? (0, 0);

        internal Func<ConsoleKey> ReadKeyFunc;
        internal Func<string> ReadLineFunc;
        internal Action<string> WriteLineAction;
        internal Action<string> WriteAction;
        internal Action ClearAction;
        internal Action<int, int> SetCursorPositionAction;
        internal Func<(int left, int top)> GetCursorPositionFunc;
    }
}
