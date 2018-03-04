using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
/*
Since Windows 10 Anniversary Update, console can use ANSI/VT100 color codes

You need set flag ENABLE_VIRTUAL_TERMINAL_PROCESSING(0x4) by SetConsoleMode
Use sequences:

"\x1b[48;5;" + s + "m" - set background color by index in table(0-255)

"\x1b[38;5;" + s + "m" - set foreground color by index in table(0-255)

"\x1b[48;2;" + r + ";" +g+";"+b + "m" - set background by r, g, b values

"\x1b[38;2;" + r + ";" +g+";"+b + "m" - set foreground by r, g, b values

  Impotant notice: Internally Windows have only 256 (or 88) colors in table and Windows will used nearest to(r, g, b) value from table.

    https://stackoverflow.com/questions/7937256/orange-text-color-in-c-sharp-console-application
  */
namespace PSConsoleColors
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        static void Main(string[] args)
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);

            for (int i = 0; i < 255; i++)
            {
                Console.Write("\x1b[48;5;" + i + "m" + i.ToString().PadLeft(5));
            }

            Console.ReadLine();
        }
    }
}
