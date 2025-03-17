using OOP_Project.Utils;

namespace OOP_Project
{
    internal class Program
    {
        //Whether debug data is printed to console
        public static bool debug = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Getting data...");
            String data = DataManagement.Load();
            Console.WriteLine("Data: " + data);
            Console.WriteLine("Input new data: ");
            data = Console.ReadLine();
            DataManagement.Save(data);
            Console.WriteLine("New data: " + data);
        }
    }
}
