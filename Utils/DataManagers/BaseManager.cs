using OOP_Project.Utils.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataManagers
{
    public abstract class BaseManager
    {
        public abstract bool isMine(String type);
        public abstract void parseData(List<String> list);
        public abstract List<String> getData();
        public abstract string getName();
        public abstract CompoundContainer getSettings();
    }
}
