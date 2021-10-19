﻿using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Models;
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
using UtilityPack.IO;

namespace MasterBoxGetWeight {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.mainheaderviewmodel;
        }

        ~MainWindow() {
            XmlHelper<MainHeaderModel>.ToXmlFile(myGlobal.mainheaderviewmodel.MHM, AppDomain.CurrentDomain.BaseDirectory + "main.xml");
        }
    }
}
