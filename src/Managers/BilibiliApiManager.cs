using NepPure.Bilibili.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NepPure.Bilibili.Managers
{
    public class BilibiliApiManager
    {
        public async Task<List<BilibiliUserVm>> GetLiveTopListAsync(int roomid, int uid)
        {
            var res = new List<BilibiliUserVm>();
            using (var client = new HttpClient())
            {
                var urlformat = $"https://api.live.bilibili.com/xlive/app-room/v1/guardTab/topList?roomid={roomid}&page={{0}}&ruid={uid}&page_size={{1}}";
                var curPage = 1;
                var totalPage = 1;
                JObject result;
                do
                {
                    var url = GetUrl(urlformat, curPage, 20);
                    var r = await client.GetStringAsync(url);
                    result = JsonConvert.DeserializeObject<JObject>(r);
                    if (result["code"].ToString() != "0")
                    {
                        throw new Exception($"接口请求出错:{result["message"].ToString()}");
                    }

                    totalPage = Convert.ToInt32(result["data"]["info"]["page"].ToString());
                    res.AddRange(result["data"]["list"].ToObject<List<BilibiliUserVm>>());
                    //await Task.Delay(500);
                    curPage++;
                } while (curPage <= totalPage);

                res.InsertRange(0, result["data"]["top3"].ToObject<List<BilibiliUserVm>>());
                return res;
            }
        }

        private string GetUrl(string urlformat, int page, int pageSize)
        {
            return string.Format(urlformat, page, pageSize);
        }

        public List<BilibiliUserVm> MergeUsers(List<BilibiliUserVm> inputOld, List<BilibiliUserVm> newList)
        {
            var oldList = JsonConvert.DeserializeObject<List<BilibiliUserVm>>(JsonConvert.SerializeObject(inputOld));
            oldList.ForEach(m => m.Guard_level = 0);

            foreach (var u in newList)
            {
                if (oldList.Where(m => m.Uid == u.Uid).Any())
                {
                    // 列表中存在
                    var o = oldList.Single(m => m.Uid == u.Uid);
                    UpdateUser(ref o, u);
                }
                else
                {
                    // 列表中不存在
                    oldList.Add(u);
                }
            }
            var no = 1;
            oldList.ForEach(m => m.No = no++);

            return oldList;
        }

        private void UpdateUser(ref BilibiliUserVm user, BilibiliUserVm info)
        {
            user.Face = info.Face;
            user.Guard_level = info.Guard_level;
            user.Is_alive = info.Is_alive;
            user.UserName = info.UserName;
        }
    }
}
