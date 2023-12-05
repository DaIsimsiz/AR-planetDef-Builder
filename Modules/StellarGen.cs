using static Modules.Basics;
using static Modules.References;

using System.Xml.Linq;


namespace Modules
{
    //Aw hell nah, I'm never using Chat-GPT to generate names again 💀
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
            //string? input;

            List<string> starV = new List<string>();
            string[] starRequired = {"name", "temp", "size"};
            //X and Y coordinates, along with these random planets will be handled separately

            XElement star = new XElement("star");
            InputAttributeValues(ref starRequired, ref starV, ref StarAttributes, ref star, true);

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

        /// <summary>
        /// Prompts the user to enter a value for the specified list of properties.
        /// </summary>
        static void InputPropertyValues(ref string[] required, ref List<string> valuesL, ref Dictionary<string, string> Dictionary, ref XElement body) {
            foreach(string propertyName in required) {
                SendMessages(true, $"Enter a value for \"{propertyName}\".\nDescription: {Dictionary[propertyName]}");
                valuesL.Add(Console.ReadLine());
            }
            InputValidation(ref valuesL, ref required);
            for(int i = 0; i < valuesL.Count; i++) {
                body.Add(new XElement(required[i], valuesL[i]));
            }
        }

        /// <summary>
        /// Prompts the user to enter a value for the specified list of attributes.
        /// </summary>
        static void InputAttributeValues(ref string[] required, ref List<string> valuesL, ref Dictionary<string, string> Dictionary, ref XElement body, bool isStar = false) {
            string input = String.Empty;
            foreach(string attributeName in required) {
                SendMessages(true, $"Enter a value for \"{attributeName}\".\nDescription: {Dictionary[attributeName]}");
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

        /// <summary>
        /// Checks if the given input for a property/attribute is valid. Otherwise returns a valid placeholder input.
        /// </summary>
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