using NepPure.Bilibili.Managers;
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
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;

namespace NepPure.Bilibili.ViewPanels
{
    /// <summary>
    /// UserList.xaml 的交互逻辑
    /// </summary>
    public partial class UserList : UserControl
    {
        private readonly BackgroundWorker _backgroundWorker;

        public UserList()
        {
            InitializeComponent();
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (e.Cancel)
                {
                    return;
                }
                RefreshAsync().Wait();
                Task.Delay(TimeSpan.FromMinutes(1)).Wait();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loading = await App.MainWin.ShowProgressAsync("请稍后", "刷新船员中...");
            loading.SetIndeterminate();
            await RefreshAsync();
            await loading.CloseAsync();
        }

        private async Task RefreshAsync()
        {
            try
            {
                var manager = new BilibiliApiManager();
                var users = await manager.GetLiveTopListAsync(823947, 653768);
                App.MainWin.MainVm.Config.BilibiliUsers = manager.MergeUsers(App.MainWin.MainVm.Config.BilibiliUsers, users);
                await App.MainWin.MainVm.UpdateSearchAsync();
            }
            catch (Exception ex)
            {
                await App.MainWin.ShowMessageAsync("刷新船员列表出错", ex.Message, MessageDialogStyle.Affirmative);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await App.MainWin.MainVm.UpdateSearchAsync();
        }
    }
}
