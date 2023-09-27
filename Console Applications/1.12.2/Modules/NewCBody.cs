using System.Xml.Linq;
using static Modules.MessageSend.Temp;

namespace Modules.NewCBody
{
    class Temp
    {
        /// <summary>
        /// Initiates the creation of a new star.
        /// </summary>
        /// <param name="binary">If true, removes some options that are incompatible with binary stars.</param>
        /// <returns></returns>
        public static XElement NewStar(bool binary = false)
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

            if(!binary) {
                star.Add(new XAttribute("x", new Random().Next(-500, 500)));
                star.Add(new XAttribute("y", new Random().Next(-500, 500)));
            }
            else{
                SendMessages("How far should this star be from the main star?");
                star.Add(new XAttribute("separation", 10.0f));
            }

            if(binary) return star;
            SendMessages(new string[] {
                "If you'd like to add another star, enter \"star\"",
                "If you'd like to add a planet, or an asteroid, enter \"planet\"",
                "If that's all, just enter \"exit\""
            });
            while(true) {
                switch(Console.ReadLine()) {
                    case "star":
                        star.Add(NewStar(true));
                        break;
                    case "planet":
                        star.Add(NewPlanet());
                        break;
                    case "exit":
                        return star;
                }
            }
        }

        static XElement NewPlanet(bool moon = false) {
            string? input;
            XElement planet = new XElement("planet");


            return planet;
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
name - name
DIMID - dimension id (usually higher than 3 for mostly vanilla gameplays, but higher is safer)
dimMapping - add this with an empty string if the dimension already exists and you just wanna turn it into a planet
customIcon - string containing either a default icon or a custom icon
*/