using System.Xml.Linq;
using System.Xml.XPath;
using System.Text.RegularExpressions;

namespace Modules
{
    /// <summary>
    /// A class housing all methods related to manipulating the planetDefs XML document.
    /// </summary>
    class PlanetDefs
    {
        /// <summary>
        /// A custom made Galaxy class, made to make the job of editing the galaxy a lot easier.
        /// Without this, the code would definitely be a lot longer and confusing.
        /// </summary>
        public class Galaxy
        {
            XDocument Document;

            /// <summary>
            /// Constructor.
            /// </summary>
            public Galaxy(string import = null) {
                try{Document = XDocument.Parse(File.ReadAllText(import));}
                catch(Exception){ Document = new(new XElement("galaxy", References.Sol())){Declaration = new XDeclaration("1.0", "UTF-8", "no")};}
            }



            public void LoadSave() {
                try{
                    Document = XDocument.Parse(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\planetDefs-Builder\AUTOSAVE.xml"));
                }catch(Exception){}
            }



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
                if(value == null) Document.XPathSelectElement(path).Attribute(attributeName).Remove();
                else if(Document.XPathSelectElement(path).Attributes().Any(att => att.Name.ToString() == attributeName)) Document.XPathSelectElement(path).Attribute(attributeName).Value = value;
                else Document.XPathSelectElement(path).Add(new XAttribute(attributeName, value));
            }



            /// <summary>
            /// Changes the property at the specified path. (Sending a null string deletes the property instead.)
            /// </summary>
            public void SetProperty(string path, string value)
            {
                if(value == null) Document.XPathSelectElement(path).Remove();
                else Document.XPathSelectElement(path).Value = value;
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
            public void Export(string? path = null) {
                Document.Save(path == null ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\planetDefs.xml" : path + @"\planetDefs.xml");
                if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\planetDefs-Builder\AUTOSAVE.xml")) {
                    try{File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\planetDefs-Builder\AUTOSAVE.xml");}catch(Exception){}
                }
            }
            /// <summary>
            /// Autosave.
            /// </summary>
            public void Export() {
                while(true) {
                    try{Document.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\planetDefs-Builder\AUTOSAVE.xml");break;}
                    catch(Exception){}
                }
            }



            /// <summary>
            /// Imports a galaxy from a file.
            /// </summary>
            public void Import(XDocument import) {
                Document = import;
            }
        }

        /// <summary>
        /// A path class custom made to make the job of keeping track of our current path a lot easier.
        /// </summary>
        public class Path
        {
            public string FullPath;

            /// <summary>
            /// The constructor will default to the root, but you can set a custom path as the starting point.
            /// This is added to hard-code one thing less.
            /// </summary>
            public Path(string fullpath = @"./galaxy") {
                this.FullPath = fullpath;
            }
            
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