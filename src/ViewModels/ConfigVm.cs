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

        /// <summary>
        /// ws服务监听端口
        /// </summary>
        public int Port { get; set; } = 5678;
    }
}
