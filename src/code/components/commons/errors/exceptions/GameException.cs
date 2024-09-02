using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.exceptions
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class GameException : Exception
    {
        GameException() : base() { }
        GameException(string message) : base(message) { }
        GameException(string message, Exception inner) : base(message, inner) { }
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
