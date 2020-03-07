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

namespace NepPure.Bilibili.ViewPanels
{
    /// <summary>
    /// LiveQueuePanel.xaml 的交互逻辑
    /// </summary>
    public partial class LiveQueuePanel : UserControl
    {
        public LiveQueuePanel()
        {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await App.MainWin.MainVm.UpdateSearchAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button btn))
            {
                return;
            }

            var v = btn.Tag as BilibiliUserVm;
            v.IsInQueue = !v.IsInQueue;
            v.InQueueTime = DateTime.Now;
            var no = 1;
            foreach (var q in App.MainWin.MainVm.Config.BilibiliUsers.Where(m => m.IsInQueue).OrderBy(m => m.InQueueTime))
            {
                q.QueueNo = no++;
            }
            await App.MainWin.MainVm.UpdateSearchAsync();
        }
    }
}
