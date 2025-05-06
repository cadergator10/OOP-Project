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
    public class BaseContainer //Capable of holding different datatypes
    {
        //Values in [] = name
        //Values in () = type
        //Values in "" = value

        private int IntData;
        private float FloatData;
        private string? StringData;
        private bool BoolData;
        public enum datatypes
        {
            Int, Float, Bool, String
        }
        private datatypes type;
        public String name;

        public BaseContainer()
        {
            name = "null";
        }
        public BaseContainer(string name, string value)
        {
            this.name = name;
            type = datatypes.String;
            StringData = value;
        }
        public BaseContainer(string name, int value)
        {
            this.name = name;
            type = datatypes.Int;
            IntData = value;
        }
        public BaseContainer(string name, float value)
        {
            this.name = name;
            type = datatypes.Float;
            FloatData = value;
        }
        public BaseContainer(string name, bool value)
        {
            this.name = name;
            type = datatypes.Bool;
            BoolData = value;
        }

        public int Parse(String str, int pos)
        {
            while (str[pos] != '/')
            {
                Console.WriteLine(str[pos]);
                if (str[pos] == '[')
                {
                    pos++;
                    name = "";
                    while (str[pos] != ']') //buildname
                    {
                        name += str[pos];
                        pos++;
                    }
                }
                else if (str[pos] == '(')
                {
                    pos++;
                    string typer = "";
                    while (str[pos] != ')') //build type
                    {
                        typer += str[pos];
                        pos++;
                    }
                    switch (typer)
                    {
                        case "int":
                            type = datatypes.Int;
                            break;
                        case "float":
                            type = datatypes.Float;
                            break;
                        case "string":
                            type = datatypes.String;
                            break;
                        case "bool":
                            type = datatypes.Bool;
                            break;
                    }
                }
                else if (str[pos] == '"')
                {
                    pos++;
                    string temp = "";
                    while (str[pos] != '"') //build value
                    {
                        temp += str[pos];
                        pos++;
                    }
                    if (type == datatypes.String)
                        StringData = temp;
                    else if (type == datatypes.Bool)
                        BoolData = temp.ToLower() == "true";
                    else if (type == datatypes.Int)
                        IntData = int.Parse(temp);
                    else if(type == datatypes.Float)
                        FloatData = float.Parse(temp);
                }
                pos++;
            }
            return pos + 1;
        }
        public string Encode()
        {
            String typeT = "";
            String valueT = "";
            switch (type){
                case datatypes.String:
                    typeT = "string";
                    valueT = StringData;
                    break;
                case datatypes.Int:
                    typeT = "int";
                    valueT = IntData.ToString();
                    break;
                case datatypes.Float:
                    typeT = "float";
                    valueT = FloatData.ToString();
                    break;
                case datatypes.Bool:
                    typeT = "bool";
                    valueT = BoolData ? "true" : "false";
                    break;
            }
            return $"[{name}]({typeT})\"{valueT}\"";
        }
        public string getValue()
        {
            switch (type)
            {
                case datatypes.String:
                    return StringData;
                case datatypes.Int:
                    return IntData.ToString();
                case datatypes.Float:
                    return FloatData.ToString();
                case datatypes.Bool:
                    return BoolData ? "true" : "false";
            }
            return "null";
        }
        public void setValue(string value)
        {
            try
            {
                switch (type)
                {
                    case datatypes.String:
                        StringData = value;
                        break;
                    case datatypes.Int:
                        IntData = int.Parse(value);
                        break;
                    case datatypes.Float:
                        FloatData = float.Parse(value);
                        break;
                    case datatypes.Bool:
                        BoolData = value == "true" ? true : false;
                        break;
                }
            }
            catch {
                Console.WriteLine("Error parsing");
            }
        }
    }
}
