using OOP_Project.Utils;
using OOP_Project.Utils.DataContainers;
using OOP_Project.Utils.DataManagers;

namespace OOP_Project
{
    internal class Program
    {
        //Whether debug data is printed to console
        public static bool debug = true;
        public static FirearmManager fm;
        public static CaliberManager cm;
        public static AttachmentManager am;

        static void Main(string[] args)
        {
            Console.WriteLine("Getting data...");
            DataManagement.Setup();
            DataManagement.Load();

            //get managers
            BaseManager bm = DataManagement.getManager<FirearmManager>();
            if (bm == null)
                return;
            fm = (FirearmManager)bm;

            bm = DataManagement.getManager<CaliberManager>();
            if (bm == null)
                return;
            cm = (CaliberManager)bm;

            bm = DataManagement.getManager<AttachmentManager>();
            if (bm == null)
                return;
            am = (AttachmentManager)bm;

            //start getting user inputs
            bool save = false;
            Console.Clear();
            while (true)
            {
                Console.Write("Firearm manager v.0.1.0 >");
                String input = Console.ReadLine();
                String[] inputs = input.Split(' ');
                if(inputs.Length < 1)
                {
                    Console.WriteLine("Usage: <list/add/remove/manage/get/info/exit> ...");
                    Console.WriteLine("Use --help after any command for extra help");
                    continue;
                }

                if (inputs[0] == "info")
                {
                    Console.WriteLine("Database info:");
                    Console.WriteLine($"Data location: {DataManagement.FILELOC}");
                }
                else if (inputs[0] == "list")
                {
                    if(inputs.Length == 1)
                    {
                        Console.WriteLine("Usage: list <calibers/attachments> [name]");
                        Console.WriteLine("Usage: list firearms [filter]");
                        continue;
                    }
                    if (inputs.Length == 2 && inputs[1] == "--help")
                    {
                        Console.WriteLine("Usage: list <calibers/attachments> [name]");
                        Console.WriteLine("Usage: list firearms [filter]");
                        Console.WriteLine("filters:");
                        Console.WriteLine("Use _ instead of no spaces");
                        Console.WriteLine("beginning is the firearm name, afterward are the accessories it supports)");
                        Console.WriteLine("example: 'Kriss_Vector?caliber=9mm&rails=1'");
                        continue;
                    }
                    List<string> list = null;
                    if (inputs[1] == "firearms")
                        list = fm.getFirearmNames(inputs.Length > 2 ? inputs[2] : null);
                    else if (inputs[1] == "calibers")
                        list = cm.getCalibers(inputs.Length > 2 ? inputs[2] : null);
                    else if (inputs[1] == "attachments")
                        list = am.getAttachments(inputs.Length > 2 ? inputs[2] : null);
                    if (list == null)
                    {
                        Console.WriteLine("Error occurred: unable to list items");
                        continue;
                    }
                    foreach (string name in list)
                        Console.WriteLine(name);
                }
                else if (inputs[0] == "add")
                {
                    if (inputs.Length < 2 || inputs[1] == "--help")
                    {
                        Console.WriteLine("Usage: add <firearm/caliber/attachment> <name> ...");
                        continue;
                    }
                    bool exists = false;
                    switch (inputs[1])
                    {
                        case "firearm":
                            if (inputs.Length < 4)
                            {
                                Console.WriteLine("Usage: add firearm <name> <caliber>");
                                continue;
                            }
                            CompoundContainer caliber = cm.getFirst(inputs[3]);
                            if (caliber == null)
                            {
                                Console.WriteLine($"Caliber {inputs[3]} does not exist");
                                continue;
                            }
                            exists = fm.addFirearm(inputs[2], caliber);
                            if (!exists)
                            {
                                Console.WriteLine($"Firearm {inputs[2]} already exists");
                                continue;
                            }
                            Console.WriteLine($"Added {inputs[2]} into the db");
                            break;
                        case "caliber":
                            if (inputs.Length < 3)
                            {
                                Console.WriteLine("Usage: add caliber <caliber>");
                                continue;
                            }
                            exists = cm.addCaliber(inputs[2]);
                            if (!exists)
                            {
                                Console.WriteLine($"Caliber {inputs[2]} already exists");
                                continue;
                            }
                            Console.WriteLine($"Added {inputs[2]} into the db");
                            break;
                        case "attachment":
                            if (inputs.Length < 3)
                            {
                                Console.WriteLine("Usage: add attachment <name>");
                                continue;
                            }
                            exists = am.addAttachment(inputs[2]);
                            if (!exists)
                            {
                                Console.WriteLine($"Attachment {inputs[2]} already exists");
                                continue;
                            }
                            Console.WriteLine($"Added {inputs[2]} into the db & updated firearms");
                            break;
                        default:
                            Console.WriteLine("Usage: add <firearm/caliber/attachment> <name>");
                            continue;
                    }
                    save = true;
                }
                else if (inputs[0] == "remove")
                {
                    if (inputs.Length < 2 || inputs[1] == "--help")
                    {
                        Console.WriteLine("Usage: remove <firearm/caliber/attachment> <id>");
                        continue;
                    }
                    bool exists = false;
                    switch (inputs[1])
                    {
                        case "firearm":
                            if (inputs.Length < 3)
                            {
                                Console.WriteLine("Usage: remove firearm <name>");
                                continue;
                            }
                            exists = fm.removeFirearm(inputs[2]);
                            if (!exists)
                            {
                                Console.WriteLine($"Firearm {inputs[2]} does not exist");
                                continue;
                            }
                            Console.WriteLine($"Removed {inputs[2]} from the db");
                            break;
                        case "caliber":
                            if (inputs.Length < 3)
                            {
                                Console.WriteLine("Usage: remove caliber <name>");
                                continue;
                            }
                            exists = cm.removeCaliber(inputs[2]);
                            if (!exists)
                            {
                                Console.WriteLine($"Caliber {inputs[2]} does not exist");
                                continue;
                            }
                            Console.WriteLine($"Removed {inputs[2]} and firearms with this caliber from the db");
                            break;
                        case "attachment":
                            if (inputs.Length < 3)
                            {
                                Console.WriteLine("Usage: remove attachment <name>");
                                continue;
                            }
                            exists = am.removeAttachment(inputs[2]);
                            if (!exists)
                            {
                                Console.WriteLine($"Attachment {inputs[2]} does not exist");
                                continue;
                            }
                            Console.WriteLine($"Removed {inputs[2]} from the db & updated firearms");
                            break;
                        default:
                            Console.WriteLine("Usage: add <firearm/caliber/attachment> <name>");
                            continue;
                    }
                    save = true;
                }
                else if (inputs[0] == "manage")
                {
                    if (inputs.Length < 2 || inputs[1] == "--help")
                    {
                        Console.WriteLine("Change a firearm's attachments");
                        Console.WriteLine("Usage: manage <firearm_id> <attachment_id> <true/false>");
                        continue;
                    }
                    if (inputs.Length < 4)
                    {
                        Console.WriteLine("Missing arguments");
                        continue;
                    }
                    CompoundContainer container = fm.getFirearm(inputs[1]);
                    if (container == null)
                    {
                        Console.WriteLine("Firearm ID does not exist");
                        continue;
                    }
                    CompoundContainer attach = am.getFirstID(inputs[2]);
                    if (attach == null)
                    {
                        Console.WriteLine("Attachment ID does not exist");
                        continue;
                    }
                    container.setValue(inputs[2], inputs[3] == "true" ? "true" : "false");
                    Console.WriteLine($"Set attachment {attach.getValue("name")} to {container.getValue(inputs[2])}");
                }
                else if (inputs[0] == "get")
                {
                    if (inputs.Length < 2 || inputs[1] == "--help")
                    {
                        Console.WriteLine("Get a firearm's details via a specific ID");
                        Console.WriteLine("Usage: get <id>");
                        continue;
                    }
                    CompoundContainer container = fm.getFirearm(inputs[1]);
                    if (container == null)
                    {
                        Console.WriteLine($"Firearm with ID {inputs[1]} does not exist");
                        continue;
                    }
                    CompoundContainer caliber = cm.getFirstID(container.getValue("caliber")); //should never be null unless main save file is tampered with somehow
                    Console.WriteLine("Name: " + container.getValue("name"));
                    Console.WriteLine("Caliber: " + caliber.getValue("caliber"));
                    foreach (BaseContainer item in container.getAll())
                    {
                        if (item.name != "name" && item.name != "id" && item.name != "caliber")
                        {
                            CompoundContainer attachment = am.getFirstID(item.name); //same as caliber: shouldn't be null
                            Console.WriteLine($"{attachment.getValue("name")}: {item.getValue()}");
                        }
                    }
                }
                else if (inputs[0] == "exit")
                    break;
                else
                {
                    Console.WriteLine("Usage: <list/add/remove/manage/get/info/exit> ...");
                    Console.WriteLine("Use --help after any command for extra help");
                    continue;
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
