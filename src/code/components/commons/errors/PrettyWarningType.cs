using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors
{
    public enum PrettyWarningType
    {
        /// <summary>
        /// Generic warning for everything
        /// </summary>
        GeneralWarning,
        /// <summary>
        /// Signifies that some state/resource/input was corrected, some data might have been lost.
        /// </summary>
        Corrected,
        /// <summary>
        /// Signifies that some resource could not be found.<br></br>It missing should not break the game. (Not an error)
        /// </summary>
        NotFound
    }
}
