using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
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
        public ISettings Load(ISettings setting)
        {
            setting.Path ??= TryConstructingPath(setting);

            if (setting.Path != null)
            {
                string jsonText = File.ReadAllText(Path.Join(setting.Path, ExternalPaths.SAVES_SETTING));
                setting.Load(jsonText);
            }
            else
            {
                throw new GameException(PrettyErrorType.OperationFailed, "GameSettingPath", "Game settings could not be loaded, path is null. Constructing path failed!");
            }
            return setting;
        }
        public ISettings Save(ISettings setting)
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
            return setting;
        }
        private string TryConstructingPath(ISettings setting)
        {
            if (setting is IGameSettings saveReceive)
            {
                return ConstructPath(saveReceive);
            }
            else
            {
                throw new GameException(PrettyErrorType.Invalid, "GameSettingPath", "Settings path is null. Could not construct a path since the settings are not of SaveSetting type.", "Error is most likely caused by using a wrong coupler!");
            }
        }
        private string ConstructPath(IGameSettings saveSettingReceive)
        {
            if (saveSettingReceive.SaveName != "" || saveSettingReceive.SaveName != null)
            {
                Logger.Log(PrettyInfoType.Checking, "Save Directory");
                DirectoryHelper.ValidateUserDirectories(Path.Join(SavesPath, saveSettingReceive.SaveName));
                Logger.Log(PrettyInfoType.Success, "Save Directory Checked");
                return Path.Join(SavesPath, saveSettingReceive.SaveName);
            }
            else
            {
                throw new GameException(PrettyErrorType.Invalid, "Invalid GameSetting", "Game setting (SaveName) is invalid. Valid SaveName is required.");
            }
        }
    }
}
