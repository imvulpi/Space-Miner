using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using SpaceMiner.src.code.components.processing.special.load.checkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.game.couplers
{
    internal class GameSettingCoupler : ISettingCoupler
    {
        private readonly string SavesPath = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR);
        public void Load(ISetting setting)
        {
            setting.Path ??= TryConstructingPath(setting);

            if (setting.Path != null)
            {
                string jsonText = File.ReadAllText(Path.Join(setting.Path, ExternalPaths.SAVES_SETTING));
                setting.Load(jsonText);
            }
            else
            {
                throw new Exception("Game settings could not be loaded. Constructing Path failed, path could not be determined (null).");
            }
        }
        public void Save(ISetting setting)
        {
            setting.Path ??= TryConstructingPath(setting);

            if (setting.Path != null)
            {
                string json = setting.GetData();
                File.WriteAllText(Path.Join(setting.Path, ExternalPaths.SAVES_SETTING), json);
            }
            else
            {
                throw new Exception("Game settings could not be saved. Constructing Path failed, path could not be determined (null).");
            }
        }
        private string TryConstructingPath(ISetting setting)
        {
            if (setting.Path == null && setting is IGameSettingReceive saveReceive)
            {
                
                return ConstructPath(saveReceive);
            }
            else
            {
                return ConstructingPathFail(setting);
            }
        }
        private string ConstructPath(IGameSettingReceive saveSettingReceive)
        {
            if (saveSettingReceive.SaveName != "" || saveSettingReceive.SaveName != null)
            {
                GD.Print(new PrettyInfo(PrettyInfoType.Checking, "Save Directory"));
                if (DirectoryHelper.ValidateUserDirectories(Path.Join(SavesPath, saveSettingReceive.SaveName)))
                {
                    GD.Print(new PrettyInfo(PrettyInfoType.Success, "Save Directory Checked"));
                    return Path.Join(SavesPath, saveSettingReceive.SaveName);
                }
                else
                {
                    GD.PushError(new PrettyError(PrettyErrorType.OperationFailed, "Save directory could not be corrected"));
                    return null;
                }
            }
            else
            {
                GD.PushError(new PrettyError(PrettyErrorType.Invalid, "GameSetting", "Invalid game setting SaveName value. Could not construct a path since SaveName is required"));
                return null;
            }
        }
        private string ConstructingPathFail(ISetting setting)
        {
            if (setting.Path == null)
            {
                GD.PushError(new PrettyError(PrettyErrorType.Invalid, "SettingsPath", "Settings path is null. Could not construct a path since the settings are not of SaveSetting type."));
                return null;
            }
            else
            {
                GD.PushError(new PrettyError(PrettyErrorType.Unhandled, "SettingsPath", "Settings path is not null since why it should be handled already."));
                return null;
            }
        }
    }
}
