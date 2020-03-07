using NepPure.Bilibili.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepPure.Bilibili.Managers
{
    /// <summary>
    /// 配置文件服务
    /// </summary>
    public static class ConfigurationManager
    {
        private const string _configPath = "config.json";

        public static void Save(ConfigVm config)
        {
            var configContent = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(_configPath, configContent);
        }

        public static ConfigVm Read()
        {
            if (!File.Exists(_configPath))
            {
                return new ConfigVm();
            }

            var configContent = File.ReadAllText(_configPath);
            return JsonConvert.DeserializeObject<ConfigVm>(configContent);
        }
    }
}
