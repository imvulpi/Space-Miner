using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.ui.components.boards
{
    public interface IConfirmationDialog
    {
        /// <summary>
        /// Where:<br></br>true -> confirmed,<br></br>false -> canceled,
        /// </summary>
        public event Action<bool> Decision;
    }
}
