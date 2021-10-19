using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Asset.IO;
using MasterBoxGetWeight.Models;
using MasterBoxGetWeight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityPack.Converter;

namespace MasterBoxGetWeight.Commands {
    public class GetInputBox2Command : ICommand {

        private GetWeightViewModel _gvm;
        public GetInputBox2Command(GetWeightViewModel gvm) {
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
            myGlobal.get_start_thread = new Thread(new ThreadStart(() => {
                var Setw = _gvm.SM;
                var getw = _gvm.GM;

                if (!checkPrecondition(getw, Setw)) {
                    getw.allowInput1 = true;
                    getw.allowInput2 = false;
                    return;
                }

                App.Current.Dispatcher.Invoke(new Action(() => { getw.Init(); }));
                getw.Wait();
                int count_max = 300; //tương đương timeout = 30s
                int retry_max = 3;
                int count = 0;
                int retry = 0;
                bool r = false;

            RE:
                count++;
                retry++;

                //cân mất kết nối
                string data = myGlobal.cas_get.getSTValue();
                if (data == null) {
                    Thread.Sleep(100);
                    if (count < count_max) goto RE;
                    else goto END;
                }

                //giá trị cân < 10g
                double st_value = double.Parse(data);
                if (st_value < 10) {
                    Thread.Sleep(100);
                    if (count < count_max) goto RE;
                    else goto END;
                }

                double ul = double.Parse(Setw.upperLimit);
                double ll = double.Parse(Setw.lowerLimit);
                double actual = 0;

                switch (Setw.UOM.ToLower()) {
                    case "kg": { actual = (st_value * 1.0) / 1000; break; }
                    case "mg": { actual = st_value * 1000; break; }
                    default: { actual = st_value; break; }
                }

                getw.actualValue = actual.ToString();

                r = actual >= ll && actual <= ul;
                if (!r) {
                    if (retry < retry_max) goto RE;
                    else goto END;
                }

            END:
                bool ___ = r ? getw.Pass() : getw.Fail();
                if (myGlobal.cas_get != null) myGlobal.cas_get.Dispose();
            }));
            myGlobal.get_start_thread.IsBackground = true;
            myGlobal.get_start_thread.Start();


            Thread z = new Thread(new ThreadStart(() => {
                int count = 0;
                while (myGlobal.get_start_thread.IsAlive) {
                    count++;
                    Thread.Sleep(500);
                    var getw = _gvm.GM;
                    getw.totalTime = myConverter.intToTimeSpan(count * 500);
                }
                if (myGlobal.cas_get != null) myGlobal.cas_get.Dispose();
            }));
            z.IsBackground = true;
            z.Start();

        }


        private bool checkBarcodeFormat(string barcode) {
            string pattern = "^T[0-9,A-Z]{7,100}$";
            return System.Text.RegularExpressions.Regex.IsMatch(barcode, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private bool checkPrecondition(GetWeightModel getw, SettingModel Setw) {
            bool r = false;
            //check format
            r = checkBarcodeFormat(getw.inputBarcode2);
            if (r == false) {
                System.Windows.MessageBox.Show($"Input barcode 2 \"{getw.inputBarcode2.ToUpper()}\" sai định dạng.", "Barcode Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode1 = "";
                getw.inputBarcode2 = "";
                return false;
            }

            //check same
            r = getw.inputBarcode1.Equals(getw.inputBarcode2);
            if (r == false) {
                System.Windows.MessageBox.Show($"Input barcode 1 \"{getw.inputBarcode1.ToUpper()}\" khác Input barcode 2 \"{getw.inputBarcode2.ToUpper()}\".", "Barcode Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                getw.inputBarcode1 = "";
                getw.inputBarcode2 = "";
                return false;
            }

            return true;
        }

        #endregion


    }
}
