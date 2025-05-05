using OOP_Project.Utils;
using OOP_Project.Utils.DataManagers;

namespace OOP_Project
{
    internal class Program
    {
        //Whether debug data is printed to console
        public static bool debug = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Getting data...");
            DataManagement.Setup();
            DataManagement.Load();

            //get managers
            BaseManager bm = DataManagement.getManager<FirearmManager>();
            if (bm == null)
                return;
            FirearmManager fm = (FirearmManager)bm;

            bool save = false;

            Console.Clear();
            while (true)
            {
                Console.Write("Firearm manager v.0.1.0 >");
                String input = Console.ReadLine();
                String[] inputs = input.Split(' ');
                if(inputs.Length < 1)
                {

                }

                if (inputs[0] == "get")
                {
                    List<string> list = fm.getFirearmNames();
                    foreach (string name in list)
                        Console.WriteLine(name);
                }
                else if(input == "savefirearms")
                {
                    Console.WriteLine("Name?");
                    input = Console.ReadLine();
                    fm.addFirearm(input);
                    save = true;
                }

                if (save)
                    DataManagement.Save();
                save = false;
            }
            //Console.WriteLine("Data: " + data);
            //Console.WriteLine("Input new data: ");
            //data = Console.ReadLine();
            //DataManagement.Save(data);
            //Console.WriteLine("New data: " + data);
        }
    }
}
