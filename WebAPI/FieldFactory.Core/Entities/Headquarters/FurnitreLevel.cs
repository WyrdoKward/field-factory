using FieldFactory.Core.Enum;
using FieldFactory.Core.Ressources;
using System.Collections.Generic;

namespace FieldFactory.Core.Entities.Headquarters
{
    public class FurnitreLevel
    {
        public int Level;
        public string Description;

        public List<SpendableRessource> Cost;

        // public ??? Switch

        public Dictionary<ERessourceType, int> GetCost()
        {
            var res = new Dictionary<ERessourceType, int>();
            foreach (var r in Cost)
            {
                res.Add(r.Type, r.Amount);
            }
            return res;
        }
    }
}
