using NepPure.Bilibili.Managers;
using Newtonsoft.Json;
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

        public IEnumerable<BilibiliUserVm> InQueueList { get; set; }

        public string SearchKey { get; set; }

        public string TempUserName { get; set; }

        public IEnumerable<BilibiliUserVm> SearchedList { get; set; }

        public Task UpdateSearchAsync()
        {
            InQueueList = Config.BilibiliUsers.Where(m => m.IsInQueue).OrderBy(m => m.QueueNo);

            if (string.IsNullOrWhiteSpace(SearchKey))
            {
                SearchedList = Config.BilibiliUsers;
            }
            else
            {
                SearchedList = Config.BilibiliUsers.Where(m => m.UserName.Contains(SearchKey) || m.Uid.ToString().Contains(SearchKey));
            }

            WebsocketServer.Broadcast(JsonConvert.SerializeObject(InQueueList));
            return Task.CompletedTask;
        }
    }
}
