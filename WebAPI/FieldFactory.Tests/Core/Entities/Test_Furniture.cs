using System;
using System.Collections.Generic;
using System.Text;
using FieldFactory.Core.Entities.Headquarters;
using FieldFactory.Core.Enum;
using FieldFactory.Core.Ressources;
using NUnit.Framework;
using Newtonsoft.Json;

namespace FieldFactory.Tests.Core.Entities
{
    public class Test_Furniture
    {

        [Test]
        public void Serialize_Furniture()
        {
            Furniture f = new Furniture();
            f.Id = 1;
            f.Title = "Dummy Furniture";
            f.Description = "This is a furniture. It''s built in the headquarters and provide bonuses or new actions.";
            f.Levels = new List<FurnitreLevel>();
            f.Levels.Add(new FurnitreLevel() { Level = 1, Description = "Level 1", Cost = new List<SpendableRessource>() { new SpendableRessource() { Type = ERessourceType.gold, Amount = 20 } } }) ;

            string json = JsonConvert.SerializeObject(f, new Newtonsoft.Json.Converters.StringEnumConverter());
            string expected = "{\"Id\":1,\"Title\":\"Dummy Furniture\",\"Description\":\"This is a furniture. It''s built in the headquarters and provide bonuses or new actions.\",\"Levels\":[{\"Level\":1,\"Description\":\"Level 1\",\"Cost\":[{\"Type\":\"gold\",\"Amount\":20}]}]}";

            Assert.AreEqual(expected, json);
        }
    }
}
