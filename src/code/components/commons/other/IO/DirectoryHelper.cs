using Godot;
using System.Collections.Generic;
using System.IO;
using System;
using SpaceMiner.src.code.components.commons.errors;

namespace SpaceMiner.src.code.components.commons.other.IO
{
    public class DirectoryHelper
    {
        public DirectoryHelper() { }
        
        /// <summary>
        /// Gets all directory paths leading to a specific file/directory.<br></br> NOTE: Does not skip the last directory.
        /// </summary>
        /// <returns>Paths to directories</returns>
        public static string[] GetParentDirectories(string path)
        {
            string[] paths = path.Split(new char[] { '/',  '\\' });
            List<string> directoriesList = new List<string>();
            string continuousPath = "";
            foreach (string stringPath in paths)
            {
                continuousPath = Path.Join(continuousPath, stringPath);
                if (File.GetAttributes(continuousPath) == FileAttributes.Directory)
                {
                    directoriesList.Add(stringPath);
                }
            }
            return directoriesList.ToArray();
        }

        /// <summary>
        /// Checks or corrects directories in the user:// path. <br></br>NOTE: doesn't check validity of the user:// directory and path. |
        /// This is why it's faster than EssentialDirectoryChecker which checks all directories leading to a path.
        /// </summary>
        /// <param name="relativePath">A relative path to the godots user:// path</param>
        /// <returns>Is validated<br></br>Will return false in the first instance of unfixable error</returns>
        public static bool ValidateUserDirectories(string relativePath)
        {
            string userPath = OS.GetUserDataDir();
            string continuousPath = "";
            foreach(var path in GetParentDirectories(relativePath))
            {
                continuousPath = Path.Join(continuousPath, path);
                string fullPath = Path.Combine(userPath, continuousPath);
                if(!Directory.Exists(fullPath))
                {
                    try
                    {
                        Directory.CreateDirectory(fullPath);
                    }catch(Exception) {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Will check if a specific directory specified in a path exists.<br></br>
        /// NOTE: This will create the directory if missing.
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="shouldLog">Whether it should write logs</param>
        /// <returns>true if the directory exists or was successfully created<br></br>false if directory creation failed</returns>
        public static bool ValidateDirectory(string path, bool shouldLog = false)
        {
            if (!Directory.Exists(path))
            {
                if (shouldLog)
                {
                    GD.PushError(new PrettyError(PrettyErrorType.NotFound, $"{path}", "Directory not found"));
                    GD.Print(new PrettyInfo(PrettyInfoType.RepairAttempt, $"{path}"));
                }
                try
                {
                    Directory.CreateDirectory(path);
                    if (shouldLog)
                    {
                        GD.Print(new PrettyInfo(PrettyInfoType.Successful, $"{path} created successfully."));
                    }
                    return true;
                }
                catch(Exception ex)
                {
                    GD.PushError(new PrettyError(PrettyErrorType.Failed, $"{ex}", $"{path} Repair failed"));
                    return false;
                }
            }
            return true;
        }
    }
}
