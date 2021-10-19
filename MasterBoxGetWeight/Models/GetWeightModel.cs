using MasterBoxGetWeight.Asset.Custom;
using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MasterBoxGetWeight.Models {
    public class GetWeightModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public GetWeightModel() {
            Init();
            collectionWeight = new ObservableCollection<GetItemResult>();
            productID = "";
            inputBarcode1 = "";
            inputBarcode2 = "";
        }

        #region method

        public void Init() {
            actualValue = startTime = endTime = logSystem = logWeigh = "";
            totalTime = "00:00:00";
            totalResult = "";
            allowInput1 = true;
            allowInput2 = false;
        }

        public void Wait() {
            totalResult = "Waiting...";
            productID = inputBarcode1;
            allowInput1 = false;
            allowInput2 = false;
            startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public bool Pass() {
            totalResult = "Passed";
            allowInput1 = true;
            allowInput2 = false;
            inputBarcode1 = "";
            inputBarcode2 = "";
            endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            App.Current.Dispatcher.Invoke(new Action(() => {
                var item = new GetItemResult();
                collectionWeight.Add(item);
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                db.InsertDataToTable<GetItemResult>(item, "tb_get_weight");
                db.Close();
            }));
            return true;
        }

        public bool Fail() {
            totalResult = "Failed";
            allowInput1 = true;
            allowInput2 = false;
            inputBarcode1 = "";
            inputBarcode2 = "";
            endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            App.Current.Dispatcher.Invoke(new Action(() => {
                var item = new GetItemResult();
                collectionWeight.Add(item);
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                db.InsertDataToTable<GetItemResult>(item, "tb_get_weight");
                db.Close();
            }));
            return true;
        }

        public bool ByPass() {
            totalResult = "ByPass";
            allowInput1 = true;
            allowInput2 = false;
            inputBarcode1 = "";
            inputBarcode2 = "";
            App.Current.Dispatcher.Invoke(new Action(() => {
                var item = collectionWeight[collectionWeight.Count - 1];
                DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
                db.QueryDeleteOrUpdate("DELETE FROM tb_get_weight WHERE uid3='" + item.uid3 + "' AND start_time='" + item.start_time + "' AND end_time='" + item.end_time + "'");
                Thread.Sleep(100);
                item.result = totalResult;
                item.end_time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                db.InsertDataToTable<GetItemResult>(item, "tb_get_weight");
                db.Close();
            }));
            return true;
        }

        #endregion

        #region property

        bool _allow_input1;
        public bool allowInput1 {
            get { return _allow_input1; }
            set {
                _allow_input1 = value;
                OnPropertyChanged(nameof(allowInput1));
            }
        }
        bool _allow_input2;
        public bool allowInput2 {
            get { return _allow_input2; }
            set {
                _allow_input2 = value;
                OnPropertyChanged(nameof(allowInput2));
            }
        }
        string _input_barcode1;
        public string inputBarcode1 {
            get { return _input_barcode1; }
            set {
                _input_barcode1 = value;
                OnPropertyChanged(nameof(inputBarcode1));
            }
        }
        string _input_barcode2;
        public string inputBarcode2 {
            get { return _input_barcode2; }
            set {
                _input_barcode2 = value;
                OnPropertyChanged(nameof(inputBarcode2));
            }
        }
        string _product_id;
        public string productID {
            get { return _product_id; }
            set {
                _product_id = value;
                OnPropertyChanged(nameof(productID));
            }
        }
        string _actual_value;
        public string actualValue {
            get { return _actual_value; }
            set {
                _actual_value = value;
                OnPropertyChanged(nameof(actualValue));
            }
        }
        string _start_time;
        public string startTime {
            get { return _start_time; }
            set {
                _start_time = value;
                OnPropertyChanged(nameof(startTime));
            }
        }
        string _end_time;
        public string endTime {
            get { return _end_time; }
            set {
                _end_time = value;
                OnPropertyChanged(nameof(endTime));
            }
        }
        string _total_time;
        public string totalTime {
            get { return _total_time; }
            set {
                _total_time = value;
                OnPropertyChanged(nameof(totalTime));
            }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set {
                _log_system = value;
                OnPropertyChanged(nameof(logSystem));
            }
        }
        string _log_weigh;
        public string logWeigh {
            get { return _log_weigh; }
            set {
                _log_weigh = value;
                OnPropertyChanged(nameof(logWeigh));
                string[] buffer = _log_weigh.Split('\n');
                if (buffer.Length > 25) _log_weigh = "";
            }
        }

        #endregion

        ObservableCollection<GetItemResult> _collection_weight;
        public ObservableCollection<GetItemResult> collectionWeight {
            get { return _collection_weight; }
            set {
                _collection_weight = value;
                OnPropertyChanged(nameof(collectionWeight));
            }
        }
    }
}
