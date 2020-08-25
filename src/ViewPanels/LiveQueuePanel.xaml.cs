using MahApps.Metro.Controls.Dialogs;
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
            await AddOrRemove(v);
        }

        private async Task AddOrRemove(BilibiliUserVm v)
        {
            if (v.IsInQueue)
            {
                // 本次是移出
                v.IsInQueue = false;
                int order = 1;
                foreach (var p in App.MainWin.MainVm.Config.BilibiliUsers.Where(m => m.IsInQueue).OrderBy(m => m.QueueNo))
                {
                    p.QueueNo = order++;
                }
            }
            else
            {
                // 本次是移入
                v.IsInQueue = true;
                v.InQueueTime = DateTime.Now;
                v.QueueNo = App.MainWin.MainVm.Config.BilibiliUsers.Where(m => m.IsInQueue).Count();
            }

            await App.MainWin.MainVm.UpdateSearchAsync();
        }

        private BilibiliUserVm CheckInQueue(object sender)
        {
            if (!(sender is Button btn))
            {
                return null;
            }

            var v = btn.Tag as BilibiliUserVm;
            if (v.IsInQueue)
            {
                return v;
            }

            return null;
        }

        private async Task SetOrder(BilibiliUserVm v, int newOrder)
        {
            if (v.QueueNo == newOrder)
            {
                return;
            }

            var max = App.MainWin.MainVm.Config.BilibiliUsers
                .Where(m => m.IsInQueue)
                .Count();

            var target = App.MainWin.MainVm.Config.BilibiliUsers
                .Where(m => m.IsInQueue)
                .Where(m => m.QueueNo == newOrder)
                .FirstOrDefault();

            if (target == null)
            {
                return;
            }

            var listNo = new Queue<int>();
            var curent = 1;
            while (curent <= max)
            {
                if (curent == newOrder)
                {
                    curent++;
                    continue;
                }

                listNo.Enqueue(curent);
                curent++;
            }

            v.QueueNo = newOrder;

            foreach (var p in App.MainWin.MainVm.Config.BilibiliUsers
                .Where(m => m.IsInQueue)
                .Where(m => m.Uid != v.Uid)
                .OrderBy(m => m.QueueNo))
            {
                p.QueueNo = listNo.Dequeue();
            }

            await App.MainWin.MainVm.UpdateSearchAsync();
        }

        private async void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            var v = CheckInQueue(sender);
            if (v == null)
            {
                return;
            }

            await SetOrder(v, v.QueueNo - 1);
        }

        private async void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            var v = CheckInQueue(sender);
            if (v == null)
            {
                return;
            }

            await SetOrder(v, v.QueueNo + 1);
        }

        private async void ButtonTop_Click(object sender, RoutedEventArgs e)
        {
            var v = CheckInQueue(sender);
            if (v == null)
            {
                return;
            }

            await SetOrder(v, 1);
        }

        private async void ButtonBottom_Click(object sender, RoutedEventArgs e)
        {
            var v = CheckInQueue(sender);
            if (v == null)
            {
                return;
            }

            await SetOrder(v, App.MainWin.MainVm.Config.BilibiliUsers
                .Where(m => m.IsInQueue)
                .Count());
        }

        private async void Button_Click_AddTemp(object sender, RoutedEventArgs e)
        {
            var exitUser = App.MainWin.MainVm.Config.BilibiliUsers.FirstOrDefault(m => m.UserName == App.MainWin.MainVm.TempUserName);
            if (exitUser != null)
            {
                await AddOrRemove(exitUser);
                return;
            }

            var loading = await App.MainWin.ShowProgressAsync("请稍后", "查询中...");
            loading.SetIndeterminate();

            try
            {
                var manager = new BilibiliApiManager();
                var user = await manager.GetUserName(App.MainWin.MainVm.TempUserName);
                if (user == null)
                {
                    await App.MainWin.ShowMessageAsync("此人不存在", $"未找到：{App.MainWin.MainVm.TempUserName}", MessageDialogStyle.Affirmative);
                    return;
                }
                App.MainWin.MainVm.Config.BilibiliUsers = manager.MergeUsers(App.MainWin.MainVm.Config.BilibiliUsers, new List<BilibiliUserVm> { user }, false);
                await AddOrRemove(App.MainWin.MainVm.Config.BilibiliUsers.First(m => m.Uid == user.Uid));
            }
            catch (Exception ex)
            {
                await App.MainWin.ShowMessageAsync("增加路人出错", ex.Message, MessageDialogStyle.Affirmative);
            }
            finally
            {
                await loading.CloseAsync();
            }
        }

        private async void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            var confirm = await App.MainWin.ShowMessageAsync("您确认吗？", "正在清空队列", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings
            {
                AffirmativeButtonText = "清空",
                NegativeButtonText = "取消"
            });

            if (confirm.ToString() == "Negative")
            {
                return;
            }

            App.MainWin.MainVm.Config.BilibiliUsers.ForEach(m => m.IsInQueue = false);
            await App.MainWin.MainVm.UpdateSearchAsync();
        }
    }
}
