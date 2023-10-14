using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Modules
{
    class PlanetDef
    {
        public class Galaxy
        {
            public static XDocument Document = new(new XElement("galaxy"))
            {
                Declaration = new XDeclaration("1.0", "UTF-8", "no")
            };

            
            public void AddSystem(XElement star)
            {
                #pragma warning disable
                //Adding to the galaxy (it's a star, thus a new star system) 
                Document.Element("galaxy").Add(star);
            }

            public void AddToBody(string bodyName, XElement body, string moonName = null)
            {
                //Adding to a star (it's either a binary star or a planet, doesn't matter)
                if(moonName == null) Document.Element("galaxy").Element(bodyName).Add(body);
                //Adding to a planet (it's a moon)
                else Document.Element("galaxy").Element(bodyName).Element(moonName).Add(body);
            }
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            public XElement GetSystem(string systemName) {
                return Document.Element("galaxy").Element(systemName);
            }
            public XElement GetBody(string systemName, string bodyName, string moonName = null) {
                //Return something that's in the star system (excluding the main star), planet or binary star
                if(moonName == null) return Document.Element("galaxy").Element(systemName).Element(bodyName);
                //Return a moon of a planet
                else return Document.Element("galaxy").Element(systemName).Element(bodyName).Element(moonName);
            }
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            public void SetSystem(string systemName, XElement modifiedBody) {
                Document.Element("galaxy").Element(systemName).Remove();
                Document.Element("galaxy").Add(modifiedBody);
            }
            public void SetBody(string systemName, string bodyName, XElement modifiedBody, string moonName = null) {
                if(moonName == null) {
                    Document.Element("galaxy").Element(systemName).Element(bodyName).Remove();
                    Document.Element("galaxy").Element(systemName).Add(modifiedBody);
                }else{
                    Document.Element("galaxy").Element(systemName).Element(bodyName).Element(moonName).Remove();
                    Document.Element("galaxy").Element(systemName).Element(bodyName).Add(modifiedBody);
                }
            }
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            public void RemoveSystem(string systemName) {
                Document.Element("galaxy").Element(systemName).Remove();
            }
            public void RemoveBody(string systemName, string bodyName, string moonName = null) {
                if(moonName == null) {
                    Document.Element("galaxy").Element(systemName).Element(bodyName).Remove();
                }else{
                    Document.Element("galaxy").Element(systemName).Element(bodyName).Element(moonName).Remove();
                }
            }
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            public void Export(string path = null) {
                Console.WriteLine(Document);

                Document.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\planetDefsEXPORT.xml");
            }
        }
    }
}