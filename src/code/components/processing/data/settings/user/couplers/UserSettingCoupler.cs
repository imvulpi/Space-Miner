using SpaceMiner.src.code.components.commons.other.paths;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.settings.user.couplers
{
    public class UserSettingCoupler : ISettingCoupler
    {
        public UserSettingCoupler() { 
            
        }
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
