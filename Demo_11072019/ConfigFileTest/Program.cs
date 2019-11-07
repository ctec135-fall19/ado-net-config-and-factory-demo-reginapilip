using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add components
// need to manually reference System.Configuration
    // Solution Explorer >> Right-click References >> Add Reference >> Assemblies
    //                   >> Scroll and check box for System.Configuration
using System.Configuration;
using static System.Console;

namespace ConfigFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("*** Fun with Config Files ***\n");
            WriteLine("*AppSettings\n");
            // created a place to put config file contents
            string[] appSettings = new string[5];
            appSettings[0] = ConfigurationManager.AppSettings["provider"];
            appSettings[1] = ConfigurationManager.AppSettings["provider2"];
            appSettings[2] = ConfigurationManager.AppSettings["connectionString2"];
            appSettings[3] = ConfigurationManager.AppSettings["provider3"];
            appSettings[4] = ConfigurationManager.AppSettings["connectionString3"];

            // printing config file
            foreach(string s in appSettings)
            {
                WriteLine(s);
            }
            WriteLine();

            WriteLine("*Connection Strings");
            WriteLine(ConfigurationManager.
                ConnectionStrings["AutoLotOleDBProvider"].ConnectionString);
            WriteLine();
        }
    }
}
