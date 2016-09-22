using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using D_and_D_3_5_BattleSimulator_UWP.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace D_and_D_3_5_BattleSimulator_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
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
    }
}
