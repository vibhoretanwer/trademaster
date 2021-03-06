﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace TradeMaster.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        public string AccessToken { get; set; }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //AccessToken = File.ReadAllText(@"D:\accessToken.txt");
            if (string.IsNullOrEmpty(AccessToken))
            {
                new LoginWindow().ShowDialog();
                this.Close();
            }
            //else
            //{
            //    DataContext = new MainWindowViewModel(AccessToken);
            //}
        }
    }
}
