using GruInject.API.Attributes;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.couplers
{
    [RegisterSingleton]
    public class SettingCoupler : ISettingCoupler
    {
        public void Load(ISetting setting)
        {
            string jsonText = File.ReadAllText(setting.Path);
            setting.Load(jsonText);
        }

        public void Load(ISetting setting, string path)
        {
            string jsonText = File.ReadAllText(path);
            setting.Load(jsonText);
        }

        public void Save(ISetting setting)
        {
            string jsonText = setting.GetData();
            File.WriteAllText(setting.Path, jsonText);
        }

        public void Save(ISetting setting, string path)
        {
            string jsonText = setting.GetData();
            File.WriteAllText(path, jsonText);
        }
    }
}
