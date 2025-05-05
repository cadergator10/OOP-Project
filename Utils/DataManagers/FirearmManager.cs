using OOP_Project.Utils.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataManagers
{
    public class FirearmManager : BaseManager
    {
        List<string> idList = new List<string>(); //used ids.
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
            for (int i = 1; i < list.Count; i++)
            {
                string str = list[i];
                CompoundContainer container = new CompoundContainer();
                container.Load(str);
                idList.Add(container.getValue("id"));
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

        public void addFirearm(String name)
        {
            CompoundContainer container = new CompoundContainer();
            container.Add(new BaseContainer("name", name));
            container.Add(new BaseContainer("caliber", "9mm"));
            int i = 1;
            while (idList.Contains(i.ToString()))
            {
                i++;
            }
            idList.Add(i.ToString());
            container.Add(new BaseContainer("id", i));
            compoundContainers.Add(container);
        }

        public override string getName()
        {
            return "firearms";
        }

        public List<string> getFirearmNames()
        {
            List<string> data = new List<string>();
            foreach(CompoundContainer container in compoundContainers)
            {
                data.Add(container.getValue("name"));
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
                //if (settings.getValue("") == null)
                //{
                //    settings.setValue("id", "");
                //}
            }
            return settings;
        }
    }
}
