﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Models {

    public class MainHeaderModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public MainHeaderModel() {
            productName = "REMOTE UEI";
            stationName = "Get weight master box";
            Version = "RMTUEIVN0U0002";
            buildTime = "26/08/2021 08:00";
            copyRight = "VNPT Technology 2021";
        }

        string _product_name;
        public string productName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(productName));
            }
        }
        string _station_name;
        public string stationName {
            get { return _station_name; }
            set {
                _station_name = value;
                OnPropertyChanged(nameof(stationName));
            }
        }
        string _version;
        public string Version {
            get { return _version; }
            set {
                _version = value;
                OnPropertyChanged(nameof(Version));
            }
        }
        string _build_time;
        public string buildTime {
            get { return _build_time; }
            set {
                _build_time = value;
                OnPropertyChanged(nameof(buildTime));
            }
        }
        string _copy_right;
        public string copyRight {
            get { return _copy_right; }
            set {
                _copy_right = value;
                OnPropertyChanged(nameof(copyRight));
            }
        }
    }
}
