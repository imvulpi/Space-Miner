using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SpaceMiner.src.code.components.processing.special.load.checkers
{
    public class EssentialDirectoryChecker
    {
        public EssentialDirectoryChecker() { }
        private string UserPath = OS.GetUserDataDir();
        private List<string> PathsToCheck;
        public void Check()
        {
            string userPath = OS.GetUserDataDir();

            PathsToCheck = new List<string>()
            {
                userPath,
                Path.Combine(userPath, ExternalPaths.SAVES_DIR),
                Path.Combine(userPath, ExternalPaths.LOGS_DIR),
                Path.Combine(userPath, ExternalPaths.LOGS_DIR, ExternalPaths.REPORTS_DIR),
                Path.Combine(userPath, ExternalPaths.TEMP_DIR),
            };
            
            foreach(string path in PathsToCheck) {
                CheckDirectory(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        /// <exception cref="GameException"></exception>
        public bool CheckDirectory(string directoryPath)
        {
            Logger.Log(PrettyInfoType.Checking, $"{directoryPath}");
            string[] dirs = DirectoryHelper.GetParentDirectories(directoryPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                string currentPath = dirs[i];
                if (!Directory.Exists(currentPath))
                {
                    Logger.Log(PrettyWarningType.NotFound, $"{currentPath}", "Essential directory does not exist.");
                    try
                    {
                        Logger.Log(PrettyInfoType.RepairAttempt, $"{currentPath}", $"Directory Repair Attempt");
                        Directory.CreateDirectory(currentPath);
                        Logger.Log(PrettyInfoType.Success, $"{currentPath}", $"Directory repaired!");
                    }
                    catch (Exception ex)
                    {
                        throw new GameException(PrettyErrorType.OperationFailed, $"Directory Creation", $"Creating directory at: {currentPath} failed due to an exception: {ex.Message}");
                    }
                }
            }
            return true;
        }

    }
}
