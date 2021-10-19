﻿using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using UtilityPack.IO;
using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Asset.Global;

namespace MasterBoxGetWeight.Commands {
    public class SettingSaveCommand : ICommand {

        private SettingViewModel _svm;
        public SettingSaveCommand(SettingViewModel svm) {
            _svm = svm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button save setting
        public bool CanExecute(object parameter) {
            return true;
        }

        //save setting
        public void Execute(object parameter) {
            string msg;
            bool r = mySupport.checkSettingInfo(_svm, out msg);
            if (r == false) {
                System.Windows.MessageBox.Show(msg, "Setting Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                //return;
            }

            new ProductFileHelper().fromSettingToFile();
            XmlHelper<SettingModel>.ToXmlFile(_svm.SM, AppDomain.CurrentDomain.BaseDirectory + "setting.xml");
            System.Windows.MessageBox.Show("Sucess!", "Save Setting", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        #endregion

    }
}
