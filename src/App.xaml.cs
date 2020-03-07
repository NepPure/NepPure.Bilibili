using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NepPure.Bilibili
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWin { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWin = new MainWindow();
            MainWin.Show();

            base.OnStartup(e);
        }
    }
}
