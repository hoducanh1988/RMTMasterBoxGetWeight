using MasterBoxGetWeight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxGetWeight.ViewModels {
    public class AboutViewModel {

        public AboutViewModel() {
            _abouts = new ObservableCollection<AboutModel>();
            _abouts.Add(new AboutModel() {
                ID = "1",
                Version = "RMTUEIVN0U0001",
                Content = "- Phát hành lần đầu.",
                Date = "26/07/2021", ChangeType = "Tạo mới",
                Person = "Hồ Đức Anh"
            });
            _abouts.Add(new AboutModel() {
                ID = "2",
                Version = "RMTUEIVN0U0002",
                Content = "- Fix bug tool báo pass khi không đặt sản phẩm lên cân.\n" +
                          "- Fix bug tool báo pass khi mất kết nối cân với usb to uart.\n" + 
                          "- Update thêm chức năng check trùng lặp số thùng.\n" + 
                          "- Update thêm chức năng check định dạng product ID (bắt đầu là T và > 7 kí tự).",
                Date = "26/08/2021", ChangeType = "Chỉnh sửa",
                Person = "Hồ Đức Anh"
            });
            _abouts.Add(new AboutModel() {
                ID = "3",
                Version = "RMTUEIVN0U0003",
                Content = "- Update thêm chức năng check trùng khớp 2 tem FG dán trên thùng carton.",
                Date = "05/10/2021", ChangeType = "Chỉnh sửa",
                Person = "Hồ Đức Anh"
            });
        }

        ObservableCollection<AboutModel> _abouts;
        public ObservableCollection<AboutModel> Abouts {
            get { return _abouts; }
            set { _abouts = value; }
        }
    }
}
