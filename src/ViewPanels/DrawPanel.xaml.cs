using MahApps.Metro.Controls.Dialogs;
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
    /// DrawPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DrawPanel : UserControl
    {
        public DrawPanel()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loading = await App.MainWin.ShowProgressAsync("请稍后", "抽奖中...");
            loading.SetIndeterminate();
            try
            {
                var drawCount = App.MainWin.MainVm.DrawCount;
                if (drawCount <= 0)
                {
                    throw new Exception("迷之抽奖人数");
                }

                var guards = App.MainWin.MainVm.Config.BilibiliUsers.Where(m => m.Guard_level > 0).ToList();

                if (drawCount > guards.Count)
                {
                    throw new Exception("你没有这么多舰长");
                }
                var random = new Random();
                var result = new HashSet<BilibiliUserVm>();

                while (result.Count < drawCount)
                {
                    var r = random.Next(0, guards.Count);
                    result.Add(guards[r]);
                }

                App.MainWin.MainVm.DrawedList = result;
            }
            catch (Exception ex)
            {
                await App.MainWin.ShowMessageAsync("抽奖提示", ex.Message, MessageDialogStyle.Affirmative);
            }
            finally
            {
                await loading.CloseAsync();
            }

        }
    }
}
