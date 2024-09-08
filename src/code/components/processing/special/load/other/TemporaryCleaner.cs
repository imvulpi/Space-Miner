using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.special.load.other
{
    /// <summary>
    /// Cleans the whole temporary directory.
    /// </summary>
    public class TemporaryCleaner
    {
        /// <summary>
        /// Clears the temporary folder.
        /// </summary>
        public static void ClearTemp()
        {
            string tempPath = Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.TEMP_DIR);
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath);
                Directory.CreateDirectory(tempPath);
            }
        }
    }
}
