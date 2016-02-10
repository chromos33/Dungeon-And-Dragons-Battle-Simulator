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

        }

        private void Weapon_Save_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadWeapons()
        {
            if(!Directory.Exists(Directory.GetCurrentDirectory()+"/Weapons"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Weapons");
            }
            else
            {
                foreach(string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/Weapons","*.weapon"))
                {
                    string weaponjsonstring = File.ReadAllText(file);
                    Weapon insertweapon = JsonConvert.DeserializeObject<Weapon>(weaponjsonstring);
                    Weapons.Add(insertweapon);
                }
            }
        }
    }
}
