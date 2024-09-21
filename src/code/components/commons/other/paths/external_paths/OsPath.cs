using Godot;
using System.IO;
using System.Linq;

namespace SpaceMiner.src.code.components.commons.other.paths.external_paths
{
    public static class OsPath
    {
        /// <summary>
        /// Joins given strings to a User Data Directory in an OS (ex. AppData on Windows)
        /// </summary>
        /// <param name="paths">strings to be added to the path.</param>
        /// <returns>Combined paths to a user data directory</returns>
        public static string Join(params string[] paths)
        {
            paths = (string[])paths.Prepend(OS.GetUserDataDir());
            return Path.Join(paths);
        }
    }
}
