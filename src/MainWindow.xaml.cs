using MahApps.Metro.Controls;
using NepPure.Bilibili.Managers;
using NepPure.Bilibili.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NepPure.Bilibili
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainVm MainVm { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainVm = new MainVm
            {
                Config = ConfigurationManager.Read()
            };

            DataContext = MainVm;
            WebsocketServer.Start(MainVm.Config.Port);
        }

        protected override void OnClosed(EventArgs e)
        {
            // 关闭后保存配置
            ConfigurationManager.Save(MainVm.Config);
            WebsocketServer.Stop();
            base.OnClosed(e);
        }

        private async void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
          await  MainVm.UpdateSearchAsync();
        }
    }
}
