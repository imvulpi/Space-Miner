using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using System;
using System.IO;
using System.Threading;

namespace SpaceMiner.src.code.components.processing.special.load.checkers
{
    public class UserSettingChecker
    {
        public UserSettingChecker()
        {
            
        }

        private readonly string UserSettingPath = Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.USER_SETTING);
        private const int MAX_RETRY = 3;
        public void Check()
        {
            GD.Print(new PrettyInfo(PrettyInfoType.Checking, $"{ExternalPaths.USER_SETTING}"));
            UserSettings settings = new();
            SettingCoupler coupler = new();
            CheckFileExistance();
            for(int i = 0; i < MAX_RETRY; i++)
            {
                (bool loaded, Exception ex) = CheckSettingLoad(settings, coupler);
                if (loaded)
                {
                    GD.Print(new PrettyInfo(PrettyInfoType.Loaded, $"{ExternalPaths.USER_SETTING}"));
                    break;
                }
                else
                {
                    GD.Print(new PrettyInfo(PrettyInfoType.Retry, $"{ExternalPaths.USER_SETTING}", $"User Settings repair and load retry ({i+1}/{MAX_RETRY})"));
                    Thread.Sleep(100);
                    TryRepairSettingLoad(settings, coupler);
                }

                if(i+1 >= MAX_RETRY)
                {
                    (loaded, ex) = CheckSettingLoad(settings, coupler);
                    if (loaded)
                    {
                        GD.Print(new PrettyInfo(PrettyInfoType.Loaded, $"{ExternalPaths.USER_SETTING}"));
                    }
                    else
                    {
                        GD.PushError(new PrettyError(PrettyErrorType.OperationFailed, $"{ex.Message}", $"Repairing {ExternalPaths.USER_SETTING} failed, loading was unsuccesful"));
                    }
                }
            }
        }

        private void CheckFileExistance()
        {
            if (UserSettingPath != null && !File.Exists(UserSettingPath))
            {
                GD.PushWarning(new PrettyWarning(PrettyWarningType.NotFound, $"{ExternalPaths.USER_SETTING}"));
                try
                {
                    File.Create(UserSettingPath).Close();
                    GD.Print(new PrettyInfo(PrettyInfoType.Created, $"{ExternalPaths.USER_SETTING}", "Succesfully created"));
                }
                catch (Exception ex)
                {
                    GD.PushError(new PrettyError(PrettyErrorType.OperationFailed, $"{ExternalPaths.USER_SETTING}", $"Creation failed, message: {ex.Message}"));
                }
            }
        }

        private (bool loaded, Exception exception) CheckSettingLoad(UserSettings settings, SettingCoupler coupler)
        {
            try
            {
                coupler.Load(settings, UserSettingPath);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        private void TryRepairSettingLoad(UserSettings settings, SettingCoupler coupler)
        {
            coupler.Save(settings, UserSettingPath);
        }
    }
}
