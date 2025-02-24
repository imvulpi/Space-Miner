using Godot;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.settings.user.couplers
{
    public class UserSettingCoupler : ISettingCoupler
    {
        public UserSettingCoupler() { 
            
        }
        public ISettings Load(ISettings setting)
        {
            setting.Path ??= Path.Join(OS.GetUserDataDir(), ExternalPaths.USER_SETTING);
            string jsonText = File.ReadAllText(setting.Path);
            setting.Load(jsonText);
            return setting;
        }
        public ISettings Save(ISettings setting)
        {
            setting.Path ??= Path.Join(OS.GetUserDataDir(), ExternalPaths.USER_SETTING);
            string jsonText = setting.GetData();
            File.WriteAllText(setting.Path, jsonText);
            return setting;
        }
    }
}
