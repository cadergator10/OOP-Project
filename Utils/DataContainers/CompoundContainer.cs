using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataContainers
{
    /// <summary>
    /// Author: Cade Kiser
    /// ID: 801341186
    /// </summary>
    public class CompoundContainer
    {
        List<BaseContainer> containers = new List<BaseContainer>();
        public void Load(string line)
        {
            containers = new List<BaseContainer>();
            int pos = 0;
            while (pos < line.Length)
            {
                BaseContainer container = new BaseContainer();
                pos = container.Parse(line, pos);
                containers.Add(container);
            }
        }
        public string Save()
        {
            if(containers.Count > 0)
            {
                String str = "";
                for (int i = 0; i < containers.Count; i++)
                {
                    str += containers[i].Encode();
                    str += '/';
                }
                return str;
            }
            return null;
        }
        public void Add(BaseContainer container)
        {
            containers.Add(container);
        }
        public string getValue(string name)
        {
            foreach(BaseContainer container in containers)
            {
                if(container.name == name)
                    return container.getValue();
            }
            return "null";
        }
        public void setValue(string name, string value)
        {
            foreach (BaseContainer container in containers)
            {
                if (container.name == name)
                {
                    container.setValue(value);
                    break;
                }
            }
        }
        public void Remove(string name)
        {
            foreach (BaseContainer container in containers)
            {
                if (container.name == name)
                    containers.Remove(container);
            }
        }
        public List<BaseContainer> getAll()
        {
            return containers;
        }
    }
}
