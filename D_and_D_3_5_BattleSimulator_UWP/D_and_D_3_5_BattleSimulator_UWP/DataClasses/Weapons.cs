namespace D_and_D_3_5_BattleSimulator_UWP.DataClasses
{
    public class Weapons
    {
        public string name;
        public int Die;
        public int Dice;
        public bool Twohanded;
        public bool Range;
        public int Attackrange;
        public int Bonusdamage;
        public int UseChance;

        public Weapons(string _name, int _dice,int _die,int _useChance, int _bonusdamage = 0, bool _twohanded = false, bool _range = false, int _attackrange = 0)
        {
            name = _name;
            Dice = _dice;
            Die = _die;
            Twohanded = _twohanded;
            Range = _range;
            Attackrange = _attackrange;
            Bonusdamage = _bonusdamage;
            UseChance = _useChance;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
