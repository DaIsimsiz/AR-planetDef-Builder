using static Modules.DefaultSol;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Modules
{
    class PlanetDef
    {
        public class Galaxy
        {
            #pragma warning disable //We *do* want to get null references.
            XDocument Document = new(new XElement("galaxy", Default()))
            {
                Declaration = new XDeclaration("1.0", "UTF-8", "no")
            };

            /// <summary>
            /// Returns the attribute at the specified path.
            /// </summary>
            public XAttribute GetAttribute(string path, string attributeName)
            {
                return GetProperty(path).Attribute(attributeName);
            }
            /// <summary>
            /// Returns the property at the specified path.
            /// </summary>
            public XElement GetProperty(string path)
            {
                return Document.XPathSelectElement(path);
            }
            /// <summary>
            /// Changes/Removes/Adds the attribute at the specified path.
            /// </summary>
            public void SetAttribute(string path, string attributeName, string value)
            {
                Document.XPathSelectElement(path).Attribute(attributeName).Value = value;
            }
            /// <summary>
            /// Changes/Removes the property at the specified path.
            /// </summary>
            public void SetProperty(string path, string value)
            {
                if(value != null) Document.XPathSelectElement(path).Value = value;
                Document.XPathSelectElement(path).Remove();
            }
            /// <summary>
            /// Changes/Removes/Adds the property at the specified path.
            /// </summary>
            public void SetProperty(string path, XElement property)
            {
                Document.XPathSelectElement(path).Add(property);
            }


            public void Export(string path = null) {
                Console.WriteLine(Document);
                Document.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\planetDefsEXPORT.xml");
            }
        }
    }
}