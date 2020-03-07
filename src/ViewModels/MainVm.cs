using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepPure.Bilibili.ViewModels
{
    public class MainVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainVm()
        {
            Config = new ConfigVm();
        }

        public string VersionLabel => "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public ConfigVm Config { get; set; }
    }
}
