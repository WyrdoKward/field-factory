DROP TABLE IF EXISTS "Player";
CREATE TABLE "Player" (
  "idPlayer" text NOT NULL UNIQUE,
	"email" TEXT NOT NULL UNIQUE,
	"hashpwd" TEXT NOT NULL,
  "token" TEXT NULL);

INSERT INTO Player (idPlayer, email, hashpwd) VALUES("wyrdokward", "wyrdokward@gmail.com", "123456");
INSERT INTO Player (idPlayer, email, hashpwd) VALUES("nono", "no@no.com", "pwd");
