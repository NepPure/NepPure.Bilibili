using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepPure.Bilibili.ViewModels
{
    public class ConfigVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ConfigVm()
        {
            BilibiliUsers = new List<BilibiliUserVm>();
        }

        public List<BilibiliUserVm> BilibiliUsers { get; set; }
    }
}
