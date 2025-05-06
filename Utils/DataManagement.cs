using OOP_Project.Utils.DataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOP_Project.Utils
{
    public class DataManagement
    {
        public static readonly string FILELOC = Directory.GetCurrentDirectory() + "/firearmData.txt";

        static List<BaseManager> managers;

        public static void Setup()
        {
            managers = new List<BaseManager>(); //initialize all the managers
            managers.Add(new FirearmManager());
            managers.Add(new CaliberManager());
            managers.Add(new AttachmentManager());
        }

        public static void Save()
        {
            if(Program.debug)
                Console.WriteLine("Saving data to " + FILELOC);

            using (StreamWriter reader = new StreamWriter(FILELOC))
            {
                foreach(BaseManager tempmanager in managers)
                {
                    reader.WriteLine(tempmanager.getName());
                    List<string> data = tempmanager.getData();
                    foreach(string str in data)
                    {
                        reader.WriteLine(str);
                    }
                    reader.WriteLine("END");
                }
                reader.Close();
            }
        }
        public static void Load()
        {
            if (Program.debug)
                Console.WriteLine("Loading data from " + FILELOC);
            
            //return File.ReadAllText(Directory.GetCurrentDirectory() + "/firearmData.txt");
            //FileStream file = File.Open(Directory.GetCurrentDirectory() + "/firearmData.txt", FileMode.Open);
            //StringBuilder sb = new StringBuilder();
            //Byte[] buffer = Encoding.UTF8.GetBytes(data);

            //Decoder decoder = Encoding.UTF8.GetDecoder();
            //StringBuilder sb = new StringBuilder();
            //byte[] inputBuffer = new byte[1];
            //int isEOF; // if 0, EOF. otherwise continue

            //while (file.Read(inputBuffer, 0, inputBuffer.Length) > 0)
            //{
            //    int charCount = decoder.GetCharCount(inputBuffer, 0, 1);
            //    char[] rgch = new char[charCount];

            //    decoder.GetChars(inputBuffer, 0, 1, rgch, 0);
            //    sb.Append(rgch);
            //}

            //file.Close();
            if (!File.Exists(FILELOC))
                File.Create(FILELOC).Close();
            using (StreamReader reader = new StreamReader(FILELOC))
            {
                while (reader.Peek() >= 0)
                {
                    BaseManager manager = null;
                    string type = reader.ReadLine() ?? "null";
                    //check through the managers to see if they exist
                    foreach(BaseManager tempmanager in managers)
                    {
                        if (tempmanager.isMine(type))
                        {
                            manager = tempmanager;
                            break;
                        }
                    }
                    if(manager != null)
                    {
                        List<string> data = new List<string>();
                        bool continueLoop = true;
                        while (continueLoop) //build up all data the manager will parse
                        {
                            string line = reader.ReadLine() ?? "END";
                            if (line == "END")
                            {
                                continueLoop = false;
                                continue;
                            }
                            data.Add(line);
                        }
                        manager.parseData(data);
                    }

                }
            }
        }

        public static BaseManager getManager<T>()
        {
            foreach(BaseManager manager in managers)
            {
                if(manager is T)
                    return manager;
            }
            return null;
        }
    }
}
