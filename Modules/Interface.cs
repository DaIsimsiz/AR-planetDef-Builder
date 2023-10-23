using System.Xml.Linq;
using static Modules.MessageSend;

namespace Modules
{
    class Interface
    {
        /// <summary>
        /// Writes the children of the element of your choice.
        /// </summary>
        public static void LoadObject(XElement body)
        {
            //This is coded in a way that will work for any amount of attributes, even if there arent more than 10.

            int idL = (body.Attributes().Count() > body.Elements().Count()) ? (body.Attributes().Count() - 1).ToString().Length : (body.Elements().Count() - 1).ToString().Length;
            int nameL = 0;
            foreach(XAttribute x in body.Attributes()) {
                nameL = x.Name.ToString().Length > nameL ? x.Name.ToString().Length : nameL;
            }
            foreach(XElement x in body.Elements()) {
                nameL = x.Name.ToString().Length > nameL ? x.Name.ToString().Length : nameL;
            }
            int attributeC = body.Attributes().Count();
            int propertyC = body.Elements().Count();
            

            Console.Write($"Body name: ");

            switch(body.Name.ToString()) {
                case "galaxy":
                    Console.WriteLine("Galaxy");
                    break;
                case "star":
                    Console.WriteLine(body.Attribute("name").Value);
                    break;
                case "planet":
                    Console.WriteLine(body.Attribute("name").Value);
                    break;
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
                    //#pragma warning disable
                    case "planet":
                        Console.Write($"> {body.Elements().ElementAt(i).Attribute("name").Value}");
                        break;
                    case "star":
                        Console.Write($"> {body.Elements().ElementAt(i).Attribute("name").Value}");
                        break;
                    default:
                        Console.Write($"> {body.Elements().ElementAt(i).Value}");
                        break;
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Enables the user to modify a value of your choice.
        /// </summary>
        public static void ModifyValue(params string[] messages)
        {
            Console.Clear();
            foreach(string message in messages) Console.WriteLine(message);
        }
    }
}