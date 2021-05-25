using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    public class ErrorLogger
    {
        private static string _rootpath = Directory.GetCurrentDirectory();

        public static void SaveMsg(string errmsg)
        {
            using (StreamWriter w = File.AppendText(_rootpath + "/Logfiles/log.txt"))
            {
                Log(errmsg, w);
            }

        }

        public static void Log(string logMsg, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMsg}");
            w.WriteLine("-------------------------------");

        }

    }

}
