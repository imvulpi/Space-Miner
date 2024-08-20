using Godot;
using SpaceMiner.src.code.components.commons.other.paths;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using SpaceMiner.src.code.components.processing.data.settings.user.couplers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.boot.checkers
{
    internal class UserSettingsChecker
    {
        public UserSettingsChecker() { 
            
        }

        public void Check()
        {
            SettingCoupler coupler = new();
            string userSettingPath = Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.USER_SETTING);
            if (userSettingPath != null && !File.Exists(userSettingPath))
            {
                File.Create(userSettingPath).Close();
            }
            UserSettings settings = new();
            try
            {
                coupler.Load(settings, userSettingPath);
            }
            catch (Exception ex)
            {
                GD.Print(ex);
                settings.Check();
                coupler.Save(settings, userSettingPath);
            }
        }
    }
}
