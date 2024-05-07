using ProjPL3D;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace ProjPL3D
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {         
            InitializeComponent();
            mainFrame.Navigate(new LoginPage());
        }
    }
}