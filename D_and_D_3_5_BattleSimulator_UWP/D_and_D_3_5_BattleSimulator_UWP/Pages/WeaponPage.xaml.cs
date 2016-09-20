using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using D_and_D_3_5_BattleSimulator_UWP.DataClasses;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace D_and_D_3_5_BattleSimulator_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeaponPage : Page
    {
        ObservableCollection<Weapons> WeaponItems = new ObservableCollection<DataClasses.Weapons>();
        public WeaponPage()
        {
            this.InitializeComponent();
            WeaponsList.ItemsSource = WeaponItems;
        }
        public override string ToString()
        {
            return "Weapons";
        }
        private void Weapons_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WeaponPage));
        }

        private void Armors_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Feats_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Abilities_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Characters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Battlefields_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Simulate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Numeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(((TextBox)sender).Text != "")
            {
                ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9.]", "");
            }
            else
            {
                ((TextBox)sender).Text = "0";
            }
            
        }

        private void RemoveWeapon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = Int32.Parse(((Button)sender).Tag.ToString());
                WeaponItems.RemoveAt(ID);
                if(WeaponItems.Count() > 1)
                {
                    foreach (Weapons weapon in WeaponItems)
                    {
                        if (weapon.ID > ID)
                        {
                            weapon.ID--;
                        }
                    }
                }
            }catch(FormatException)
            {

            }
            
        }

        private void AddWeapon_Click(object sender, RoutedEventArgs e)
        {
            Weapons newWeapon = new Weapons("", 0, 0, 0, WeaponItems.Count(),"");
            WeaponItems.Add(newWeapon);
        }
        
    }
}
