using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Analog_Performance_viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        Thread thread;
        delegate void refrech_dele();
        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        RotateTransform rotateTransform_cpu;
        RotateTransform rotateTransform_ram;
        DoubleAnimation cpu_rotate_doubleAnimation;
        DoubleAnimation ram_rotate_doubleAnimation;
        double cpu_i = 0;
        double ram_i = 0;
        double prev_cpu_i;
        double prev_ram_i;
        long memKb;
        public static double _Height;
        public static double _Width;
        bool use_animations = true;
        public MainWindow()
        {

            InitializeComponent();
            
            _Height = this.Height;
            _Width = this.Width;
            set_statup_loc();

            this.StateChanged += MainWindow_StateChanged;

            GetPhysicallyInstalledSystemMemory(out memKb);


            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            rotateTransform_cpu = new RotateTransform();
            rotateTransform_ram = new RotateTransform();
            cpu_rotate_doubleAnimation = new DoubleAnimation();
            ram_rotate_doubleAnimation = new DoubleAnimation();
            rotateTransform_cpu.CenterX = -55;
            rotateTransform_cpu.CenterY = 0;
            rotateTransform_cpu.Angle = -200;

            rotateTransform_ram.CenterX = -35;
            rotateTransform_ram.CenterY = 0;
            rotateTransform_ram.Angle = -200;

            prev_cpu_i = -200;
            prev_ram_i = -200;

            cpu_usage_pointer.RenderTransform = rotateTransform_cpu;
            ram_usage_pointer.RenderTransform = rotateTransform_ram;

            thread = new Thread(Refresh);
            thread.Start();
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {

            if (this.WindowState == WindowState.Minimized) { Thread.Sleep(100); this.WindowState = WindowState.Maximized; }
        }

        public void set_statup_loc()
        {
            this.Left = AppSettings.Default.Left_loc;
            this.Top = AppSettings.Default.Top_loc;

        }


        public double map(double i)
        {
            return i * 2 - 200;
        }


        public void Refresh()
        {
            while (true)
            {
                cpu_i = getCurrentCpuUsage();
                ram_i = getUsedRAM();
                refrech_dele refrech = new refrech_dele(Refresh_UI);
                this.Dispatcher.Invoke(refrech);
                prev_cpu_i = cpu_i;
                prev_ram_i = ram_i;

                Thread.Sleep(200);
            }
        }

        public void Refresh_UI()
        {
            cpu_usage_lab.Content = (int)cpu_i + "%";
            ram_usage_lab.Content = (int)ram_i + "%";

            if (use_animations)
            {
                cpu_rotate_doubleAnimation.From = map(prev_cpu_i);
                cpu_rotate_doubleAnimation.To = map(cpu_i);
                cpu_rotate_doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

                rotateTransform_cpu.BeginAnimation(RotateTransform.AngleProperty, cpu_rotate_doubleAnimation);


                ram_rotate_doubleAnimation.From = map(prev_ram_i);
                ram_rotate_doubleAnimation.To = map(ram_i);
                ram_rotate_doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                rotateTransform_ram.BeginAnimation(RotateTransform.AngleProperty, ram_rotate_doubleAnimation);
            }
            else
            {
                rotateTransform_cpu.Angle = map(cpu_i);
                rotateTransform_ram.Angle = map(ram_i);
            }
           

        }
        public double getCurrentCpuUsage()
        {
            double f_val = cpuCounter.NextValue();
            Thread.Sleep(1000);

            double s_val = cpuCounter.NextValue();
            return s_val;

        }

        public float getUsedRAM()
        {
            float mem_total = memKb / 1024;
            float used_ram = (memKb / 1024) - (long)ramCounter.NextValue();
            float pres = (used_ram / mem_total) * 100;
            return pres;
        }

        private void exit_but_Click(object sender, RoutedEventArgs e)
        {
            thread.Abort();
            this.Close();
        }

        private void settings_but_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Closed += (s, _) => { on_settingsWindow_closed(); };
            settingsWindow.Show();

        }

        private void on_settingsWindow_closed()
        {
            set_statup_loc();
            if(AppSettings.Default.Show_CPU_usage)
            {
                CPU_usage_canvas.Visibility = Visibility.Visible;
            }
            else
            {
                CPU_usage_canvas.Visibility = Visibility.Collapsed;
            }

            if (AppSettings.Default.UI_animations)
            {
                use_animations = true;

            }
            else
            {
                use_animations = false;
            }
        }


    }
}
