using static Modules.Basics;

using System.Xml.Linq;


namespace Modules
{
    /// <summary>
    /// A class with methods for creating new celestial bodies (planets, stars, moons, etc.)
    /// </summary>
    static class StellarGen
    {
        /// <summary>
        /// Initiates the creation of a new star.
        /// </summary>
        /// <param name="binary">If true, removes some options that are incompatible with binary stars.</param>
        /// <returns></returns>
        public static XElement NewStar(bool binary = false)
        {
            List<string> starV = new List<string>();
            string[] starRequired = {"name", "temp", "size"};
            //X and Y coordinates, along with the random planets will be handled separately

            XElement star = new XElement("star");
            InputAttributeValues(ref starRequired, ref starV, ref References.StarAttributes, ref star, true);

            if(!binary) {
                star.Add(new XAttribute("x", new Random().Next(-500, 500)));
                star.Add(new XAttribute("y", new Random().Next(-500, 500)));
                star.Add(new XAttribute("numPlanets", 0));
                star.Add(new XAttribute("numGasGiants", 0));
            }
            else{
                SendMessages(true, "How far should this star be from the main star?");
                star.Add(new XAttribute("separation", 10.0f));
            }

            return star;
        }

        static int DIMID = 3;
        /// <summary>
        /// Creates a new planet.
        /// </summary>
        public static XElement NewPlanet() {
            string? input;
            bool gaseous = false;
            XElement planet = new XElement("planet");

            while(true) {
                SendMessages(true, "Is the planet gaseous?\n y/n");
                input = Console.ReadLine().ToLower();
                if(input == "y") {gaseous = true;break;}
                else if(input == "n") {gaseous = false;break;}
            }

            List<string> AttributesV = new();
            List<string> TerrestrialV = new();
            List<string> GaseousV = new();
            string[] requiredAttributes = {"name"};
            string[] requiredTerrestrial = {"isKnown", "fogColor", "skyColor", "gravitationalMultiplier", "orbitalDistance", "orbitalTheta", "orbitalPhi", "retrograde", "avgTemperature", "rotationalPeriod", "atmosphereDensity", "generateCraters", "generateCaves", "generateVolcanos", "generateStructures", "generateGeodes", "biomeIds"};
            string[] requiredGaseous = {"isKnown", "GasGiant", "gas", "fogColor", "skyColor", "gravitationalMultiplier", "orbitalDistance", "orbitalTheta", "orbitalPhi", "retrograde", "avgTemperature", "rotationalPeriod", "atmosphereDensity", "generateCraters", "generateCaves", "generateVolcanos", "generateStructures", "generateGeodes"};

            InputAttributeValues(ref requiredAttributes, ref AttributesV, ref References.PlanetAttributes, ref planet);
            AttributesV.Add(DIMID.ToString());DIMID++;

            if(gaseous) {
                InputPropertyValues(ref requiredGaseous, ref GaseousV, ref References.GaseousPlanetSpecifications, ref planet);
            }else{
                InputPropertyValues(ref requiredTerrestrial, ref TerrestrialV, ref References.TerrestrialPlanetSpecifications, ref planet);
            }

            return planet;
        }

        /// <summary>
        /// Prompts the user to enter a value for the specified list of properties.
        /// </summary>
        static void InputPropertyValues(ref string[] required, ref List<string> valuesL, ref Dictionary<string, string> Dictionary, ref XElement body) {
            foreach(string propertyName in required) {
                SendMessages(true, $"Enter a value for \"{propertyName}\".\nDescription: {Dictionary[propertyName]}");
                valuesL.Add(Console.ReadLine());
            }
            for(int i = 0; i < valuesL.Count; i++) {
                body.Add(new XElement(required[i], valuesL[i]));
            }
        }

        /// <summary>
        /// Prompts the user to enter a value for the specified list of attributes.
        /// </summary>
        static void InputAttributeValues(ref string[] required, ref List<string> valuesL, ref Dictionary<string, string> Dictionary, ref XElement body, bool isStar = false) {
            string? input;
            foreach(string attributeName in required) {
                SendMessages(true, $"Enter a value for \"{attributeName}\".\nDescription: {Dictionary[attributeName]}");
                input = Console.ReadLine();
                valuesL.Add(input);
            }
            for(int i = 0; i < valuesL.Count; i++) {
                body.Add(new XAttribute(required[i], valuesL[i]));
            }
        }

        static public bool IsValid(string e, string? value) {
            string type;

            if(References.PlanetAttributesValid.ContainsKey(e)) {type = References.PlanetAttributesValid[e];}
            else if(References.TerrestrialPlanetSpecificationsValid.ContainsKey(e)) {type = References.TerrestrialPlanetSpecificationsValid[e];}
            else if(References.GaseousPlanetSpecificationsValid.ContainsKey(e)) {type = References.GaseousPlanetSpecificationsValid[e];}
            else if(References.BinaryStarAttributesValid.ContainsKey(e)) {type = References.BinaryStarAttributesValid[e];}
            else if(References.StarAttributesValid.ContainsKey(e)) {type = References.StarAttributesValid[e];}
            else if(References.SpawnableAttributesValid.ContainsKey(e)) {type = References.SpawnableAttributesValid[e];}
            else{throw new NullReferenceException();}

            switch(type) {
                case "int":
                    return int.TryParse(value, out _);
                case "empty":
                    return (value == String.Empty || value == "");
                case "bool":
                    return bool.TryParse(value, out _);
                case "float":
                    return float.TryParse(value, out _);
                case "special":
                    switch(e) {
                        case "fogColor":
                            foreach(string f in value.Split(',')) {
                                if(!float.TryParse(f, out _)) return false;
                            }
                            return true;
                        case "skyColor":
                            foreach(string f in value.Split(',')) {
                                if(!float.TryParse(f, out _)) return false;
                            }
                            return true;
                        case "ringColor":
                            foreach(string f in value.Split(',')) {
                                if(!float.TryParse(f, out _)) return false;
                            }
                            return true;
                        default:
                            return true;
                    }
                default:
                    return true;
            }
        }//Public method to check value validation
    }
} 

/*
Planet Attributes List
spawnable - specifies what can spawn on the planet e.g. <spawnable weight="1" groupMin="1" groupMax="5">minecraft:villager</spawnable>
*/