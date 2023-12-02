using static Modules.PlanetDef;
using static Modules.Interface;
using static Modules.Msg;

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
                    "new       => Add a new element/attribute (star)");
                    break;
                case 2:
                    SendMessages(false,
                    "new       => Add a new element/attribute (star/planet/attribute)");
                    break;
                case 3:
                    if(path.Last() == "star") SendMessages(false, "new       => Add a new element/attribute (attribute)"); //Binary star
                    else SendMessages(false, "new       => Add a new element/attribute (attribute/property/moon)"); //Planet
                    break;
                case 4:
                    SendMessages(false, "new       => Add a new element/attribute (attribute/property)"); //Moon
                    break;
            }
            SendMessages(false,
                    "edit <ID> => Modify the specified property/attribute",
                    "del <ID>  => Delete the specified property/attribute");
        }
        static void AnalyzeInput(string input){
            int index;
            if(input.StartsWith("edit ")) {
                if(input[5] == 'p') {
                    if(int.TryParse(input[6..], out _)) {
                        Console.WriteLine(galaxy.GetProperty(path.FullPath).Elements().ElementAt(int.Parse(input[6..]))); //Continue here//
                    }
                }
            }
            if(input.StartsWith("del ")) {}
            switch(path.Depth()) { //a planet or moon property should enter here
                case 1:
                    SendMessages(false,
                    "new       => Add a new element/attribute (star)");
                    break;
                case 2:
                    SendMessages(false,
                    "new       => Add a new element/attribute (star/planet/attribute)");
                    break;
                case 3:
                    if(path.Last() == "star") SendMessages(false, "new       => Add a new element/attribute (attribute)"); //Binary star
                    else SendMessages(false, "new       => Add a new element/attribute (attribute/property/moon)"); //Planet
                    break;
                case 4:
                    SendMessages(false, "new       => Add a new element/attribute (attribute/property)"); //Moon
                    break;
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