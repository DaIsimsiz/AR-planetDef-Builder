using System.Xml.Linq;

namespace Modules
{
    /// <summary>
    /// A class filled with lots of text and lines, they have been put here to prevent polluting the other files.
    /// Their only purpose is to use the ingame keywords in the code easily.
    /// </summary>
    class References
    {
        /// <summary>
        /// Generates the Sol, Earth, and Luna, referencing from the automatically generated planetDefs.xml file.
        /// </summary>
        public static XElement Sol()

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
        /// A list of attributes a planet may have.
        /// </summary>
        static public Dictionary<string, string> PlanetAttributes = new()
        {
                { "name", "Name of the planet" },
                { "DIMID", "Dimension ID for the planet, make sure to not use an ID in use!"},
                { "dimMapping", "Enter an empty string if the dimension you have specifies already exists." },
                { "customIcon", "You can choose a custom icon if you want to specify which one should be used."}
            };
        /// <summary>
        /// A list of properties a terrestrial planet may have.
        /// </summary>
        static public Dictionary<string, string> TerrestrialPlanetSpecifications = new()
            {
                { "isKnown", "If true, the planet will have to be discovered from the Warp Controller." },
                { "hasRings", "If true, the planet will have rings." },
                { "genType", "Experimental specification, 0 is overworld-like generation, 1 is nether-like generation and 2 is asteroids." },
                { "fogColor", "3 floating point numbers (1.0 - 0) or a hex code (0xFFFFFF) to choose the distance fog color. (e.g 1.0,1.0,1.0)" },
                { "skyColor", "3 floating point numbers (1.0 - 0) or a hex code (0xFFFFFF) to choose the sky color. (e.g 1.0,1.0,1.0)" },
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
                { "ringColor", "3 floating point numbers (1.0 - 0) or a hex code (0xFFFFFF) to choose the color of the rings. (e.g 1.0,1.0,1.0)" },
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
        /// <summary>
        /// A list of properties a gaseous planet may have.
        /// </summary>
        static public Dictionary<string, string> GaseousPlanetSpecifications = new()
            {
                { "isKnown", "If true, the planet will have to be discovered from the Warp Controller." },
                { "GasGiant", "If true, the planet will become a Gas Giant, making landing impossible, but it will allow gas to be harvested from its atmosphere." },
                { "gas", "Specifies which gas(es) are in the Gas Giant's atmosphere." },
                { "orbitalDistance", "Planet's distance from the star." },
                { "orbitalTheta", "Starting position of the planet in its orbit in degrees." },
                { "retrograde", "If true, orbit direction swaps to counter-clockwise." },
            };
        /// <summary>
        /// A list of attributes a binary star may have.
        /// </summary>
        static public Dictionary<string, string> BinaryStarAttributes = new()
            {
                { "temp", "Temperature of the star, affects it's color. (Sol's temperature is 100)" },
                { "blackHole", "If true, the star becomes a black hole." },
                { "size", "Size of the star, default is 1.0" },

                { "separation", "Distance from the center star." }
            };
        /// <summary>
        /// A list of attributes a main star may have.
        /// </summary>
        static public Dictionary<string, string> StarAttributes = new()
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
        /// <summary>
        /// A list of attributes the <spawnable> property may have.
        /// </summary>
        static public Dictionary<string, string> SpawnableAttributes = new()
            {
                { "weight", "Likelihood of the mob to spawn." },
                { "groupMin", "Minimum amount of mobs to spawn." },
                { "groupMax", "Maximum amount of mobs to spawn." },
                { "nbt", "NBT tags to change various stuff about the mob." }
            };
        /// <summary>
        /// A list of attributes a planet may have.
        /// </summary>
        static public Dictionary<string, string> PlanetAttributesValid = new()
        {
                { "name", "string" },
                { "DIMID", "int"},
                { "dimMapping", "empty" },
                { "customIcon", "string"}
            };
        /// <summary>
        /// A list of properties a terrestrial planet may have.
        /// </summary>
        static public Dictionary<string, string> TerrestrialPlanetSpecificationsValid = new()
            {
                { "isKnown", "bool" },
                { "hasRings", "bool" },
                { "genType", "int" },
                { "fogColor", "special" },
                { "skyColor", "special" },
                { "atmosphereDensity", "int" },
                { "hasOxygen", "bool" },
                { "gravitationalMultiplier", "int" },
                { "orbitalDistance", "int" },
                { "orbitalTheta", "int" },
                { "orbitalPhi", "int" },
                { "rotationalPeriod", "int" },
                { "fillerBlock", "string" },
                { "oceanBlock", "string" },
                { "seaLevel", "int" },
                { "spawnable", "unknown" },
                { "biomeIds", "string" },
                { "artifact", "string" },
                { "generateCraters", "bool" },
                { "generateCaves", "bool" },
                { "generateVolcanos", "bool" },
                { "generateStructures", "bool" },
                { "generateGeodes", "bool" },
                { "avgTemperature", "int" },
                { "retrograde", "bool" },
                { "ringColor", "special" },
                { "forceRiverGeneration", "bool" },
                { "oreGen", "unknown" },
                { "laserDrillOres", "unknown" },
                { "geodeOres", "string" },
                { "craterOres", "string" },
                { "craterBiomeWeights", "unknown" },
                { "craterFrequencyMultiplier", "unknown" },
                { "volcanoFrequencyMultiplier", "unknown" },
                { "geodefrequencyMultiplier", "unknown" },
                { "hasShading", "bool" },
                { "hasColorOverride", "bool" },
                { "skyRenderOverride", "bool" }
            };
        /// <summary>
        /// A list of properties a gaseous planet may have.
        /// </summary>
        static public Dictionary<string, string> GaseousPlanetSpecificationsValid = new()
            {
                { "isKnown", "bool" },
                { "GasGiant", "bool" },
                { "gas", "string" },
                { "orbitalDistance", "int" },
                { "orbitalTheta", "int" },
                { "retrograde", "bool" },
            };
        /// <summary>
        /// A list of attributes a binary star may have.
        /// </summary>
        static public Dictionary<string, string> BinaryStarAttributesValid = new()
            {
                { "temp", "int" },
                { "blackHole", "bool" },
                { "size", "float" },

                { "separation", "int" }
            };
        /// <summary>
        /// A list of attributes a main star may have.
        /// </summary>
        static public Dictionary<string, string> StarAttributesValid = new()
            {
                { "temp", "int" },
                { "blackHole", "bool" },
                { "size", "float" },

                { "name", "string" },
                { "numPlanets", "int" },
                { "numGasGiants", "int" },
                { "x", "int" },
                { "y", "int" }
            };
        /// <summary>
        /// A list of attributes the <spawnable> property may have.
        /// </summary>
        static public Dictionary<string, string> SpawnableAttributesValid = new()
            {
                { "weight", "int" },
                { "groupMin", "int" },
                { "groupMax", "int" },
                { "nbt", "unknown" }
            };
    }
}