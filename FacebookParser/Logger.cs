using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + message);
        }

    }
}
