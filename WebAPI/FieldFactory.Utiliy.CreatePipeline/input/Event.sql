DROP TABLE IF EXISTS "Event";
CREATE TABLE "Event" ("idEvent" text,"json" text, PRIMARY KEY ("idEvent"));

INSERT INTO "Event" ("idEvent", "json") VALUES
('dummyLocationEvent', '{
    "id":0,
    "title":"Dummy quest",
    "steps":[
       {
          "id":0,
          "text":"This is the first text of the quest. What do you do ?",
          "choices":[
             {
                "id":1,
                "text":"Step 0 - Choice 1 (15 gold & fin OU ca continue)",
                "outcomes":[
                   {
                      "nextStepId":1,
                      "weight":1
                   },
                   {
                      "nextStepId":2,
                      "weight":1
                   }
                ]
             },
             {
                "id":2,
                "text":"Step 0 - Choice 2 (GOTO step 2)",
                "outcomes":[
                   {
                      "nextStepId":2,
                      "weight":1
                   }
                ]
             }
          ]
       },
       {
          "id":1,
          "text":"S1 - No further choices. You gain 15 golds, quest over."
       },
       {
          "id":2,
          "text":"S2 - There is an other choice to make.",
          "choices":[
             {
                "id":1,
                "text":"Step 2 - Choice 1 - Compagnon + ou -",
                "outcomes":[
                   {
                      "nextStepId":3,
                      "weight":1
                   },
                   {
                      "nextStepId":4,
                      "weight":1
                   }
                ]
             },
             {
                "id":2,
                "text":"Step 2 - Choice 2 - Secret ou Livre",
                "outcomes":[
                   {
                      "nextStepId":5,
                      "weight":1
                   },
                   {
                      "nextStepId":6,
                      "weight":1
                   }
                ]
             }
          ]
       },
       {
          "id":3,
          "text":"S3 - Companion is wounded ! Quest Over."
       },
       {
          "id":4,
          "text":"S4 - New compaignon ! Quest Over."
       },
       {
          "id":5,
          "text":"S5 - You hear about some secret place ! Quest Over."
       },
       {
          "id":6,
          "text":"S6 - You find an interesting book to study once back home ! Quest Over."
       }
    ]
 }
'),
('eglingenEvent','{
    "id":1,
    "title":"Arrivée à Eglingen",
    "steps":[
       {
          "id":0,
          "text":"Vous arrivez enfin devant la maison, après un long voyage sous la pluie. Vous vous apprêtez à rentrer mais un chat blanc menançant vous barre la route en rugissant.",
          "choices":[
             {
                "id":1,
                "text":"Vous tentez de l amadouer avec des croquettes",
                "outcomes":[
                   {
                      "nextStepId":1,
                      "weight":1
                   },
                   {
                      "nextStepId":2,
                      "weight":1
                   }
                ]
             },
             {
                "id":2,
                "text":"Vous l ignorez pour aller saluer directement vos parents.",
                "outcomes":[
                   {
                      "nextStepId":3,
                      "weight":1
                   }
                ]
             }
          ],
          "DurationInMin":1
       },
       {
          "id":1,
          "text":"La bête vous saute dessus avant que vous n atteignez le paquet de croquettes, en vous arrachant un bras. C est un échec."
       },
       {
          "id":2,
          "text":"Les croquettes semblent être à son gout, il ne vous prête plus attention et vous pouvez allez voir vos parents.",
          "choices":[
             {
                "id":1,
                "text":"Voir papa dans le jardin qui joue avec le ramasse-noix.",
                "outcomes":[
                   {
                      "nextStepId":4,
                      "weight":1
                   },
                   {
                      "nextStepId":5,
                      "weight":1
                   }
                ]
             },
             {
                "id":2,
                "text":"Monter dans la cuisine voir maman.",
                "outcomes":[
                   {
                      "nextStepId":6,
                      "weight":1
                   },
                   {
                      "nextStepId":7,
                      "weight":1
                   }
                ]
             }
          ],
          "DurationInMin":1
       },
       {
          "id":3,
          "text":"Papa vous fait un signe de la main depuis le fond du jardin. En parallèle vous sentez une odeur de tarte émanant de la cuisine.",
          "choices":[
             {
                "id":1,
                "text":"Voir papa dans le jardin qui joue avec le ramasse-noix.",
                "outcomes":[
                   {
                      "nextStepId":4,
                      "weight":1
                   },
                   {
                      "nextStepId":5,
                      "weight":1
                   }
                ]
             },
             {
                "id":2,
                "text":"Monter dans la cuisine voir maman.",
                "outcomes":[
                   {
                      "nextStepId":6,
                      "weight":1
                   },
                   {
                      "nextStepId":7,
                      "weight":1
                   }
                ]
             }
          ],
          "DurationInMin":1
       },
       {
          "id":4,
          "text":"Dans un élan d exitation, Papa vous blesse avec le ramasse-noix. C est un échec."
       },
       {
          "id":5,
          "text":"Papa comprend que vous trouvez que le ramasse-noix est une invention formidable. Il vous propose de ce pas de remplir 2 seaux avant d aller manger. Quête terminé, vous gagnez 37kg de noix et un potiron."
       },
       {
          "id":6,
          "text":"Maman vous acceuille avec 2 tartes et une histoire à tiroirs impliquant le kiné, la directrice d école, et Enzo l élève difficle de la classe. Vous suivez tant bien que mal mais finissez par vous faire un noeud au cerveau lors de la 16e digression. Vous vous évanouissez avec un filet de bave. La quete est un échec."
       },
       {
          "id":7,
          "text":"Maman est dans la cuisine, le repas est prêt et il n y a qu à mettre les pieds sous la table, gros fainéant que vous êtes. La quête est terminée, bon apétit !"
       }
    ]
 }');