using MasterBoxGetWeight.Asset.Global;
using MasterBoxGetWeight.Commands;
using MasterBoxGetWeight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterBoxGetWeight.ViewModels {
    public class GetWeightViewModel {
        
        public GetWeightViewModel() {
            _gm = new GetWeightModel();
            _sm = myGlobal.settingviewmodel.SM;
            ByPassCommand = new GetByPassCommand(this);
            InputBox1Command = new GetInputBox1Command(this);
            InputBox2Command = new GetInputBox2Command(this);
        }

        GetWeightModel _gm;
        public GetWeightModel GM {
            get => _gm;
        }
        SettingModel _sm;
        public SettingModel SM {
            get => _sm;
        }

        //command
        public ICommand InputBox1Command {
            get;
            private set;
        }
        public ICommand InputBox2Command {
            get;
            private set;
        }
        public ICommand ByPassCommand {
            get;
            private set;
        }

    }
}
