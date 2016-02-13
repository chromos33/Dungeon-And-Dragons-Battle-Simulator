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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace DandDBattleSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Weapon> Weapons;
        List<Armor> Armors;
        List<BattleField> Battlefields;
        List<Character> Characters;
        Random randomizer;
        List<Weapon> tempWeaponList;
        List<Armor> tempArmorList;

        public MainWindow()
        {
            InitializeComponent();
            Weapons = new List<Weapon>();
            Armors = new List<Armor>();
            Battlefields = new List<BattleField>();
            Characters = new List<Character>();
            tempWeaponList = new List<Weapon>();
            tempArmorList = new List<Armor>();
            LoadBattlefields();
            LoadWeapons();
            LoadArmors();
            LoadCharacters();
            InitComboBoxesSourceItems();
            randomizer = new Random();

            /*
            
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
        #region dataload
        private void InitComboBoxesSourceItems()
        {
            Weapon_WeaponsList.ItemsSource = Weapons;
            Character_Weapon_ComboBox.ItemsSource = Weapons;
            Armor_ArmorsList.ItemsSource = Armors;
            Character_Armor_ComboBox.ItemsSource = Armors;
            Battlefield_Configs.ItemsSource = Battlefields;
            Character_Weapons.ItemsSource = tempWeaponList;
            Character_Armors.ItemsSource = tempArmorList;
        }
        private void LoadWeapons()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/weapon"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/weapon");
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/weapon", "*.weapon"))
                {
                    string weaponjsonstring = File.ReadAllText(file);
                    try
                    {
                        Weapon insertweapon = JsonConvert.DeserializeObject<Weapon>(weaponjsonstring);
                        Weapons.Add(insertweapon);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(file + " was corrupted or is not a valid file");
                    }
                }
            }
        }
        private void LoadArmors()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/armor"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/armor");
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/armor", "*.armor"))
                {
                    string jsonstring = File.ReadAllText(file);
                    try
                    {
                        Armor insertArmor = JsonConvert.DeserializeObject<Armor>(jsonstring);
                        Armors.Add(insertArmor);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(file + " was corrupted or is not a valid file");
                    }
                
                }
            }
        }
        private void LoadBattlefields()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/battlefield"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/battlefield");
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/battlefield", "*.battlefield"))
                {
                    string jsonstring = File.ReadAllText(file);
                    try
                    {
                        BattleField insertBattlefield = JsonConvert.DeserializeObject<BattleField>(jsonstring);
                        Battlefields.Add(insertBattlefield);
                    } catch (Exception)
                    {
                        MessageBox.Show(file + " was corrupted or is not a valid file");
                    }
                }
            }
        }
        private void LoadCharacters()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/character"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/character");
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "/character", "*.character"))
                {
                    string jsonstring = File.ReadAllText(file);
                    try
                    {
                        Character insertCharacter = JsonConvert.DeserializeObject<Character>(jsonstring);
                        Characters.Add(insertCharacter);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(file + " was corrupted or is not a valid file");
                    }
                }
            }
        }
        #endregion
        #region Weapon
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

        #endregion
        #region Armor
        private void Armor_Save_Click(object sender, RoutedEventArgs e)
        {
            string sName = Armor_Name.Text;
            if (sName == "")
            {
                MessageBox.Show("You must enter a name for the weapon");
                return;
            }
            int iAC = 0;
            if (!Int32.TryParse(Armor_AC.Text, out iAC))
            {
                MessageBox.Show("You must enter a number for Armor Class");
                return;
            }
            int iMR = 0;
            if (!Int32.TryParse(Armor_MovementRestriction.Text, out iMR))
            {
                MessageBox.Show("You must enter a number for MovementRestriction");
                return;
            }
            IEnumerable<Armor> ArmorQuery = Armors.Where(x => x.sName == sName);
            Armor writeArmor = null;
            if (ArmorQuery.Count() > 0)
            {
                writeArmor = ArmorQuery.First();
                writeArmor.iAC = iAC;
                writeArmor.sName = sName;
                writeArmor.iMovementRestriction = iMR;

            }
            else
            {
                writeArmor = new Armor(iAC,iMR,sName);
            }
            if (Armor_ArmorsList.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("You have selected an item do you want to override(yes),create new (no),or cancel", "Attention", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {

                }
                if (result == MessageBoxResult.No)
                {
                    Armor_ArmorsList.SelectedIndex = -1;
                }
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }


            }
            if (ArmorQuery.Count() == 0)
            {
                Armors.Add(writeArmor);
            }
            string jsonwrite = JsonConvert.SerializeObject(writeArmor);
            WriteJsonStringToFile(jsonwrite, "armor", sName);
        }

        private void Armor_ArmorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Armor_ArmorsList.SelectedIndex != -1)
            {
                Armor SelectedItem = (Armor)Armor_ArmorsList.SelectedItem;
                Armor_Name.Text = SelectedItem.sName;
                Armor_AC.Text = SelectedItem.iAC.ToString();
                Armor_MovementRestriction.Text = SelectedItem.iMovementRestriction.ToString();
            }
            else
            {
            }
        }
        #endregion
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
        #region Battlefield
        List<DandDBattleSimulator.Classes.Point> CurrentBattlefieldbuild;
        Grid CurrentBattleFieldGrid;
        private void Battlefield_RawMapGenerate_Click(object sender, RoutedEventArgs e)
        {
            int width = 0;
            int height = 0;
            if (!Int32.TryParse(Battlefield_Height.Text, out height))
            {
                MessageBox.Show("You must state a height for the battlefield.");
                return;
            }
            if (!Int32.TryParse(Battlefield_Width.Text, out width))
            {
                MessageBox.Show("You must state a width for the battlefield.");
                return;
            }
            Battlefield_Container.Children.Clear();
            Grid BattlefieldGrid = new Grid();
            BattlefieldGrid.Name = "Battlefield_Rendered";
            BattlefieldGrid.Width = Double.NaN;
            BattlefieldGrid.Height = Double.NaN;
            BattlefieldGrid.HorizontalAlignment = HorizontalAlignment.Left;
            BattlefieldGrid.VerticalAlignment = VerticalAlignment.Top;
            BattlefieldGrid.ShowGridLines = true;
            BattlefieldGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            for(int w = 0; w < height;w++)
            {
                BattlefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int h = 0; h < width; h++)
            {
                BattlefieldGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int w = 0; w < height; w++)
            {
                for (int h = 0; h < width; h++)
                {
                    Button button = new Button();
                    button.Background = new SolidColorBrush(Color.FromArgb(255,0,0,0));
                    button.MinHeight = 50;
                    button.MinWidth = 50;
                    button.Name = "Field_" + w + "_" + h;
                    button.Click += new RoutedEventHandler(Battlefield_Change_Field);
                    button.Content = "";
                    Grid.SetColumn(button, w);
                    Grid.SetRow(button, h);
                    BattlefieldGrid.Children.Add(button);
                }
            }
            Grid.SetColumn(BattlefieldGrid, 0);
            Grid.SetRow(BattlefieldGrid, 2);
            Battlefield_Container.Children.Add(BattlefieldGrid);

            CurrentBattleFieldGrid = BattlefieldGrid;


            CurrentBattlefieldbuild = new List<DandDBattleSimulator.Classes.Point>();

        }
        private void Battlefield_Change_Field(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int row = Grid.GetRow(b);
            int column = Grid.GetColumn(b);
            DandDBattleSimulator.Classes.Point newPoint = new Classes.Point(column, row);
            if (b.Content.ToString() == " ")
            {
                CurrentBattlefieldbuild.RemoveAll(p => p.X == newPoint.X && p.Y == newPoint.Y);
                b.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                b.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                b.Content = "";
            }
            if (b.Content.ToString() == "")
            {
                CurrentBattlefieldbuild.Add(newPoint);
                b.Foreground = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                b.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                b.Content = " ";
            }
        }
        private void Battlefield_Save_Click(object sender, RoutedEventArgs e)
        {
            //Battlefields
            BattleField newBattlefield = new BattleField(randomizer, CurrentBattlefieldbuild);
            newBattlefield.sName = BattleField_Name.Text;
            Battlefields.Add(newBattlefield);
            string jsonwrite = JsonConvert.SerializeObject(newBattlefield);
            WriteJsonStringToFile(jsonwrite, "battlefield", newBattlefield.sName);
        }

        private void Battlefield_Configs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Battlefield_Configs.SelectedIndex != -1)
            {
                BattleField newBattleField = (BattleField)Battlefield_Configs.SelectedItem;
                Grid BattlefieldGrid = GenerateBattleGrid(newBattleField.getDimensions());
                int height = newBattleField.getDimensions().Item2;
                int width = newBattleField.getDimensions().Item1;
                BattleField_Name.Text = newBattleField.sName;
                Battlefield_Container.Children.Clear();
                BattlefieldGrid.Name = "Battlefield_Rendered";
                BattlefieldGrid.Width = Double.NaN;
                BattlefieldGrid.Height = Double.NaN;
                BattlefieldGrid.HorizontalAlignment = HorizontalAlignment.Left;
                BattlefieldGrid.VerticalAlignment = VerticalAlignment.Top;
                BattlefieldGrid.ShowGridLines = true;
                BattlefieldGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
                for (int w = 0; w < height; w++)
                {
                    BattlefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                for (int h = 0; h < width; h++)
                {
                    BattlefieldGrid.RowDefinitions.Add(new RowDefinition());
                }
                for (int w = 0; w < height; w++)
                {
                    for (int h = 0; h < width; h++)
                    {
                        Button button = new Button();
                        if (newBattleField.mapconfig.Where(x => x.X == w && x.Y == h).Count() > 0)
                        {
                            button.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                        }
                        else
                        {
                            button.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                        }
                        button.MinHeight = 50;
                        button.MinWidth = 50;
                        button.Name = "Field_" + w + "_" + h;
                        button.Click += new RoutedEventHandler(Battlefield_Change_Field);
                        button.Content = "";
                        Grid.SetColumn(button, w);
                        Grid.SetRow(button, h);
                        BattlefieldGrid.Children.Add(button);
                    }
                }
                Grid.SetColumn(BattlefieldGrid, 0);
                Grid.SetRow(BattlefieldGrid, 2);
                Battlefield_Container.Children.Add(BattlefieldGrid);

                CurrentBattleFieldGrid = BattlefieldGrid;


                CurrentBattlefieldbuild = new List<DandDBattleSimulator.Classes.Point>();
                //Battlefield_Container.Children.Add(CurrentBattleFieldGrid);
            }
        }
        #endregion
        #region Character
        private void Character_Save(object sender, RoutedEventArgs e)
        {
            #region Character_Stats_Data
            string name = Character_Name.Text;
            if (name == "")
            {
                MessageBox.Show("You must enter a name for the weapon");
                return;
            }
            int Health = 0;
            if (!Int32.TryParse(Character_HP.Text, out Health))
            {
                MessageBox.Show("You must enter a value for Health");
                return;
            }
            int Strength = 0;
            if (!Int32.TryParse(Character_STR.Text, out Strength))
            {
                MessageBox.Show("You must enter a value for Strength");
                return;
            }
            int Dexterity = 0;
            if (!Int32.TryParse(Character_Dex.Text, out Dexterity))
            {
                MessageBox.Show("You must enter a value for Dexterity");
                return;
            }
            int Constitution = 0;
            if (!Int32.TryParse(Character_Con.Text, out Constitution))
            {
                MessageBox.Show("You must enter a value for Constitution");
                return;
            }
            int Intelligence = 0;
            if (!Int32.TryParse(Character_Int.Text, out Intelligence))
            {
                MessageBox.Show("You must enter a value for Intelligence");
                return;
            }
            int Wisdom = 0;
            if (!Int32.TryParse(Character_Wis.Text, out Wisdom))
            {
                MessageBox.Show("You must enter a value for Wisdom");
                return;
            }
            int Charisma = 0;
            if (!Int32.TryParse(Character_Cha.Text, out Charisma))
            {
                MessageBox.Show("You must enter a value for Charisma");
                return;
            }
            int Will_Save = 0;
            if (!Int32.TryParse(Character_WillSave.Text, out Will_Save))
            {
                MessageBox.Show("You must enter a value for Will Save");
                return;
            }
            int Fortitude_Save = 0;
            if (!Int32.TryParse(Character_FortSave.Text, out Fortitude_Save))
            {
                MessageBox.Show("You must enter a value for Fortitude Save");
                return;
            }
            int Reflex_Save = 0;
            if (!Int32.TryParse(Character_FortSave.Text, out Reflex_Save))
            {
                MessageBox.Show("You must enter a value for Reflex Save");
                return;
            }
            int Death_Save = 0;
            if (!Int32.TryParse(Character_FortSave.Text, out Death_Save))
            {
                MessageBox.Show("You must enter a value for Death Save");
                return;
            }
            int Movement_Range = 0;
            if (!Int32.TryParse(Character_MoveMentrate.Text, out Movement_Range))
            {
                MessageBox.Show("You must enter a value for Movement Range");
                return;
            }
            #endregion
            #region Character
            //TODO integrate choice if wanting to update character (Combobox)
            Character Character;
            if (Character_CharacterList.SelectedItem != null)
            {
                Character = (Character) Character_CharacterList.SelectedItem;
            }
            else
            {
               Character = new Character();
            }
            
            Character.Name = name;
            Character.Strength = Strength;
            Character.Dexterity = Dexterity;
            Character.Constitution = Constitution;
            Character.Intelligence = Intelligence;
            Character.Wisdom = Wisdom;
            Character.Charisma = Charisma;
            Character.Willsave = Will_Save;
            Character.Fortitudesave = Fortitude_Save;
            Character.Reflexsave = Reflex_Save;
            Character.Deathsave = Death_Save;
            Character.MovementRate = Movement_Range;
            Character.Weapons = tempWeaponList;
            Character.Armors = tempArmorList;

            string jsonstring = JsonConvert.SerializeObject(Character);
            WriteJsonStringToFile(jsonstring, "character", Character.Name);
            Characters.Add(Character);
            #endregion
        }

        private void Character_Weapons_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Weapon weapon = StaticJSONCloneHelper.Clone((Weapon)Character_Weapon_ComboBox.SelectedItem);
            weapon.is_primary = false;
            if (Character_Weapons_IsPrimary.IsChecked == true)
            {
                weapon.is_primary = true;
            }
            tempWeaponList.Add(weapon);
            Character_Weapons.Items.Refresh();
        }
        private void Character_Weapons_RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            int weapon = Character_Weapons.SelectedIndex;
            tempWeaponList.RemoveAt(weapon);
            Character_Weapons.Items.Refresh();
        }
        private void Character_Armors_AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Armor armor = StaticJSONCloneHelper.Clone((Armor)Character_Armor_ComboBox.SelectedItem);
           
            tempArmorList.Add(armor);
            Character_Armors.Items.Refresh();
        }
        private void Character_Armors_RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            int armor = Character_Armors.SelectedIndex;
            tempArmorList.RemoveAt(armor);
            Character_Armors.Items.Refresh();
        }
        private void Character_CharacterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: load Character Stats into UI
        }
        
        #endregion

        private Grid GenerateBattleGrid(Tuple<int,int>Dimensions)
        {
            Grid newGrid = new Grid();

            Grid BattlefieldGrid = new Grid();
            BattlefieldGrid.Name = "Battlefield_Rendered";
            BattlefieldGrid.Width = Double.NaN;
            BattlefieldGrid.Height = Double.NaN;
            BattlefieldGrid.HorizontalAlignment = HorizontalAlignment.Left;
            BattlefieldGrid.VerticalAlignment = VerticalAlignment.Top;
            BattlefieldGrid.ShowGridLines = true;
            BattlefieldGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            for (int w = 0; w < Dimensions.Item1; w++)
            {
                BattlefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int h = 0; h < Dimensions.Item2; h++)
            {
                BattlefieldGrid.RowDefinitions.Add(new RowDefinition());
            }
            return newGrid;
        }
    }
}
