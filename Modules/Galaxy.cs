using System.Xml.Linq;
using System.Xml.XPath;
using System.Text.RegularExpressions;

namespace Modules
{
    class PlanetDef
    {
        public class Galaxy
        {
            #pragma warning disable //We *do* want to get null references.
                                    //Getting a null reference will be helpful to decide if the element exists or not. This will be accounted for in the rest of the code.
            XDocument Document = new(new XElement("galaxy", Modules.Default.Sol()))
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
            /// Changes the property at the specified path. (Sending a null string deletes the property instead.)
            /// </summary>
            public void SetProperty(string path, string value)
            {
                if(value != null) Document.XPathSelectElement(path).Value = value;
                Document.XPathSelectElement(path).Remove();
            }
            /// <summary>
            /// Changes/Adds the property at the specified path.
            /// </summary>
            public void SetProperty(string path, XElement property)
            {
                Document.XPathSelectElement(path).Add(property);
            }



            /// <summary>
            /// Exports the galaxy into a file.
            /// </summary>
            public void Export(string path = null) {
                //Console.WriteLine(Document);
                Document.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\planetDefsEXPORT.xml");
            }
        }

        public class Path
        {
            public string FullPath = @"./galaxy";
            
            /// <summary>
            /// Returns the element the path currently points at.
            /// </summary>
            public string Last(){
                string x = Regex.Match(FullPath, $@"(?!\/)(?:.(?!\/))+$").Value;
                return x[..x.LastIndexOf('[')];
            }

            /// <summary>
            /// Returns the sub-element depth.
            /// </summary>
            public int Depth(){
                return Regex.Count(FullPath, $@"\/");
            }

            /// <summary>
            /// Goes to the specified element, assuming it exists.
            /// </summary>
            public void GoTo(string child, int index = 0){
                FullPath += @$"/{child}[{index}]";
            }

            /// <summary>
            /// Goes back to the previous element.
            /// </summary>
            public void Back(){
                if(FullPath == @"./galaxy") return;
                else{FullPath = FullPath[..FullPath.LastIndexOf('/')];}
            }
        }
    }
}