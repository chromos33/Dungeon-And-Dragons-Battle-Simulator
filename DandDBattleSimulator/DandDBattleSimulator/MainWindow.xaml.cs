using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DandDBattleSimulator.Classes;
using System.IO;
using Newtonsoft.Json;

namespace DandDBattleSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Weapon> Weapons;
        public MainWindow()
        {
            InitializeComponent();
            Weapons = new List<Weapon>();
            LoadWeapons();
            Weapon_WeaponsList.ItemsSource = Weapons;
            Character_Weapon_ComboBox.ItemsSource = Weapons;

            /*
            Random randomizer = new Random();
            BattleField Battlefield = new BattleField(randomizer, 4);
            Character ally = new Character();
            ally.Name = "ally";
            Character enemy = new Character();
            enemy.Name = "enemy";

            Battlefield.addCharacter(ally, 3, 2);
            Battlefield.addCharacter(enemy, 1, 3);
            string msg = "";
            foreach (Character chara in Battlefield.getCharacters())
            {
                msg += chara.Name + " x: " + chara.getPoint().X + " y:" + chara.getPoint().Y + System.Environment.NewLine;
            }
            List<Classes.Point> resultmove = ally.MoveTo(enemy.getPoint());
            */
        }

        private void Weapon_WeaponsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Weapon_WeaponsList.SelectedIndex != -1)
            {
                Weapon SelectedItem = (Weapon)Weapon_WeaponsList.SelectedItem;
                Weapon_Name_Entry.Text = SelectedItem.name;
                Weapon_Dice_Entry.Text = SelectedItem.Dice.ToString();
                Weapon_TwoHandedCheckBox.IsChecked = SelectedItem.twohanded;
                Weapon_RangedCheckBox.IsChecked = SelectedItem.range;
                Weapon_RangedWeaponRange_Entry.Text = SelectedItem.attackrange.ToString();
                Weapon_BonusDmg_Entry.Text = SelectedItem.bonusdamage.ToString();
            }
            else
            {
            }
            
        }

        private void Weapon_Save_Click(object sender, RoutedEventArgs e)
        {
            string name = Weapon_Name_Entry.Text;
            if(name == "")
            {
                MessageBox.Show("You must enter a name for the weapon");
                return;
            }
            int Dice = 0;
            if(!Int32.TryParse(Weapon_Dice_Entry.Text,out Dice))
            {
                MessageBox.Show("You must enter a dice for the weapon");
                return;
            }
            bool twohanded = false;
            if(Weapon_TwoHandedCheckBox.IsChecked != null)
            {
                twohanded = (bool)Weapon_TwoHandedCheckBox.IsChecked;
            }

            bool ranged = false;
            if (Weapon_RangedCheckBox.IsChecked != null)
            {
                ranged = (bool)Weapon_RangedCheckBox.IsChecked;
            }
            int rangedrange = 0;
            if(!Int32.TryParse(Weapon_RangedWeaponRange_Entry.Text,out rangedrange))
            {
                MessageBox.Show("You must enter a Range for the Rangeweapon for the weapon");
                return;
            }
            int bonusdamage = 0;
            if(!Int32.TryParse(Weapon_BonusDmg_Entry.Text,out bonusdamage))
            {
                MessageBox.Show("You must enter a number bonus damage for the weapon");
                return;
            }
            IEnumerable<Weapon> WeaponQuery = Weapons.Where(x => x.name == name);
            Weapon writeWeapon = null;
            if(WeaponQuery.Count() > 0)
            {
                writeWeapon = WeaponQuery.First();
                writeWeapon.bonusdamage = bonusdamage;
                writeWeapon.Dice = Dice;
                writeWeapon.attackrange = rangedrange;
                writeWeapon.range = ranged;
                writeWeapon.twohanded = twohanded;
            }
            else
            {
                writeWeapon = new Weapon(name, Dice, bonusdamage, twohanded,ranged, rangedrange);
            }
            if (Weapon_WeaponsList.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("You have selected an item do you want to override(yes),create new (no),or cancel","Attention",MessageBoxButton.YesNoCancel);
                if(result == MessageBoxResult.Yes)
                {

                }
                if (result == MessageBoxResult.No)
                {
                    Weapon_WeaponsList.SelectedIndex = -1;
                }
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }


            }
            if (WeaponQuery.Count() == 0)
            {
                Weapons.Add(writeWeapon);
            }
            string jsonwrite = JsonConvert.SerializeObject(writeWeapon);
            WriteJsonStringToFile(jsonwrite, "weapon", name);
        }
        private void LoadWeapons()
        {
            if(!Directory.Exists(Directory.GetCurrentDirectory()+"/weapon"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/weapon");
            }
            else
            {
                foreach(string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/weapon","*.weapon"))
                {
                    string weaponjsonstring = File.ReadAllText(file);
                    Weapon insertweapon = JsonConvert.DeserializeObject<Weapon>(weaponjsonstring);
                    Weapons.Add(insertweapon);
                }
            }
        }
        private void WriteJsonStringToFile(string json,string type,string name)
        {
            type = type.ToLower();
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/"+type))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + type);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory()+"/"+type+"/"+name+"."+type))
            {
                file.WriteLine(json);
            }
        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            Weapon_WeaponsList.SelectedIndex = -1;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
