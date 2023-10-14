using System.Xml.Linq;
using static Modules.MessageSend;
using static Modules.NewCBody;
using static Modules.DefaultSol;
using static Modules.PlanetDef;

namespace ARplanetDefBuilder
{
    class Program
    {
        static void Main()
        {
            Galaxy galaxy = new Galaxy();
            galaxy.AddSystem(Default());

            string? input;
            while(true) {
                SendMessages(new string[] {
                    "Solar System has been automatically generated.",
                    "star   => Create a new star system.",
                    "export => Export the file"
                });
                input = Console.ReadLine();
                if(input == "star") galaxy.AddSystem(NewStar());
                else if(input == "export") {
                    SendMessages("Are you sure? (y/n)");
                    while(true) {
                        input = Console.ReadLine();
                        if(input == "y") {galaxy.Export();return;}
                        else if(input == "n") break;
                    }
                }
            }
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



/*
#####################################((((((((((((###################((((##((((((
#######################################(((((((((##########################((((((
########################################(((((((#############################(###
#########################################(((((##################################
########################################(((((###################################
############################/, /(#########(((###################################
###############((////***,,,,,*.    ,(#######(###################################
#############.*################((/ ,  .(##((####################################
############( ###################/ /(,    ./####################################
############./###################(.  /###*                   *##################
######%####/ ##%################*  *####(. . (#*. .*##(((#######################
#######%###.(############%####, /(./###( (### /######,  *(######################
#####%%%##/ ##%%##%%%##%#%###/ ##(./####.(###(.##########/ *####################
##########.*##%##%%%%%%%%%####( (#./####,.###( ############# *##################
,,         ., /(#########%#%%### (./####( #### ##############(.,################
(((((((((((((///*,,,.       ., /( .,##### /###,(################.,##############
((((((((((((((((((((((((((((((((//,,.           *((###############.,############
((((((((((((((((((((((((((((((((((((((((((((((((((//*,.        ., /(. *#########
((((((((((((((((((((((((((((##(((((((((((((((#(((((((((((((((((((///*,,.        
(((((((((((((((((((((((((((((#((((((((((((((((((((((((((((((((((((((((((((((((((
#######((((((((((((((((((((((#((((((((((((((((((((/(((((w(((((((((((((((((((((((





*/