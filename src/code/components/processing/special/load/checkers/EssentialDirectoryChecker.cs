using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using System;
using System.IO;

namespace SpaceMiner.src.code.components.processing.special.load.checkers
{
    public class EssentialDirectoryChecker
    {
        public EssentialDirectoryChecker() { }

        public void Check()
        {
            string userPath = OS.GetUserDataDir();
            CheckDirectory(userPath);

            string savesDirectory = Path.Combine(userPath, ExternalPaths.SAVES_DIR);
            CheckDirectory(savesDirectory);
        }

        public bool CheckDirectory(string directoryPath)
        {
            GD.Print(new PrettyInfo(PrettyInfoType.Checking, $"{directoryPath}"));
            string[] dirs = DirectoryHelper.GetParentDirectories(directoryPath);
            string continuousPath = "";
            for (int i = 0; i < dirs.Length; i++)
            {
                string currentPath = dirs[i];
                continuousPath = Path.Join(continuousPath, currentPath);
                if (!Directory.Exists(continuousPath))
                {
                    GD.PushWarning(new PrettyWarning(PrettyWarningType.NotFound, $"{continuousPath}", "Essential directory does not exist."));
                    try
                    {
                        GD.Print(new PrettyInfo(PrettyInfoType.RepairAttempt, $"{continuousPath}", $"Directory Repair Attempt"));
                        Directory.CreateDirectory(continuousPath);
                        GD.Print(new PrettyInfo(PrettyInfoType.Successful, $"{continuousPath}", $"Directory repaired!"));
                    }
                    catch (Exception ex)
                    {
                        GD.PushError(new PrettyError(PrettyErrorType.Error, $"{ex.Message}"));
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
