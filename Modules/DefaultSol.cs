using System.Xml.Linq;

namespace Modules
{
    class DefaultSol
    {
        /// <summary>
        /// Generates the Sol, Earth, and Luna, referencing from my automatically generated planetDefs.xml file.
        /// </summary>
        public static XElement Default()

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
    }
}