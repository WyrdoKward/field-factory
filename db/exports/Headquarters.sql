DROP TABLE IF EXISTS "Headquarters";
CREATE TABLE "Headquarters" ("idHeadquarters" text,"json" text, PRIMARY KEY ("idHeadquarters"));

INSERT INTO "Headquarters" ("idHeadquarters", "json") VALUES
('dummyHeadquarters', '{
    "Id": 1,
    "Title": "Dummy Headquarters",
    "Description": "This is a basic HQ. Furnitures[] correspond aux colonnes actives dans PlayerHeadquarters",
	"Furnitures":[
		"dummyFurniture",
		"watchtower"
	]
    
}
');