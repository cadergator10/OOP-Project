using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils
{
    public class DataManagement
    {
        static String defaulter = "default";
        public static void Save(String data)
        {
            if(Program.debug)
                Console.WriteLine("Saving data to " + Directory.GetCurrentDirectory() + "/firearmData.txt");
            File.WriteAllText(Directory.GetCurrentDirectory() + "/firearmData.txt", data);
        }
        public static String Load()
        {
            if (Program.debug)
                Console.WriteLine("Loading data from " + Directory.GetCurrentDirectory() + "/firearmData.txt");
            try
            {
                return File.ReadAllText(Directory.GetCurrentDirectory() + "/firearmData.txt");
            }
            catch (Exception e)
            {
                return defaulter;
            }
        }
    }
}
