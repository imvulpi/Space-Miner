using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.logging;
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
            PrettyLogger.Log(PrettyInfoType.Checking, $"{directoryPath}");
            string[] dirs = DirectoryHelper.GetParentDirectories(directoryPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                string currentPath = dirs[i];
                if (!Directory.Exists(currentPath))
                {
                    PrettyLogger.Log(PrettyWarningType.NotFound, $"{currentPath}", "Essential directory does not exist.");
                    try
                    {
                        PrettyLogger.Log(PrettyInfoType.RepairAttempt, $"{currentPath}", $"Directory Repair Attempt");
                        Directory.CreateDirectory(currentPath);
                        PrettyLogger.Log(PrettyInfoType.Success, $"{currentPath}", $"Directory repaired!");
                    }
                    catch (Exception ex)
                    {
                        PrettyLogger.Log(PrettyErrorType.GeneralError, $"{ex.Message}");
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
