using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DandDBattleSimulator.Classes
{
    class Weapon
    {
        public string name;
        public int Dice;
        public bool twohanded;
        public bool range;
        public int attackrange;

        public Weapon(string _name,int _dice,bool _twohanded = false,bool _range = false,int _attackrange = 0)
        {
            name = _name;
            Dice = _dice;
            twohanded = _twohanded;
            range = _range;
            attackrange = _attackrange;
        }
    }
}
