using static Modules.PlanetDefs;
using static Modules.Interface;
using static Modules.Basics;

using System.Xml.Linq;
using System.Text.Json;


namespace ARplanetDefBuilder
{
    class AppData
    {
        public string saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public bool neverAsk = false;
    }
    class Program
    {
        static readonly Galaxy galaxy = new();
        static readonly Modules.PlanetDefs.Path path = new();

        static readonly string dataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\planetDefs-Builder";

        static void Main()
        {
            Console.Title = "planetDefs Builder";
            string? input;
            while(true) {
                LoadObject(galaxy.GetProperty(path.FullPath));
                Console.WriteLine();
                InputPrompt();
                
                input = Console.ReadLine().ToLower();
                
                AnalyzeInput(input);
            }
        }

        /// <summary>
        /// Sends some console messages, prompting the user to send a command for the app.
        /// </summary>
        static void InputPrompt() {
            switch(path.Depth()) { //a planet or moon property should enter here
                case 1:
                    SendMessages(false,
                    "new <object> => Add a new element/attribute (star)");
                    break;
                case 2:
                    SendMessages(false,
                    "new <object> => Add a new element/attribute (star/planet/attribute)");
                    break;
                case 3:
                    if(path.Last() == "star") SendMessages(false, "new <object> => Add a new element/attribute (attribute)"); //Binary star
                    else SendMessages(false, "new <object> => Add a new element/attribute (attribute/property/moon)"); //Planet
                    break;
                case 4:
                    SendMessages(false, "new <object> => Add a new element/attribute (attribute/property)"); //Moon
                    break;
            }
            SendMessages(false,
                    "edit <ID>    => Modify the specified property/attribute",
                    "del <ID>     => Delete the specified property/attribute",
                    "back         => Go back one step",
                    "export       => Export the galaxy to desktop");
        }

        /// <summary>
        /// Analyzes the given input with regard for the current element.
        /// </summary>
        static void AnalyzeInput(string? input){
            if(input.StartsWith("edit ")) {
                if(input[5] == 'p' && int.TryParse(input[6..], out _)) {
                    if(int.Parse(input[6..]) < galaxy.GetProperty(path.FullPath).Elements().Count()) {
                        if(galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[6..])).Name.ToString() == "star" ||
                            galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[6..])).Name.ToString() == "planet") {
                            int index = 0;
                            string name = galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[6..])).Name.ToString();
                            for(int i = 0;i <= int.Parse(input[6..]);i++) {
                                if(galaxy.GetProperty(path.FullPath).Elements().ElementAt(i).Name.ToString() == name) index++;
                            }
                            path.GoTo(name, index);
                        }
                        else {
                            int index = 0;
                            string name = galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[6..])).Name.ToString();
                            for(int i = 0;i <= int.Parse(input[6..]);i++) {
                                if(galaxy.GetProperty(path.FullPath).Elements().ElementAt(i).Name.ToString() == name) index++;
                            }
                            while(true) {
                                SendMessages(true, $"Enter a new valid value for {name}.");
                                
                                input = Console.ReadLine();
                                if(Modules.StellarGen.IsValid(name, input)) {
                                
                                    path.GoTo(name, index);
                                    galaxy.SetProperty(path.FullPath, input);
                                    path.Back();
                                    break;
                                }
                            }
                        }
                    }
                }
                else if(input[5] == 'a' && int.TryParse(input[6..], out _)) {
                    if(int.Parse(input[6..]) < galaxy.GetProperty(path.FullPath).Attributes().Count()) {
                        string attribute = galaxy.GetProperty(path.FullPath).Attributes().ElementAt(int.Parse(input[6..])).Name.ToString();
                        while(true) {
                            SendMessages(true, $"Enter a new valid value for {attribute}.");
                            
                            input = Console.ReadLine();
                            if(Modules.StellarGen.IsValid(attribute, input)) {
                            
                                galaxy.SetAttribute(path.FullPath, attribute, input);
                                break;
                            }
                        }
                    }
                }
            }

            if(input.StartsWith("del ")) {
                if(input[4] == 'p' && int.TryParse(input[5..], out _)) {
                    if(int.Parse(input[5..]) < galaxy.GetProperty(path.FullPath).Elements().Count()) {
                        /////
                        int index = 0;
                        string name = galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Name.ToString();
                        for(int i = 0;i <= int.Parse(input[5..]);i++) {
                            if(galaxy.GetProperty(path.FullPath).Elements().ElementAt(i).Name.ToString() == name) index++;
                        }
                        /////
                        int id = int.Parse(input[5..]);
                        string displayName = 
                            (
                                galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Name.ToString() == "star" ||
                                galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Name.ToString() == "planet"
                            )
                                ?
                                    
                                    galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Attribute("name").Value.ToString()
                                    
                                :
                                    galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Name.ToString();
                        ///// Fancy ternary operator
                        while(true) {
                            SendMessages(true,
                                $"Are you sure you want to delete {displayName}?",
                                "y/n");

                            
                            input = Console.ReadLine().ToLower();
                            

                            if(input == "y") {
                                path.GoTo(name, index);
                                galaxy.SetProperty(path.FullPath, string.Empty);
                                path.Back();
                                break;
                            }
                            else if(input == "n") break;
                        }
                    }
                }
                else if(input[4] == 'a' && int.TryParse(input[5..], out _)) {
                    if(int.Parse(input[5..]) < galaxy.GetProperty(path.FullPath).Attributes().Count()) {
                        string displayName = galaxy.GetProperty(path.FullPath).Attributes().ElementAt(int.Parse(input[5..])).Name.ToString();
                        while(true) {
                            SendMessages(true,
                                $"Are you sure you want to delete {displayName}?",
                                "y/n");

                            
                            input = Console.ReadLine().ToLower();
                            

                            if(input == "y") {
                                galaxy.SetAttribute(path.FullPath, displayName, string.Empty);
                                break;
                            }
                            else if(input == "n") break;
                        }
                    }
                }
            }
            else if(input.StartsWith("back")) {
                path.Back();
            }
            else if(input.StartsWith("export") && path.Depth() == 1) {
                AppData data;
                bool changed = false;
                bool loop = true;
                if(!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);
                if(!File.Exists(dataDir + @"\saveDir.json")) File.Create(dataDir + @"\saveDir.json");
                data = ValidData(File.ReadAllText(dataDir));
                while(!data.neverAsk && loop) {
                    SendMessages(true, "Current export directory is:", $"{data.saveDirectory}", "Enter \"continue\" if you want to proceed (enter \"continue -neverAsk\" if you don't want to see this message anymore)", "Enter \"edit\" if you want to change the directory");
                    switch (Console.ReadLine().ToLower()) {
                        case "continue -neverask":
                            data.neverAsk = true;
                            changed = true;
                            loop = false;
                            break;
                        case "continue":
                            loop = false;
                            break;
                        case "edit":
                            while(true) {
                                SendMessages(true, "Enter a new calid directory");
                                
                                input = Console.ReadLine();
                                
                                if(!Directory.Exists(input)) continue;
                                else {
                                    data.saveDirectory = input;
                                    changed = true;
                                    loop = false;
                                    break;
                                }
                            }
                            break;
                    }
                }
                if(changed) File.WriteAllText(dataDir, JsonSerializer.Serialize(data));
                galaxy.Export(data.saveDirectory);
                System.Environment.Exit(1);
            }
            else if(input.StartsWith("new ")) {
                switch(path.Depth(), input[4..]) {

                    case (1, "star"):
                        galaxy.SetProperty(path.FullPath, Modules.StellarGen.NewStar(false));
                        break; //New star system



                    case (2, "star"):
                        galaxy.SetProperty(path.FullPath, Modules.StellarGen.NewStar(true));
                        break; //New binary star
                    case (2, "planet"):
                        while(true) {
                            SendMessages(true, "Would you like the planet to be generated randomly?", "y/n");
                            
                            input = Console.ReadLine().ToLower();
                            
                            if(input == "n") {galaxy.SetProperty(path.FullPath, Modules.StellarGen.NewPlanet());break;}
                            else{
                                while(true) {
                                    SendMessages(true, "Should the planet be Gaseous or Terrestrial?", "gaseous/terrestrial");
                                    
                                    input = Console.ReadLine().ToLower();
                                    
                                    if(input == "terrestrial") {galaxy.SetAttribute(path.FullPath, "numPlanets", (int.Parse(galaxy.GetAttribute(path.FullPath, "numPlanets").Value.ToString()) + 1).ToString());break;}
                                    else if(input == "gaseous") {galaxy.SetAttribute(path.FullPath, "numGasGiants", (int.Parse(galaxy.GetAttribute(path.FullPath, "numGasGiants").Value.ToString()) + 1).ToString());break;}
                                }
                                break;
                            } 
                        }
                        break; //New planet
                    case (2, "attribute"):
                        (string, string) attInfoS = MissingAttributes(galaxy.GetProperty(path.FullPath), Modules.References.StarAttributes);
                        galaxy.SetAttribute(path.FullPath, attInfoS.Item1, attInfoS.Item2);
                        break; //New star attribute
                    


                    case (3, "attribute"):
                        if(path.Last() == "star") {
                            (string, string) attInfoB = MissingAttributes(galaxy.GetProperty(path.FullPath), Modules.References.StarAttributes);
                            galaxy.SetAttribute(path.FullPath, attInfoB.Item1, attInfoB.Item2);
                        } //Binary star attribute
                        else {
                            (string, string) attInfoP = MissingAttributes(galaxy.GetProperty(path.FullPath), Modules.References.PlanetAttributes);
                            galaxy.SetAttribute(path.FullPath, attInfoP.Item1, attInfoP.Item2);
                        } //Planet attribute
                        break;
                    case (3, "property"):
                        if(path.Last() == "star") break;
                        else {
                            (string, string) attInfoP = MissingAttributes(galaxy.GetProperty(path.FullPath), Modules.References.PlanetAttributes);
                            galaxy.SetAttribute(path.FullPath, attInfoP.Item1, attInfoP.Item2);
                        }
                        break; //New planet property
                    case (3, "moon"):
                        if(path.Last() == "star") break;
                        else {
                            galaxy.SetProperty(path.FullPath, Modules.StellarGen.NewPlanet());
                        }
                        break; //New moon



                    case (4, "attribute"):
                        (string, string) attInfoM = MissingAttributes(galaxy.GetProperty(path.FullPath), Modules.References.PlanetAttributes);
                        galaxy.SetAttribute(path.FullPath, attInfoM.Item1, attInfoM.Item2);
                        break; //New moon attribute
                    case (4, "property"):
                        (string, string) propInfoM = MissingProperties(galaxy.GetProperty(path.FullPath));
                        galaxy.SetProperty(path.FullPath, new XElement(propInfoM.Item1, propInfoM.Item2));
                        break; //New moon property
                }
            }
        }
        static AppData ValidData(string data) {
            try
            {
                return JsonSerializer.Deserialize<AppData>(data);
            }
            catch(Exception) {return new AppData();}
        }
    }
}

/*
KKKKKKKKKKKKKKKKKKKKKKKKKKKK0KKKKKKKKKKKKKKKK
KKKKKKKKKKKKKKKOkxddooooddxkO0KKKKKKKKKKKKKKK
KKKKKKKKK0K0xooooddxdlcdxdooooox0KKKKK0KKKKKK
000000000OolldO0K00KOdokK00K0OdlldO0000000000
0000000Odclk00000kxdlccldxk00000klcdO00000000
000000klcx0000koloooddddooolok0000xclk0000000
OOOOOkcckOOOkl;:oxkkkkkkkkxo:,lkOOOkcck0OOOOO
OOOOOl:xOOOk:  ..,;;::::;;,.. .:kOOOx:lOOOOOO
OOOOx:cOOOOl.  .,,;cllllc;,,.  .lOOOOc:xOOOOO
kkkOd;lOkOx;   ',;codxxdoc;,'   ;kOkOl;dOkkkk
kkkkd;lkkkk:   .,,:loddol:,,.   :xkkkl;dkkkkk
kkkkx::xkkxl.   .',;;:::;,'..  .okkkx::xkkkkk
xxxxko;cxxdoc..';:ccccccccc;'..lxxxxl;okxxxxx
xxxxxxl;lxxdolc:codxxxxxxdlc:coxxxxl;lxxxxxxx
xxxxxxxl;:oxxxxdlcccc::ccccldxxxxo:;lxxxxxxxx
dddddddxoc;:ldxddxddl::ccodddxdl:;:oddddddddd
ddddddddddoc:::cloddo::looolc::;coddddddddddd
dddddddddddddoc:::::;,,;:::::codddddddddddddd
oooooooooooodooddooolllloooddoooooooooooooooo
ooooooooooooooooooooooooooooooooooooooooooooo
*/


/*
To do
-------------------
Disable the removing of some key properties and attributes to avoid breaking the code or the file. e.g. "name" attribute of planets and stars.
*/