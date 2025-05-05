using OOP_Project.Utils.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataManagers
{
    public class CaliberManager : BaseManager
    {
        int idList = 0;
        CompoundContainer settings;
        bool settingsChecked = false;
        List<CompoundContainer> compoundContainers = new List<CompoundContainer>();
        public override bool isMine(string type)
        {
            return type == getName();
        }

        public override void parseData(List<string> list)
        {
            settings = new CompoundContainer();
            settings.Load(list[0]);
            idList = int.Parse(getSettings().getValue("id"));
            for (int i = 1; i < list.Count; i++)
            {
                string str = list[i];
                CompoundContainer container = new CompoundContainer();
                container.Load(str);
                compoundContainers.Add(container);
            }
        }

        public override List<string> getData()
        {
            List<string> data = new List<string>();
            data.Add(getSettings().Save());
            foreach (CompoundContainer container in compoundContainers)
            {
                data.Add(container.Save());
            }
            return data;
        }

        public void addCaliber(String caliber)
        {
            CompoundContainer container = new CompoundContainer();
            container.Add(new BaseContainer("caliber", caliber));
            container.Add(new BaseContainer("id", idList++));
            getSettings().setValue("id", idList.ToString());
            compoundContainers.Add(container);
        }

        public override string getName()
        {
            return "caliber";
        }

        public List<string> getCalibers()
        {
            List<string> data = new List<string>();
            foreach(CompoundContainer container in compoundContainers)
            {
                data.Add(container.getValue("caliber"));
            }
            return data;
        }

        public override CompoundContainer getSettings()
        {
            if(settings == null)
            {
                settings = new CompoundContainer();
            }
            if (!settingsChecked) {
                if (settings.getValue("id") == null)
                {
                    settings.setValue("id", "0");
                }
            }
            return settings;
        }
    }
}
