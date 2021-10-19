using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterBoxGetWeight.Commands {
    public class GetInputBox1Command : ICommand {

        private GetWeightViewModel _gvm;
        public GetInputBox1Command(GetWeightViewModel gvm) {
            _gvm = gvm;
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
            var Setw = _gvm.SM;
            var getw = _gvm.GM;

            if (!checkPrecondition(getw, Setw)) return;
            else {
                _gvm.GM.allowInput1 = false;
                _gvm.GM.allowInput2 = true;
            }
        }

        private bool checkBarcodeFormat(string barcode) {
            string pattern = "^T[0-9,A-Z]{7,100}$";
            return System.Text.RegularExpressions.Regex.IsMatch(barcode, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private bool checkPrecondition(GetWeightModel getw, SettingModel Setw) {
            bool r = false;
            //check format
            r = checkBarcodeFormat(getw.inputBarcode1);
            if (r == false) {
                System.Windows.MessageBox.Show($"Input barcode \"{getw.inputBarcode1.ToUpper()}\" sai định dạng.", "Barcode Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode1 = "";
                getw.inputBarcode2 = "";
                return false;
            }

            //check dupplicate
            DBHelper db = new DBHelper(AppDomain.CurrentDomain.BaseDirectory + "References\\GetWeight.accdb");
            r = db.CheckDupplicate("tb_get_weight", "uid3", getw.inputBarcode1, "work_order", Setw.workOrder);
            db.Close();
            if (r) {
                System.Windows.MessageBox.Show("Sản phẩm bị trùng lặp.", "Setting Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode1 = "";
                getw.inputBarcode2 = "";
                return false;
            }

            //check setting
            string msg;
            r = mySupport.checkSettingInfo(myGlobal.settingviewmodel, out msg);
            if (r == false) {
                System.Windows.MessageBox.Show(msg, "Setting Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode1 = "";
                getw.inputBarcode2 = "";
                return false;
            }

            //check connection to weigh
            myGlobal.cas_get = new Asset.IO.CASEDHHelper<Models.GetWeightModel>(getw, Setw.weighAddress, Setw.weighBaudRate);
            if (myGlobal.cas_get.isConnected == false) {
                System.Windows.MessageBox.Show("Không kết nối được tới cân.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode1 = "";
                getw.inputBarcode2 = "";
                return false;
            }

            return true;
        }

        #endregion
    }
}
