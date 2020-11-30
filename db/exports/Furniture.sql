DROP TABLE IF EXISTS "Furniture";
CREATE TABLE "Furniture" ("idFurniture" text,"json" text, PRIMARY KEY ("idFurniture"));

INSERT INTO "Furniture" ("idFurniture", "json") VALUES
('dummyFurniture', '{
    "Id": 1,
    "Title": "Dummy Furniture",
    "Description": "This is a furniture. It''s built in the headquarters and provide bonuses or new actions.",
    "LevelMax": "3",
    "Levels": [
        {
		"level":"1",
		"description" : "The level 1 offers a bonus",
		"cost":
			{
			"gold":"200",
			"otherRessource":"10"
			}
		},
		{
		"level":"2",
		"description" : "Overrides previous desc if present",
		"cost":
			{
			"gold":"500",
			"otherRessource":"15",
			"thirdRessource":"2"
			}
		},	
		{
		"level":"3",
		"description" : "The third level enables dummySwitch",
		"cost":
			{
			"gold":"600",
			"otherRessource":"20",
			"thirdRessource":"5"
			},
		"switch":"dummySwitch"
		}
    ]
}
'),
('watchtower', '{
    "Id": 2,
    "Title": "Watch tower",
    "Description": "This is a room. It provides the ability to see the map further from the simple city outskirts.",
    "LevelMax": "3",
    "Levels": [
        {
		"level":"1",
		"description" : "Grants a range of +2 hexes",
		"cost":
			{
			"gold":"200",
			"otherRessource":"10"
			}
		},
		{
		"level":"2",
		"description" : "Grants a range of +3 hexes",
		"cost":
			{
			"gold":"500",
			"otherRessource":"15",
			"thirdRessource":"2"
			}
		},	
		{
		"level":"3",
		"description" : "Grants a range of +4 hexes",
		"cost":
			{
			"gold":"600",
			"otherRessource":"20",
			"thirdRessource":"5"
			}
		}
    ]
}
');