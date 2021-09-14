using System;
using System.Collections.Generic;
using System.Text;

namespace ConfluentKafa.Interfaces.Extentions
{
    public class ConsoleExtetion
    {
        public static void PrintMEthodName()
        {
            System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
            System.Reflection.MethodBase mb = ss.GetFrame(1).GetMethod();
            var str = mb.DeclaringType.FullName;

            Console.WriteLine(str);
        }
    }
}
