using System.Collections;
using System.Collections.Generic;

namespace Assets.Codes.DAOs
{
    public struct Card
    {
        public System.Int32 Id;
        public string Name;
        public string Effect;
        public string Oracle;
        public string Type;
        public string Power;
        public string Toughness;
        public Hashtable Cost;
        public IList<string[]> Editions;        

        public Card(System.Int32 Id, string Name, string Effect, string Type, string Power, string Toughness, string Oracle, Hashtable Cost)
        {
            this.Id = Id;
            this.Name = Name;
            this.Effect = Effect;
            this.Oracle = Oracle;
            this.Type = Type;
            this.Power = Power;
            this.Toughness = Toughness;
            this.Cost = Cost;
            Editions = new List<string[]>();
        }
    }
}
