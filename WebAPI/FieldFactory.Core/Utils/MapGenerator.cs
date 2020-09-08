using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Utils
{
    public static class MapGenerator
    {
        public static ELandType[,] Template1 = new ELandType[,]
        {
            { ELandType.forest, ELandType.hills, ELandType.grass, ELandType.grass, ELandType.forest  },
            { ELandType.forest, ELandType.hills, ELandType.hills, ELandType.grass, ELandType.forest  },
            { ELandType.ocean, ELandType.ocean, ELandType.hills, ELandType.forest, ELandType.forest  },
            { ELandType.ocean, ELandType.ocean, ELandType.grass, ELandType.forest, ELandType.ocean  },
            { ELandType.ocean, ELandType.ocean, ELandType.ocean, ELandType.ocean, ELandType.ocean  },
        };

        public static Location?[][] TemplateLocations1 = new Location?[][]
        {
            new Location?[]{null, null, new Location() { Id = 1, LocationType = ELocationType.townCenter, Verbs = new EVerb[] { EVerb.Parler, EVerb.Piller, EVerb.Construire}, Title = "Otrock", Description = "This is my hometown." }, null, null},
            new Location?[]{null, null, null, null, null},
            new Location?[]{null, null, null, new Location() { Id = 1, LocationType = ELocationType.mine, Verbs = new EVerb[] { EVerb.Explorer, EVerb.Méditer}, Title = "Peaceful cave", Description = "The entrance is collapsed, but it can provide a decent shelter for a weary traveller." }, null},
            new Location?[]{null, null, new Location() { Id = 1, LocationType = ELocationType.mine, Verbs = new EVerb[] { EVerb.Explorer, EVerb.Piller}, Title = "Mine", Description = "Maybe there could be some stuff in here." }, null, null},
            new Location?[]{null, null, null, null, null},
        };

        public static List<Tile> GenerateMap(ELandType[,] Template)
        {
            List<Tile> res = new List<Tile>();
            int currentId = 0;
            int mapWidth = Template.GetLength(0);
            int mapHeight = Template.GetLength(1);

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    res.Add(new Tile() { Id = currentId, LandType = Template[i,j], X = i, Y = j, Location = TemplateLocations1[i][j] });
                    
                    currentId++;
                }
            }

            return res;
        }
    }
}
