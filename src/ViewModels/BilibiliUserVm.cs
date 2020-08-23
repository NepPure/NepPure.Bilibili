using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NepPure.Bilibili.ViewModels
{
    public class BilibiliUserVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 序号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// 身份Id
        /// </summary>
        public int Ruid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Face { get; set; }

        /// <summary>
        /// 0当前未观看直播
        /// 1当前在观看直播
        /// </summary>
        public int Is_alive { get; set; }

        [JsonIgnore]
        public bool IsAlive => Is_alive > 0 ? true : false;

        [JsonIgnore]
        public string AliveStateLabel
        {
            get
            {
                if (Guard_level == 0)
                {
                    return "非舰长";
                }

                if (IsAlive)
                {
                    return "在看";
                }
                else
                {
                    return "否";
                }
            }
        }

        /// <summary>
        /// 1总督
        /// 2提督
        /// 3舰长
        /// </summary>
        public int Guard_level { get; set; }

        [JsonIgnore]
        public string GuardLevelLabel
        {
            get
            {
                switch (Guard_level)
                {
                    case 1:
                        return "总督";
                    case 2:
                        return "提督";
                    case 3:
                        return "舰长";
                    default:
                        return "大概下船了";
                }
            }
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 在队列里
        /// </summary>
        public bool IsInQueue { get; set; }

        /// <summary>
        /// 队列移动按钮名字
        /// </summary>
        [JsonIgnore]
        public string QueueLabel => IsInQueue ? "移出" : "加入";

        /// <summary>
        /// 排队顺序
        /// </summary>
        public int QueueNo { get; set; }

        /// <summary>
        /// 加入队列时间
        /// </summary>
        public DateTime InQueueTime { get; set; }
    }
}
