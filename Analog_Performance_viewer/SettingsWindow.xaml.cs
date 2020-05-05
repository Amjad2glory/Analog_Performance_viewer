using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Analog_Performance_viewer
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            InitializeCheckBoxsState();
        }

        private void InitializeCheckBoxsState()
        {
            if (AppSettings.Default.Show_CPU_usage)
            {
                show_cpu_usage_checkbox.IsChecked = true;
            }
            else
            {
                show_cpu_usage_checkbox.IsChecked = false;
            }

            if (AppSettings.Default.UI_animations)
            {
                ui_animations_checkbox.IsChecked = true;
            }
            else
            {
                ui_animations_checkbox.IsChecked = false;
            }
        }

        private void top_right_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = 10;
            AppSettings.Default.Left_loc = SystemParameters.PrimaryScreenWidth - MainWindow._Width;
            AppSettings.Default.Save();
            loc_lab.Content = "Top Right";
        }

        private void center_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Sdav");
        }

        private void center_top_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = 10;
            AppSettings.Default.Left_loc = SystemParameters.PrimaryScreenWidth / 2;
            AppSettings.Default.Save();
            loc_lab.Content = "Top Center";
        }

        private void center_bottom_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = SystemParameters.PrimaryScreenHeight - MainWindow._Height;
            AppSettings.Default.Left_loc = SystemParameters.PrimaryScreenWidth / 2;
            AppSettings.Default.Save();
            loc_lab.Content = "Bottom Center";
        }

        private void center_left_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = SystemParameters.PrimaryScreenHeight / 2;
            AppSettings.Default.Left_loc = 10;
            AppSettings.Default.Save();
            loc_lab.Content = "Bottom Center";
        }

        private void center_right_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = SystemParameters.PrimaryScreenHeight / 2;
            AppSettings.Default.Left_loc = SystemParameters.PrimaryScreenWidth - MainWindow._Width;
            AppSettings.Default.Save();
            loc_lab.Content = "Bottom Center";
        }

        private void top_left_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = 10;
            AppSettings.Default.Left_loc = 10;
            AppSettings.Default.Save();
            loc_lab.Content = "Top Left";
        }

        private void bottom_left_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = SystemParameters.PrimaryScreenHeight - MainWindow._Height;
            AppSettings.Default.Left_loc = 10;
            AppSettings.Default.Save();
            loc_lab.Content = "Bottom Left";
        }

        private void bottom_right_rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = SystemParameters.PrimaryScreenHeight - MainWindow._Height;
            AppSettings.Default.Left_loc = SystemParameters.PrimaryScreenWidth - MainWindow._Width;
            AppSettings.Default.Save();
            loc_lab.Content = "Bottom Right";
        }

        private void top_right_rect_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            AppSettings.Default.Top_loc = 10;
            AppSettings.Default.Left_loc = SystemParameters.PrimaryScreenWidth - MainWindow._Width;
            AppSettings.Default.Save();
            loc_lab.Content = "Top Right";
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void startup_checkbox_Checked(object sender, RoutedEventArgs e)
        {


            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
            key.SetValue("Analog_monitor", System.Reflection.Assembly.GetExecutingAssembly().Location);

        }

        private void help_but_Click(object sender, RoutedEventArgs e)
        {
            new HelpWindow().Show();
        }

        private void exit_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void show_cpu_usage_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.Show_CPU_usage = true;
            AppSettings.Default.Save();
        }

        private void ui_animations_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.UI_animations = true;
            AppSettings.Default.Save();
        }

        private void startup_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void show_cpu_usage_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.Show_CPU_usage = false;
            AppSettings.Default.Save();
        }

        private void ui_animations_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.UI_animations = false;
            AppSettings.Default.Save();
        }
    }
}
