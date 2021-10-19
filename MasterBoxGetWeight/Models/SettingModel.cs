using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.Models {
    public class SettingModel : INotifyPropertyChanged {

        public SettingModel() {
            weighAddress = weighBaudRate = "";
            workOrder = operatorID = lineName = stationNumber = jigNumber = operationCodeList = baseRoutingName = baseRoutingVersion = "";
            productName = productCode = upperLimit = lowerLimit = UOM = "";
        }

        string _weigh_address;
        public string weighAddress {
            get { return _weigh_address; }
            set {
                _weigh_address = value;
                OnPropertyChanged(nameof(weighAddress));
            }
        }
        string _weigh_baud_rate;
        public string weighBaudRate {
            get { return _weigh_baud_rate; }
            set {
                _weigh_baud_rate = value;
                OnPropertyChanged(nameof(weighBaudRate));
            }
        }
        string _work_order;
        public string workOrder {
            get { return _work_order; }
            set {
                _work_order = value;
                OnPropertyChanged(nameof(workOrder));
            }
        }
        string _operator_id;
        public string operatorID {
            get { return _operator_id; }
            set {
                _operator_id = value;
                OnPropertyChanged(nameof(operatorID));
            }
        }
        string _line_name;
        public string lineName {
            get { return _line_name; }
            set {
                _line_name = value;
                OnPropertyChanged(nameof(lineName));
            }
        }
        string _station_number;
        public string stationNumber {
            get { return _station_number; }
            set {
                _station_number = value;
                OnPropertyChanged(nameof(stationNumber));
            }
        }
        string _jig_number;
        public string jigNumber {
            get { return _jig_number; }
            set {
                _jig_number = value;
                OnPropertyChanged(nameof(jigNumber));
            }
        }
        string _operation_code_list;
        public string operationCodeList {
            get { return _operation_code_list; }
            set {
                _operation_code_list = value;
                OnPropertyChanged(nameof(operationCodeList));
            }
        }
        string _base_routing_name;
        public string baseRoutingName {
            get { return _base_routing_name; }
            set {
                _base_routing_name = value;
                OnPropertyChanged(nameof(baseRoutingName));
            }
        }
        string _base_routing_version;
        public string baseRoutingVersion {
            get { return _base_routing_version; }
            set {
                _base_routing_version = value;
                OnPropertyChanged(nameof(baseRoutingVersion));
            }
        }
        string _product_name;
        public string productName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(productName));
            }
        }
        string _product_code;
        public string productCode {
            get { return _product_code; }
            set {
                _product_code = value;
                OnPropertyChanged(nameof(productCode));
            }
        }
        string _upper_limit;
        public string upperLimit {
            get { return _upper_limit; }
            set {
                _upper_limit = value;
                OnPropertyChanged(nameof(upperLimit));
            }
        }
        string _lower_limit;
        public string lowerLimit {
            get { return _lower_limit; }
            set {
                _lower_limit = value;
                OnPropertyChanged(nameof(lowerLimit));
            }
        }
        string _uom;
        public string UOM {
            get { return _uom; }
            set {
                _uom = value;
                OnPropertyChanged(nameof(UOM));
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
