DROP TABLE IF EXISTS "Follower";
CREATE TABLE "Follower" ("idFollower" text,"json" text);

INSERT INTO "Follower" ("idFollower", "json") VALUES
('Gustav', '{
  "id": "Gustav",
  "description": "He will follow you until the end of time."
}');