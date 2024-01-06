using static Modules.Basics;

using System.Xml.Linq;

namespace Modules
{
    /// <summary>
    /// A class with methods with the purpose of sending messages in the console in a way that makes it easy for the user to interpret what is happening.
    /// </summary>
    class Interface
    {
        /// <summary>
        /// Neatly organizes and shows all elements and attributes of an XElement.
        /// </summary>
        public static void LoadObject(XElement body)
        {
            Console.Clear();
            //This is coded in a way that will work for any amount of attributes, even if there arent more than 10.

            int idL = (body.Attributes().Count() > body.Elements().Count()) ? (body.Attributes().Count() - 1).ToString().Length : (body.Elements().Count() - 1).ToString().Length;
            int nameL = 0;
            foreach(XAttribute x in body.Attributes()) {nameL = x.Name.ToString().Length > nameL ? x.Name.ToString().Length : nameL;}
            foreach(XElement x in body.Elements()) {nameL = x.Name.ToString().Length > nameL ? x.Name.ToString().Length : nameL;}
            int attributeC = body.Attributes().Count();
            int propertyC = body.Elements().Count();
            

            Console.Write($"Body name: ");

            switch(body.Name.ToString()) {
                case "galaxy": Console.WriteLine("Galaxy");break;
                case "star"  : Console.WriteLine(body.Attribute("name").Value);break;
                case "planet": Console.WriteLine(body.Attribute("name").Value);break;
            }

            Console.WriteLine("\nAttributes:");
            for(int i = 0; i < attributeC; i++) {
                Console.Write("ID: a" + i);
                for(int x = i.ToString().Length; x < idL; x++) {Console.Write(" ");}
                Console.Write($" | {body.Attributes().ElementAt(i).Name} ");
                for(int x = body.Attributes().ElementAt(i).Name.ToString().Length; x < nameL; x++) {Console.Write(" ");}
                Console.Write($"> Value: {body.Attributes().ElementAt(i).Value}");
                Console.WriteLine();
            }

            Console.WriteLine("\nProperties:");
            for(int i = 0; i < propertyC; i++) {
                Console.Write("ID: p" + i);
                for(int x = i.ToString().Length; x < idL; x++) {Console.Write(" ");}
                Console.Write($" | {body.Elements().ElementAt(i).Name} ");
                for(int x = body.Elements().ElementAt(i).Name.ToString().Length; x < nameL; x++) {Console.Write(" ");}
                switch(body.Elements().ElementAt(i).Name.ToString()) {
                    case "planet": Console.Write($"> {body.Elements().ElementAt(i).Attribute("name").Value}");break;
                    case "star"  : Console.Write($"> {body.Elements().ElementAt(i).Attribute("name").Value}");break;
                    default      : Console.Write($"> {body.Elements().ElementAt(i).Value}");break;
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Prompts the user to modify the attribute of their choice.
        /// </summary>
        public static (string?, string?) MissingAttributes(XElement body, Dictionary<string, string> dict)
        {
            List<string> missing = new();
            foreach(string a in dict.Keys) {
                if(body.Attributes().Any(att => att.Name.ToString() == a)) continue;
                else missing.Add(a);
            }
            for(int i = 0;i < missing.Count;i++) if(dict[missing[i]] == "unknown") missing.RemoveAt(i);
            string? input;
            string attributeN;
            string value;
            if(missing.Count == 0) return (null, null);

            while(true) {
                SendMessages(true, "Here is a list of attributes you may add (cAsE sEnSiTiVe):\n");
                foreach(string a in missing) Console.WriteLine(a);
                Console.WriteLine();
                input = Console.ReadLine();
                if(!missing.Contains(input)) {
                    if(input == "" || input == string.Empty || input == null) return (null, null);
                    else continue;
                }
                else{attributeN = input;break;}
            }
            while(true) {
                SendMessages(true, $"Enter a valid value for {attributeN}\n{dict[attributeN]}\n");
                input = Console.ReadLine();
                if(!StellarGen.IsValid(attributeN, input)) continue;
                value = input;
                return (attributeN, value);
            }
        }
        /// <summary>
        /// Prompts the user to modify the property of their choice.
        /// </summary>
        public static (string, string) MissingProperties(XElement body)
        {
            Dictionary <string, string> dict = References.TerrestrialPlanetSpecifications;
            foreach(var KVPair in References.GaseousPlanetSpecifications) {
                if(!dict.ContainsKey(KVPair.Key)) dict.Add(KVPair.Key, KVPair.Value);
            }

            List<string> missing = new();
            foreach(string a in dict.Keys) {
                if(body.Elements().Any(prop => prop.Name.ToString() == a)) continue;
                else missing.Add(a);
            }
            for(int i = 0;i < missing.Count;i++) if(dict[missing[i]] == "unknown") missing.RemoveAt(i);
            string? input;
            string propertyName;
            string value;
            if(missing.Count == 0) return (null, null);

            while(true) {
                SendMessages(true, "Here is a list of properties you may add (cAsE sEnSiTiVe):\n");
                foreach(string a in missing) Console.WriteLine(a);
                Console.WriteLine();
                input = Console.ReadLine();
                if(!missing.Contains(input)) {
                    if(input == "" || input == string.Empty || input == null) return (null, null);
                    else continue;
                }
                else{propertyName = input;break;}
            }
            while(true) {
                SendMessages(true, $"Enter a valid value for {propertyName}\n{dict[propertyName]}\n");
                input = Console.ReadLine();
                if(!StellarGen.IsValid(propertyName, input)) continue;
                value = input;
                return (propertyName, value);
            }
        }
    }
}