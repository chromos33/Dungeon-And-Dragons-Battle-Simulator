namespace D_and_D_3_5_BattleSimulator_UWP.DataClasses
{
    public class Weapons
    {
        public string name;
        public int Die;
        public int Dice;
        public string Type;
        public bool Twohanded;
        public bool Range;
        public int Attackrange;
        public int Bonusdamage;
        public int UseChance;
        public int ID;

        public Weapons(string _name, int _dice,int _die,int _useChance,int id, string _Type, int _bonusdamage = 0, int _attackrange = 0)
        {
            name = _name;
            Dice = _dice;
            Die = _die;
            Type = _Type;
            Attackrange = _attackrange;
            Bonusdamage = _bonusdamage;
            UseChance = _useChance;
            ID = id;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
