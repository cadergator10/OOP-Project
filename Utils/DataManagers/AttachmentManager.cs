using OOP_Project.Utils.DataContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project.Utils.DataManagers
{
    /// <summary>
    /// Author: Cade Kiser
    /// ID: 801341186
    /// </summary>
    public class AttachmentManager : BaseManager
    {

        public bool addAttachment(string name)
        {
            if (compoundContainers.Select(x => x.getValue("name")).Contains(name))
                return false;
            CompoundContainer container = new CompoundContainer();
            container.Add(new BaseContainer("name", name));
            container.Add(new BaseContainer("id", idList++));
            getSettings().setValue("id", idList.ToString());
            //Update all current guns
            Program.fm.attachmentUpdate(false, container.getValue("id"));
            compoundContainers.Add(container);
            return true;
        }

        public bool removeAttachment(string id)
        {
            CompoundContainer container = compoundContainers.FirstOrDefault(x => x.getValue("id") == id);
            if (container != null)
            {
                compoundContainers.Remove(container);
                Program.fm.attachmentUpdate(true, id);
            }
            return container != null;
        }

        public override string getName()
        {
            return "attachments";
        }

        private List<CompoundContainer> getAttachmentRaw(string name)
        {
            if (name != null)
            {
                List<CompoundContainer> data = new List<CompoundContainer>();
                foreach (CompoundContainer container in compoundContainers)
                {
                    if (container.getValue("name").ToLower().Contains(name.ToLower()))
                        data.Add(container);
                }
                return data;
            }
            return compoundContainers;
        }

        public List<string> getAttachments(string name)
        {
            return getAttachmentRaw(name).Select(x => $"[{x.getValue("id")}] {x.getValue("name")}").ToList();
        }
        public List<string> getAttachmentIDs(string name)
        {
            return getAttachmentRaw(name).Select(x => x.getValue("id")).ToList();
        }
        public CompoundContainer getFirst(string name)
        {
            List<CompoundContainer> data = getAttachmentRaw(name);
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
