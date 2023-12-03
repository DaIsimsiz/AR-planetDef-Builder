using static Modules.PlanetDef;
using static Modules.Interface;
using static Modules.Msg;

using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace ARplanetDefBuilder
{
    class Program
    {
        static readonly Galaxy galaxy = new();
        static readonly Modules.PlanetDef.Path path = new();

        static void Main()
        {
            string? input;
            while(true) {
                LoadObject(galaxy.GetProperty(path.FullPath));
                InputPrompt();
                #pragma warning disable
                input = Console.ReadLine().ToLower();
                #pragma warning restore
                AnalyzeInput(input);
            }
        }

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
                    "back         => Go back one step");
        }
        static void AnalyzeInput(string input){
            if(input.StartsWith("edit ")) {
                if(input[5] == 'p') {
                    if(int.TryParse(input[6..], out _)) {
                        int index = 0;
                        string name = galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[6..])).Name.ToString();
                        for(int i = 0;i <= int.Parse(input[6..]);i++) {
                            if(galaxy.GetProperty(path.FullPath).Elements().ElementAt(i).Name.ToString() == name) index++;
                        }
                        path.GoTo(name, index);
                    }
                }
            }
            if(input.StartsWith("del ")) {
                if(input[4] == 'p') {
                    if(int.TryParse(input[5..], out _)) {
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
                                #pragma warning disable
                                galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Attribute("name").Value.ToString()
                                #pragma warning restore
                                :
                                galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[5..])).Name.ToString();
                        ///// Fancy ternary operator
                        while(true) {
                            SendMessages(true,
                                $"Are you sure you want to delete {displayName}?",
                                "y/n");
                            #pragma warning disable
                            input = Console.ReadLine();
                            #pragma warning restore
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
            }
            else if(input.StartsWith("back")) {
                path.Back();
            }
            else if(input.StartsWith("new ")) { //WIP
                switch(path.Depth()) {
                    case 1:
                        galaxy.SetProperty(path.FullPath, Modules.NewCBody.NewStar(false));
                        break; //New star system
                    case 2:
                        SendMessages(false,
                        "new <object> => Add a new element/attribute (star/planet/attribute)");
                        break; //New planet/binary star/star attribute
                    case 3:
                        if(path.Last() == "star") SendMessages(false, "new <object> => Add a new element/attribute (attribute)"); //Binary star
                        else SendMessages(false, "new <object> => Add a new element/attribute (attribute/property/moon)"); //Planet
                        break; //New binary star attribute //New planet attribute/planet property/moon
                    case 4:
                        SendMessages(false, "new <object> => Add a new element/attribute (attribute/property)"); //Moon
                        break; //New moon attribute/moon property
                }
            }
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
Remove the possibility of null references via commands such as "del p0" when there are no properties.
Disable the removing of some key properties and attributes to avoid breaking the code or the file. e.g. "name" attribute of planets and stars.
Complete the "new" command so it makes sense.
*/