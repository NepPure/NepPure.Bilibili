﻿using NepPure.Bilibili.ViewModels;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.MainWin.MainVm.UpdateSearch();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button btn))
            {
                return;
            }

            var v = btn.Tag as BilibiliUserVm;
            v.IsInQueue = !v.IsInQueue;
            v.InQueueTime = DateTime.Now;
            App.MainWin.MainVm.UpdateSearch();
            var no = 1;
            foreach (var q in App.MainWin.MainVm.InQueueList)
            {
                q.QueueNo = no++;
            }
            App.MainWin.MainVm.UpdateSearch();
        }
    }
}