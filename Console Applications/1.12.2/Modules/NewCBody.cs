using System.Xml.Linq;
using static Modules.MessageSend;

namespace Modules
{
    static class NewCBody
    {

        static Dictionary<string, string> PlanetAttributes = new Dictionary<string, string>
            {
                { "name", "Name of the planet" },
                { "DIMID", "Dimension ID for the planet, make sure to not use an ID in use!"},
                { "dimMapping", "Enter an empty string if the dimension you have specifies already exists." },
                { "customIcon", "You can choose a custom icon if you want to specify which one should be used."}
            };
        static Dictionary<string, string> TerrestrialPlanetSpecifications = new Dictionary<string, string>
            {
                { "isKnown", "If true, the planet will have to be discovered from the Warp Controller." },
                { "hasRings", "If true, the planet will have rings." },
                { "genType", "Experimental specification, 0 is overworld-like generation, 1 is nether-like generation and 2 is asteroids." },
                { "fogColor", "3 floating point numbers (1.0 - 0) or a hex code (0xFFFFFF) to choose the distance fog color." },
                { "skyColor", "3 floating point numbers (1.0 - 0) or a hex code (0xFFFFFF) to choose the sky color." },
                { "atmosphereDensity", "Atmospheric pressure of a planet, it affects the temperature. Default is 100 (1 atm = 100)" },
                { "hasOxygen", "Specifies if the oxygen is breathable." },
                { "gravitationalMultiplier", "G force on the planet, default is 100, but values above 110 will prevent 1 block jumps. (max 400 - min 0)" },
                { "orbitalDistance", "Planet's distance from the star." },
                { "orbitalTheta", "Starting position of the planet in its orbit in degrees." },
                { "orbitalPhi", "Clockwise displacement of star's rising and setting direction. (e.g. 90 makes stars rise from north.)" },
                { "rotationalPeriod", "Day-Night cycle of the planet in ticks. (seconds x 20 = ticks)" },
                { "fillerBlock", "Swaps dirt and stone with the specified block." },
                { "oceanBlock", "Specifies which block will be placed instead in oceans. (e.g. minecraft:lava, minecraft:air, minecraft:water)" },
                { "seaLevel", "Y level water bodies start appearing at." },
                { "spawnable", "Specifies what can spawn on the planet. (e.g. minecraft:villager, minecraft:ghast)" },
                { "biomeIds", "A list of biomes that can be found on a planet, you can specify with mod:biomeName or with biome ID. Biomes are separated by semicolons. (e.g. minecraft:jungle;30)" },
                { "artifact", "Items needed to travel to the planet with the Warp Controller. (e.g. minecraft:coal 1, minecraft:obsidian) " },
                { "generateCraters", "If true, the planet will have craters on it." },
                { "generateCaves", "If true, planet will have caves in it." },
                { "generateVolcanos", "If true, planet will have volcanoes on it." },
                { "generateStructures", "If true, all sorts of structures will spawn on the planet." },
                { "generateGeodes", "If true, geodes will spawn on the planet." },
                { "avgTemperature", "Average temperature of the planet, affects atmosphere. (Default is 100)" },
                { "retrograde", "If true, orbit direction swaps to counter-clockwise." },
                { "ringColor", "3 floating point numbers (1.0 - 0) or a hex code (0xFFFFFF) to choose the color of the rings." },
                { "forceRiverGeneration", "If true, regardless of the other conditions, rivers will spawn." },
                { "oreGen", "do not use" },
                { "laserDrillOres", "Unknown." },
                { "geodeOres", "oreDict" },
                { "craterOres", "oreDict." },
                { "craterBiomeWeights", "Undocumented." },
                { "craterFrequencyMultiplier", "Lower = Higher" },
                { "volcanoFrequencyMultiplier", "Lower = Higher" },
                { "geodefrequencyMultiplier", "Lower = Higher" },
                { "hasShading", "Undocumented." },
                { "hasColorOverride", "Undocumented." },
                { "skyRenderOverride", "Undocumented." }
            };
        static Dictionary<string, string> GaseousPlanetSpecifications = new Dictionary<string, string>
            {
                { "isKnown", "If true, the planet will have to be discovered from the Warp Controller." },
                { "GasGiant", "If true, the planet will become a Gas Giant, making landing impossible, but it will allow gas to be harvested from its atmosphere." },
                { "gas", "Specifies which gas(es) are in the Gas Giant's atmosphere." },
                { "orbitalDistance", "Planet's distance from the star." },
                { "orbitalTheta", "Starting position of the planet in its orbit in degrees." },
                { "retrograde", "If true, orbit direction swaps to counter-clockwise." },
            };
        static Dictionary<string, string> BinaryStarAttributes = new Dictionary<string, string>
            {
                { "temp", "Temperature of the star, affects it's color. (Sol's temperature is 100)" },
                { "blackHole", "If true, the star becomes a black hole." },
                { "size", "Size of the star, default is 1.0" },

                { "separation", "Distance from the center star." }
            };
        static Dictionary<string, string> StarAttributes = new Dictionary<string, string>
            {
                { "temp", "Temperature of the star, affects it's color. (Sol's temperature is 100)" },
                { "blackHole", "If true, the star becomes a black hole." },
                { "size", "Size of the star, default is 1.0" },

                { "name", "Name of the star!" },
                { "numPlanets", "Number of randomly generated terrestrial planets." },
                { "numGasGiants", "Number of randomly generated gaseous planets." },
                { "x", "X coordinate of the star in galaxy view." },
                { "y", "Y coordinate of the star in galaxy view." }
            };
        static Dictionary<string, string> SpawnableAttributes = new Dictionary<string, string>
            {
                { "weight", "Likelihood of the mob to spawn." },
                { "groupMin", "Minimum amount of mobs to spawn." },
                { "groupMax", "Maximum amount of mobs to spawn." },
                { "nbt", "NBT tags to change various stuff about the mob." }
            };

        /// <summary>
        /// Initiates the creation of a new star.
        /// </summary>
        /// <param name="binary">If true, removes some options that are incompatible with binary stars.</param>
        /// <returns></returns>
        public static XElement NewStar(bool binary = false)
        {
            string? input;

            List<string> starV = new List<string>();
            string[] starRequired = {"name", "temp", "size"};
            int numPlanets = 0;
            int numGasGiants = 0;
            //X and Y coordinates, along with these random planets will be handled separately

            XElement star = new XElement("star");
            InputAttributeValues(ref starRequired, ref starV, ref StarAttributes, ref star, true);

            if(!binary) {
                star.Add(new XAttribute("x", new Random().Next(-500, 500)));
                star.Add(new XAttribute("y", new Random().Next(-500, 500)));
            }
            else{
                SendMessages("How far should this star be from the main star?");
                star.Add(new XAttribute("separation", 10.0f));
            }

            if(binary) return star;
            else{
                while(true) {
                    SendMessages(new string[] {
                        "If you'd like to add another star, enter \"star\"",
                        "If you'd like to add a planet, or an asteroid, enter \"planet\"",
                        "If that's all, just enter \"exit\""
                    });
                    switch(Console.ReadLine()) {
                        case "star":
                            star.Add(NewStar(true));
                            break;
                        case "planet":
                            XElement planet = NewPlanet(ref numPlanets);
                            if(planet != null) star.Add(planet);
                            break;
                        case "exit":
                            star.Add(new XAttribute("numPlanets", numPlanets.ToString()));
                            star.Add(new XAttribute("numGasGiants", numGasGiants.ToString()));
                            return star;
                    }
                }
            }
        }
        /// <summary>
        /// Creates a new planet.
        /// </summary>
        static XElement NewPlanet(ref int numPlanets, bool moon = false, bool gaseous = false) {
            #pragma warning disable
            string? input;
            XElement planet = new XElement("planet");

            SendMessages("Would you like the planet to be randomly generated?\n y/n");
            if(Console.ReadLine() == "y") {numPlanets++;return null;}

            List<string> AttributesV = new List<string>();
            List<string> TerrestrialV = new List<string>();
            List<string> GaseousV = new List<string>();
            string[] requiredAttributes = {"name", "DIMID"};
            string[] requiredTerrestrial = {"isKnown", "fogColor", "skyColor", "gravitationalMultiplier", "orbitalDistance", "orbitalTheta", "orbitalPhi", "retrograde", "avgTemperature", "rotationalPeriod", "atmosphereDensity", "generateCraters", "generateCaves", "generateVolcanos", "generateStructures", "generateGeodes", "biomeIds"};
            string[] requiredGaseous = {"isKnown", "GasGiant", "gas", "fogColor", "skyColor", "gravitationalMultiplier", "orbitalDistance", "orbitalTheta", "orbitalPhi", "retrograde", "avgTemperature", "rotationalPeriod", "atmosphereDensity", "generateCraters", "generateCaves", "generateVolcanos", "generateStructures", "generateGeodes"};

            InputAttributeValues(ref requiredAttributes, ref AttributesV, ref PlanetAttributes, ref planet);

            if(gaseous) {
                InputPropertyValues(ref requiredGaseous, ref GaseousV, ref GaseousPlanetSpecifications, ref planet);
            }else{
                InputPropertyValues(ref requiredTerrestrial, ref TerrestrialV, ref TerrestrialPlanetSpecifications, ref planet);
            }

            return planet;
        }

        /// <summary>
        /// Picks a random star name when true, otherwise a planet name.
        /// </summary>
        static string PickRandomName(bool isStar = false)
        { 
            //Integrate into code
            List<string> names;
            if(isStar) names = File.ReadAllLines("starNames.txt").ToList();
            else names = File.ReadAllLines("planetNames.txt").ToList();
            return names[new Random().Next(names.Count)]; //Fix so you dont read the whole file
        }

        static void InputPropertyValues(ref string[] required, ref List<string> valuesL, ref Dictionary<string, string> Dictionary, ref XElement body) {
            foreach(string propertyName in required) {
                SendMessages($"Enter a value for \"{propertyName}\".\nDescription: {Dictionary[propertyName]}");
                valuesL.Add(Console.ReadLine());
            }
            InputValidation(ref valuesL, ref required);
            for(int i = 0; i < valuesL.Count; i++) {
                body.Add(new XElement(required[i], valuesL[i]));
            }
        }
        static void InputAttributeValues(ref string[] required, ref List<string> valuesL, ref Dictionary<string, string> Dictionary, ref XElement body, bool isStar = false) {
            string input = String.Empty;
            foreach(string attributeName in required) {
                SendMessages($"Enter a value for \"{attributeName}\".\nDescription: {Dictionary[attributeName]}");
                input = Console.ReadLine();
                if(attributeName == "name" && input == "") input = PickRandomName(); 
                valuesL.Add(input);
            }
            InputValidation(ref valuesL, ref required);
            for(int i = 0; i < valuesL.Count; i++) {
                body.Add(new XAttribute(required[i], valuesL[i]));
            }
        }
        //ref ref ref ref ref everywhere
        static void InputValidation(ref List<string> values, ref string[] names) {
            foreach(string value in values) {
                switch(value) {
                    case "a":
                        //
                        break;
                    case "b":
                        //
                        break;
                    default:
                        //
                        break;
                } //oooh my god
            }
        }
    }
} 

/*
Planet Attributes List
spawnable - specifies what can spawn on the planet e.g. <spawnable weight="1" groupMin="1" groupMax="5">minecraft:villager</spawnable>
*/