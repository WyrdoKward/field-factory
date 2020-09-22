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
            { ELandType.forest_conifer_dense_covered, ELandType.forest_conifer_sparse_covered, ELandType.grassland_sparse_clear, ELandType.grassland_sparse_clear, ELandType.forest_deciduous_dense_covered  },
            { ELandType.forest_conifer_dense_covered, ELandType.forest_conifer_sparse_clear, ELandType.hills_sparse_covered, ELandType.grassland_sparse_covered, ELandType.forest_deciduous_sparse_covered  },
            { ELandType.ocean_waves_small, ELandType.ocean_waves_small, ELandType.grassland_dense_covered, ELandType.forest_deciduous_sparse_clear, ELandType.hills_dense_covered  },
            { ELandType.ocean_waves_big, ELandType.ocean_waves_small, ELandType.grassland_sparse_covered, ELandType.hills_sparse_covered, ELandType.ocean_waves_small  },
            { ELandType.ocean_waves_small, ELandType.ocean_waves_small, ELandType.ocean_waves_small, ELandType.ocean_waves_small, ELandType.ocean_waves_big  },
        };

        public static Location?[][] TemplateLocations1 = new Location?[][]
        {
            new Location?[]{null, null, new Location() { Id = 1, LocationType = ELocationType.townCenter, Verbs = new EVerb[] { EVerb.Parler, EVerb.Piller, EVerb.Construire}, Title = "Otrock", Description = "This is my hometown." }, null, null},
            new Location?[]{null, null, null, null, null},
            new Location?[]{null, null, null, new Location() { Id = 1, LocationType = ELocationType.mine, Verbs = new EVerb[] { EVerb.Explorer, EVerb.Méditer}, Title = "Peaceful cave", Description = "The entrance is collapsed, but it can provide a decent shelter for a weary traveller." }, null},
            new Location?[]{null, null, new Location() { Id = 1, LocationType = ELocationType.mine, Verbs = new EVerb[] { EVerb.Explorer, EVerb.Piller}, Title = "Mine", Description = "Maybe there could be some stuff in here." }, null, null},
            new Location?[]{null, null, null, null, null},
        };

        public static List<List<Tile>> GenerateMap(ELandType[,] Template)
        {
            List<List<Tile>> res = new List<List<Tile>>();
            int currentId = 0;
            int mapWidth = Template.GetLength(0);
            int mapHeight = Template.GetLength(1);
            Random rnd = new Random();

            for (int i = 0; i < mapWidth; i++)
            {
                List<Tile> row = new List<Tile>();

                for (int j = 0; j < mapHeight; j++)
                {
                    int variation = rnd.Next(10);
                    Tile tile = new Tile() { Id = currentId, X = i, Y = j, Location = TemplateLocations1[i][j] };
                    tile.GenerateLandType(Template1[i, j], variation);
                    row.Add(tile);

                    currentId++;
                }
                res.Add(row);
            }

            return res;
        }
    }
}
