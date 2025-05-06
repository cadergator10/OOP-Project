using OOP_Project.Utils.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Project.Utils.DataManagers
{
    /// <summary>
    /// Author: Cade Kiser
    /// ID: 801341186
    /// </summary>
    public class CaliberManager : BaseManager
    {

        public bool addCaliber(string caliber)
        {
            if (compoundContainers.Select(x => x.getValue("caliber")).Contains(caliber))
                return false;
            CompoundContainer container = new CompoundContainer();
            container.Add(new BaseContainer("caliber", caliber));
            container.Add(new BaseContainer("id", idList++));
            getSettings().setValue("id", idList.ToString());
            compoundContainers.Add(container);
            return true;
        }

        public bool removeCaliber(string id)
        {
            CompoundContainer container = compoundContainers.FirstOrDefault(x => x.getValue("id") == id);
            if (container != null)
            {
                compoundContainers.Remove(container);
                Program.fm.removeFirearms(x => x.getValue("caliber") == id);
            }
            return container != null;
        }

        public override string getName()
        {
            return "caliber";
        }

        private List<CompoundContainer> getCaliberRaw(string name)
        {
            if (name != null)
            {
                List<CompoundContainer> data = new List<CompoundContainer>();
                foreach (CompoundContainer container in compoundContainers)
                {
                    if (container.getValue("caliber").ToLower().Contains(name.ToLower()))
                        data.Add(container);
                }
                return data;
            }
            return compoundContainers;
        }

        public List<string> getCalibers(string name)
        {
            return getCaliberRaw(name).Select(x => $"[{x.getValue("id")}] {x.getValue("caliber")}").ToList();
        }
        public List<string> getCaliberIDs(string name)
        {
            return getCaliberRaw(name).Select(x => x.getValue("id")).ToList();
        }
        public CompoundContainer getFirst(string name)
        {
            List<CompoundContainer> data = getCaliberRaw(name);
            if (data.Count == 0)
                return null;
            return data[0];
        }
        public CompoundContainer getFirstID(string id)
        {
            CompoundContainer data = compoundContainers.First(x => x.getValue("id") == id);
            return data;
        }
    }
}
