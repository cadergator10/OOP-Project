using OOP_Project.Utils.DataContainers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataManagers
{
    /// <summary>
    /// Author: Cade Kiser
    /// ID: 801341186
    /// </summary>
    public abstract class BaseManager
    {
        protected CompoundContainer settings;
        protected int idList = 0;
        protected bool settingsChecked = false;
        protected List<CompoundContainer> compoundContainers = new List<CompoundContainer>();
        public bool isMine(String type)
        {
            return type == getName();
        }
        public void parseData(List<String> list)
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
        public List<String> getData()
        {
            List<string> data = new List<string>();
            data.Add(getSettings().Save());
            foreach (CompoundContainer container in compoundContainers)
            {
                data.Add(container.Save());
            }
            return data;
        }
        public abstract string getName();
        public CompoundContainer getSettings()
        {
            if (settings == null)
            {
                settings = new CompoundContainer();
            }
            if (!settingsChecked)
            {
                checkSettingsValues();
            }
            return settings;
        }

        public virtual void checkSettingsValues()
        {
            if (settings.getValue("id") == "null")
            {
                settings.Add(new BaseContainer("id", "0"));
            }
            settingsChecked = true;
        }
    }
}
