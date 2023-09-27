using System.ComponentModel;
using System.Xml.Linq;

namespace ARplanetDefBuilder
{
    class Program
    {
        static void Main()
        {
            XElement galaxy = new XElement("galaxy");

            SendMessages(new string[] {
                "Let's create the Solar system first!",
                "We will create the Sol, Earth, and Luna for you, but you can modify their properties if you wish so!"});

            galaxy.Add(Default());

            Task.Delay(5000);

            string? input;
            while(true) {
                SendMessages(new string[] {
                    "Does your galaxy need more stars?",
                    "You can enter \"star\" if so!",
                    "Or, if you are done, you can enter \"done\" and have the xml file exported to a file and sent to the console!"
                });
                input = Console.ReadLine();
                if(input == "star") galaxy.Add(NewStar(binary: false));
                else if(input == "done") break;
            }

            Console.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>");
            Console.WriteLine(galaxy);
        }

        /// <summary>
        /// Cleans the console and sends the new messages.
        /// </summary>
        static void SendMessages(params string[] messages)
        {
            Console.Clear();
            foreach(string message in messages) Console.WriteLine(message);
        }

        /// <summary>
        /// Generates the Sol, Earth, and Luna, referencing from my automatically generated planetDefs.xml file.
        /// </summary>
        static XElement Default()
    
        {
            return
            new XElement("star",
            new XAttribute("blackHole", "false"),
            new XAttribute("name", "Sol"),
            new XAttribute("numGasGiants", "0"),
            new XAttribute("numPlanets", "0"),
            new XAttribute("size", "1.0"),
            new XAttribute("temp", "100"),
            new XAttribute("x", "0"),
            new XAttribute("y", "0"),
            
            new XElement("planet",
            new XAttribute("DIMID", "0"),
            new XAttribute("dimMapping", ""),
            new XAttribute("name", "Earth"),
            new XElement("isKnown", "true"),
            new XElement("fogColor", "1.0,1.0,1.0"),
            new XElement("skyColor", "1.0,1.0,1.0"),
            new XElement("gravitationalMultiplier", "100"),
            new XElement("orbitalDistance", "100"),
            new XElement("orbitalTheta", "0"),
            new XElement("orbitalPhi", "0"),
            new XElement("retrograde", "false"),
            new XElement("avgTemperature", "287"),
            new XElement("rotationalPeriod", "24000"),
            new XElement("atmosphereDensity", "100"),
            new XElement("generateCraters", "false"),
            new XElement("generateCaves", "false"),
            new XElement("generateVolcanos", "false"),
            new XElement("generateStructures", "false"),
            new XElement("generateGeodes", "false"),

            new XElement("planet",
            new XAttribute("DIMID", "3"),
            new XAttribute("name", "Luna"),
            new XElement("isKnown", "false"),
            new XElement("fogColor", "1.0,1.0,1.0"),
            new XElement("skyColor", "1.0,1.0,1.0"),
            new XElement("gravitationalMultiplier", "16"),
            new XElement("orbitalDistance", "150"),
            new XElement("orbitalTheta", "0"),
            new XElement("orbitalPhi", "0"),
            new XElement("retrograde", "false"),
            new XElement("avgTemperature", "20"),
            new XElement("rotationalPeriod", "128000"),
            new XElement("atmosphereDensity", "0"),
            new XElement("generateCraters", "true"),
            new XElement("generateCaves", "false"),
            new XElement("generateVolcanos", "false"),
            new XElement("generateStructures", "false"),
            new XElement("generateGeodes", "false"),
            new XElement("biomeIds", "advancedrocketry:moon;30,advancedrocketry:moondark;30")
            )));
        }

        /// <summary>
        /// Initiates the creation of a new star.
        /// </summary>
        /// <param name="binary">If true, removes some options that are incompatible with binary stars.</param>
        /// <returns></returns>
        static XElement NewStar(bool binary)
        {
            string? input;
            XElement star = new XElement("star");
            SendMessages("Set a name for your new star, leave empty for random!");
            input = Console.ReadLine();
            star.Add(new XAttribute("name", input == null ? PickRandomName(true) : input));

            SendMessages(new string[] {
                "How hot is your star, multiply the heat you want by 58, floor it and enter your value if you want a realistic input!",
                "For reference, Sol's temperature is 100!",
                "Entering an invalid value or nothing will pick a random value between 40-850."
            });
            input = Console.ReadLine();
            star.Add(new XAttribute("temp", int.TryParse(input, out _) ? int.Parse(input) : new Random().Next(40, 850)));

            star.Add(new XAttribute("x", new Random().Next(-500, 500)));
            star.Add(new XAttribute("y", new Random().Next(-500, 500)));
            
            SendMessages(new string[] {
                "If you'd like to add another star, enter \"star\"",
                "If you'd like to add a planet, or an asteroid, enter \"planet\"",
                "If that's all, just enter \"exit\""
            });
            while(true) {
                switch(Console.ReadLine()) {
                    case "star":
                        //do stuff
                        break;
                    case "planet":
                        //do stuff
                        break;
                    case "exit":
                        return star;
                }
            }
        }

        /// <summary>
        /// Picks a random star name when true, otherwise a planet name.
        /// </summary>
        static string PickRandomName(bool isStar)
        {
            List<string> names;
            if(isStar) names = File.ReadAllLines("starNames.txt").ToList();
            else names = File.ReadAllLines("planetNames.txt").ToList();
            return names[new Random().Next(names.Count)];
        }
    }
}

/*
Star Specifications List
name - name
temp - 100 is default for sol
x - x
y - y
numPlanets - number of terrestrial planets randomly generated
numGasGiants - same with numPlanets but for gas giants
blackHole - true or false


Binar Star Specification Example 
        <galaxy>
            <star name="Sol" temp="100" x="0" y="0" numPlanets="1" numGasGiants="0" blackHole="false">
                <star name="Sol-2" temp="200" separation="10.0" />
                <planet name="Jole">
                    ...
                </planet>
            </star>
        </galaxy>


Planet Attributes List
isKnown - if true, planet has to be researched in warp controller first
hasRings - if true, planet has rings
GasGiant - if true, the planet cant be landed on
gas -  specifices which gases can be mined from the planet, not limited to AR gases
genType - EXPERIMENTAL 0 = overworld, 1 = nether, 2 = asteroid
fogColor - 3 floats or 1 hex code determining fog color
skyColor - same as fogColor
atmosphereDensity - atmospheric pressure on planet, default is 100
hasOxygen - overrides atmosphere oxygen presence
gravitationalMultiplier - planet gravity, 400-0, 110+ prevents jumping on full blocks so recommended is 110-10
orbitalDistance - distance from star
orbitalTheta - specifies starting angular displacement, e.g. setting 2 planets 1 with 0 and one with 180 makes them orbit at the opposite sides of the star
orbitalPhi - clockwise displacement of sun's rising and settign direction
OreGen - UNDOCUMENTED
rotationalPeriod - length of day night cycle in ticks (1s = 20t)
fillerBlock - replaces stone(?) and grass (filler blocks)
oceanBlock - ocean block
seaLevel - sea Y level
spawnable - specifies what can spawn on the planet e.g. <spawnable weight="1" groupMin="1" groupMax="5">minecraft:villager</spawnable>
biomeIds - biomes that can generate on the planet, IDs only
artifact - items needed to travel to the planet via warp controller e.g. <artifact>minecraft:coal 1</artifact>
generateCraters - generates the specified structure
generateCaves - generates the specified structure
generateVolcanos - generates the specified structure
generateStructures - generates the specified structure
generateGeodes - generates the specified structure
avgTemperature - Average temperature
retrograde - ???


Planet Specifications List
DIMID - dimension id (usually higher than 3 for mostly vanilla gameplays, but higher is safer)
dimMapping - add this with an empty string if the dimension already exists and you just wanna turn it into a planet
customIcon - string containing either a default icon or a custom icon
*/