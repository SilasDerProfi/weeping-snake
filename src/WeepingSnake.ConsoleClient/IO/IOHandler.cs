using System;

namespace WeepingSnake.ConsoleClient.IO
{
    public interface IOHandler
    {
        public ConsoleKey ReadKey();
        public string ReadLine();

        public void WriteLine(string value);
        public void WriteLine();
        public void Write(string value);

        public void Clear();
        public void SetCursorPosition(int left, int top);
        public (int left, int top) GetCursorPosition();
    }
}
