using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DandDBattleSimulator.Classes
{
    static class StaticJSONCloneHelper
    {
        public static Weapon Clone(Weapon clonesource)
        {
            string jsonwrite = JsonConvert.SerializeObject(clonesource);
            Weapon clone = JsonConvert.DeserializeObject<Weapon>(jsonwrite);
            return clone;
        }
        public static Armor Clone(Armor clonesource)
        {
            string jsonwrite = JsonConvert.SerializeObject(clonesource);
            Armor clone = JsonConvert.DeserializeObject<Armor>(jsonwrite);
            return clone;
        }
        public static Character Clone(Character clonesource)
        {
            string jsonwrite = JsonConvert.SerializeObject(clonesource);
            Character clone = JsonConvert.DeserializeObject<Character>(jsonwrite);
            return clone;
        }
    }
}
