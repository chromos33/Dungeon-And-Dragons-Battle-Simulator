using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_and_D_3_5_BattleSimulator_UWP.DataClasses
{
    public class Armors
    {
        public int iArmorClass;
        public int imovementrestriction;
        public string sname;
        public int ID;

        public int iAC
        {
            get { return iArmorClass; }
            set { iArmorClass = value; }
        }
        public int iMovementRestriction
        {
            get { return imovementrestriction; }
            set { imovementrestriction = value; }
        }
        public string sName
        {
            get { return sname; }
            set { sname = value; }
        }

        public Armors(int _iArmorClass, int _iMovementRestriction, string _sName)
        {
            iArmorClass = _iArmorClass;
            imovementrestriction = _iMovementRestriction;
            sname = _sName;
        }
        public Armors()
        {
        }

        public int MovementRestriction()
        {
            int iMR = imovementrestriction;
            // use here to add Functionality for MovementRestriction
            return iMR;
        }
        public override string ToString()
        {
            return sName;
        }
    }
}
