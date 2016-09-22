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
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace D_and_D_3_5_BattleSimulator_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArmorPage : Page
    {
        ObservableCollection<Armors> ArmorItems = new ObservableCollection<DataClasses.Armors>();
        public ArmorPage()
        {

            this.InitializeComponent();
            ArmorsList.ItemsSource = ArmorItems;
            InitFile();
        }

        private async void SaveArmors_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(ArmorItems);
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile ArmorsFile = await storageFolder.GetFileAsync("Armors.json");
            await Windows.Storage.FileIO.WriteTextAsync(ArmorsFile, json);
        }

        private void AddArmor_Click(object sender, RoutedEventArgs e)
        {
            Armors newArmor = new Armors(0,0,"");
            ArmorItems.Add(newArmor);
        }

        private void RemoveArmor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = Int32.Parse(((Button)sender).Tag.ToString());
                ArmorItems.RemoveAt(ID);
                if (ArmorItems.Count() > 1)
                {
                    foreach (Armors armor in ArmorItems)
                    {
                        if (armor.ID > ID)
                        {
                            armor.ID--;
                        }
                    }
                }
            }
            catch (FormatException)
            {

            }
        }
        
        public async void InitFile()
        {
            if (!(await FileExists("Armors.json")))
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                System.Diagnostics.Debug.WriteLine(storageFolder.Path);
                Windows.Storage.StorageFile Armors = await storageFolder.CreateFileAsync("Armors.json");
            }
            System.Diagnostics.Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
            Windows.Storage.StorageFolder storageFolder2 = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile ArmorsFile = null;
            ArmorsFile = await storageFolder2.GetFileAsync("Armors.json");

            string armors = await Windows.Storage.FileIO.ReadTextAsync(ArmorsFile);
            ObservableCollection<Armors> temp = JsonConvert.DeserializeObject<ObservableCollection<Armors>>(armors);
            if(temp != null && temp.Count() > 0)
            {
                foreach (Armors armor in temp)
                {
                    ArmorItems.Add(armor);
                }
            }
        }
        public static async Task<bool> FileExists(string _filename)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(_filename);
                return true;
            }
            catch (FileNotFoundException ex)
            {
                return false;
            }
        }
        public override string ToString()
        {
            return "Armors";
        }
        private void Weapons_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WeaponPage));
        }

        private void Armors_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ArmorPage));
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
            if (((TextBox)sender).Text != "")
            {
                ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9.]", "");
            }
            else
            {
                ((TextBox)sender).Text = "0";
            }

        }
    }
}
