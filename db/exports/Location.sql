DROP TABLE IF EXISTS "Location";
CREATE TABLE "Location" ("idLocation" integer,"name" text,"json" text, PRIMARY KEY ("idLocation"));

INSERT INTO "Location" ("idLocation", "name", "json") VALUES
(1, 'dummyLocation', '{"Id":1,"Title":"Dummy Location","Description":"This is a place where things can happen.","LocationType":"mine","Verbs":["Explorer","Méditer"],"RandomEvents":["dummyLocationEvent"]}');