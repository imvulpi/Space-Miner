using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.other.game_information
{
    public class GameInfo
    {
        /// <summary>
        /// Version of the chunking system, structured:<br></br>
        ///  major.minor.(patches/other)-(dev/alpha/beta/nothing)
        /// </summary>
        public const string CHUNK_SYSTEM_VERSION = "0.0.0-dev";
        public const string SERIALIZATION_VERSION_PLACEHOLDER = "SrlVersion";
        public const string BINARY_SERIALIZATION_VERSION = "0.0.0-dev";
    }
}
