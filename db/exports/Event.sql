DROP TABLE IF EXISTS "Event";
CREATE TABLE "Event" ("idEvent" text,"json" text, PRIMARY KEY ("idEvent"));

INSERT INTO "Event" ("idEvent", "json") VALUES
('dummyLocationEvent', '{
  "id": 0,
  "title": "Dummy quest",
  "steps": [
  {
    "id": 0,
    "text": "This is the first text of the quest. What do you do ?",
    "choices": [
      {
        "id": 1,
        "text": "Step 0 - Choice 1 (15 gold & fin OU ca continue)",
        "nextStepId" : 1
       },
      {
        "id": 2,
        "text": "Step 0 - Choice 2 (GOTO step 2)",
        "nextStepId" : 2
       }
    ]
  },
  {
    "id": 1,
    "text": "This is the step 1 text of the quest. Here is the outcome, chosen server-side.",
    "outcomes": [
      {
        "id": 1,
        "text": "Outcome 1 : +15 golds"
       },
      {
        "id": 2,
        "text": "Outcome 2 : The quest is not over, there is more !",
        "nextStepId" : 2
       }
    ]
  },{
    "id": 2,
    "text": "This is the step 2 text of the quest.",
    "choices": [
      {
        "id": 1,
        "text": "Step 2 - Choice 1",
        "nextStepId" : 3
       },
      {
        "id": 2,
        "text": "Step 2 - Choice 2",
        "nextStepId" : 4
       }
    ]
  },{
  
    "id": 3,
    "text": "This is the step 3 text of the quest. Here is the outcome, chosen server-side.",
    "outcomes": [
      {
        "id": 1,
        "text": "Outcome 1 : your companion is wounded"
       },
      {
        "id": 2,
        "text": "Outcome 2 : you gain a new companion !"
       }
    ]
  },{
  
    "id": 4,
    "text": "This is the step 4 text of the quest. Here is the outcome, chosen server-side.",
    "outcomes": [
      {
        "id": 1,
        "text": "Outcome 1 : you hear about some secret place"
       },
      {
        "id": 2,
        "text": "Outcome 2 : you find an interesting book to study once back home"
       }
    ]
  }
  ]
}
');