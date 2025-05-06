using OOP_Project.Utils.DataContainers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataManagers
{
    public class FirearmManager : BaseManager
    {

        public bool addFirearm(string name, CompoundContainer caliber)
        {
            if(compoundContainers.Select(x => x.getValue("name")).Contains(name))
                return false;
            CompoundContainer container = new CompoundContainer();
            container.Add(new BaseContainer("name", name));
            container.Add(new BaseContainer("caliber", caliber.getValue("id")));
            container.Add(new BaseContainer("id", idList++));
            getSettings().setValue("id", idList.ToString());
            compoundContainers.Add(container);
            return true;
        }

        public bool removeFirearm(string id)
        {
            CompoundContainer container = compoundContainers.FirstOrDefault(x => x.getValue("id") == id);
            if(container != null)
                compoundContainers.Remove(container);
            return container != null;
        }

        public void removeFirearms(Predicate<CompoundContainer> filter) //DANGER!
        {
            compoundContainers.RemoveAll(filter);
        }

        public override string getName()
        {
            return "firearms";
        }

        public List<string> getFirearmNames(string filter)
        {
            List<string> data = new List<string>();
            if(filter != null)
                return filterIt(filter);
            foreach(CompoundContainer container in compoundContainers)
            {
                data.Add($"[{container.getValue("id")}] {container.getValue("name")}");
            }
            return data;
        }
        public CompoundContainer getFirearm(string id)
        {
            return compoundContainers.FirstOrDefault(x => x.getValue("id") == id);
        }

        public void attachmentUpdate(bool remove, string id)
        {
            foreach(CompoundContainer container in compoundContainers)
            {
                if (remove)
                {
                    container.Remove(id);
                }
                else
                {
                    container.Add(new BaseContainer(id, false));
                }
            }
        }

        private List<string> filterIt(string filter)
        {
            List<CompoundContainer> data = new List<CompoundContainer>();
            try
            {
                int count = filter.IndexOf('?');
                string name = filter.Substring(0, count > -1 ? count : filter.Length);
                foreach (CompoundContainer container in compoundContainers)
                {
                    if(container.getValue("name").ToLower().Contains(name.ToLower()))
                        data.Add(container);
                }
                if (count > -1)
                {
                    while (count < filter.Length)
                    {
                        count++;
                        string temp = "";
                        name = "";
                        while (filter[count] != '=')
                        {
                            name += filter[count];
                            count++;
                        }
                        count++;
                        while (count < filter.Length && filter[count] != '&')
                        {
                            temp += filter[count];
                            count++;
                        }
                        for (int i = 0; i < data.Count; i++)
                        {
                            CompoundContainer container = data[i];
                            bool del = false;
                            switch (name.ToLower())
                            {
                                case "caliber":
                                    List<string> ids = Program.cm.getCaliberIDs(temp);

                                    if (!ids.Contains(container.getValue("caliber")))
                                        del = true;
                                    break;
                                default: //if anything in attachments
                                    CompoundContainer attachment = Program.am.getFirst(name);
                                    if (attachment == null)
                                    {
                                        Console.WriteLine("Attachment " + name + " does not exist");
                                    }
                                    if (container.getValue(attachment.getValue("id")) != temp)
                                        del = true;
                                    break;
                            }
                            if (del)
                            {
                                data.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
                return data.Select(x => $"[{x.getValue("id")}] {x.getValue("name")}").ToList();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
