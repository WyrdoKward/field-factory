DROP TABLE IF EXISTS "Location";
CREATE TABLE "Location" ("idLocation" text,"json" text, PRIMARY KEY ("idLocation"));

INSERT INTO "Location" ("idLocation", "json") VALUES
('dummyLocation', '{"Id":1,"Title":"Dummy Location","Description":"This is a place where things can happen.","LocationType":"mine","Verbs":["Explorer","Méditer"],"RandomEvents":["dummyLocationEvent"]}'),
('eglingen','{"Id":2,"Title":"Eglingen","Description":"Le village de votre enfance.","LocationType":"mine","Verbs":["Explorer"],"RandomEvents":["eglingenEvent"]}');