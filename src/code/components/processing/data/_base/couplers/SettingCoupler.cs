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
        public ISettings Load(ISettings setting)
        {
            string jsonText = File.ReadAllText(setting.Path);
            setting.Load(jsonText);
            return setting;
        }

        public ISettings Load(ISettings setting, string path)
        {
            string jsonText = File.ReadAllText(path);
            setting.Load(jsonText);
            return setting;
        }

        public ISettings Save(ISettings setting)
        {
            string jsonText = setting.GetData();
            File.WriteAllText(setting.Path, jsonText);
            return setting;
        }

        public void Save(ISettings setting, string path)
        {
            string jsonText = setting.GetData();
            File.WriteAllText(path, jsonText);
        }
    }
}
