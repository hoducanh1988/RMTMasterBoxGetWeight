﻿using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Commands;
using MasterBoxGetWeight.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using UtilityPack.IO;

namespace MasterBoxGetWeight.ViewModels {
    public class SettingViewModel {

        public SettingViewModel() {

            //load setting file
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "setting.xml") == false) _sm = new SettingModel();
            else _sm = XmlHelper<SettingModel>.FromXmlFile(AppDomain.CurrentDomain.BaseDirectory + "setting.xml");

            List<string> listCom = new List<string>();
            List<string> listNumber = new List<string>();
            for (int i = 1; i <= 99; i++) {
                listCom.Add($"COM{i}");
                listNumber.Add($"{i}");
            }
            myGlobal.listProduct = new ProductFileHelper().fromFileToList();

            _collection_serial_port = new CollectionView(listCom);
            _collection_baud_rate = new CollectionView(new List<int>() { 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 });
            _collection_station_number = new CollectionView(listNumber);
            _collection_jig_number = new CollectionView(listNumber);
            _collection_weight_unit = new CollectionView(new List<string>() { "kg", "g", "mg" });
            if (myGlobal.listProduct == null) {
                _collection_product_name = null;
                _collection_product_code = null;
            }
            else {
                _collection_product_name = new CollectionView(myGlobal.listProduct.Select(x => x.Split(',')[1]));
                _collection_product_code = new CollectionView(myGlobal.listProduct.Select(x => x.Split(',')[0]));
            }
            SaveCommand = new SettingSaveCommand(this);
        }


        SettingModel _sm;
        public SettingModel SM {
            get => _sm;
        }

        CollectionView _collection_baud_rate;
        public CollectionView collectionBaudRate {
            get => _collection_baud_rate;
        }
        CollectionView _collection_serial_port;
        public CollectionView collectionSerialPort {
            get => _collection_serial_port;
        }
        CollectionView _collection_station_number;
        public CollectionView collectionStationNumber {
            get => _collection_station_number;
        }
        CollectionView _collection_jig_number;
        public CollectionView collectionJigNumber {
            get => _collection_jig_number;
        }
        CollectionView _collection_weight_unit;
        public CollectionView collectionWeightUnit {
            get => _collection_weight_unit;
        }
        CollectionView _collection_product_name;
        public CollectionView collectionProductName {
            get => _collection_product_name;
        }
        CollectionView _collection_product_code;
        public CollectionView collectionProductCode {
            get => _collection_product_code;
        }

        //command
        public ICommand SaveCommand {
            get;
            private set;
        }

    }
}
