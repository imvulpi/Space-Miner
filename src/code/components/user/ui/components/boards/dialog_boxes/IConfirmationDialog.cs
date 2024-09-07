using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.ui.components.boards.dialog_boxes
{
    public interface IConfirmationDialog
    {
        /// <summary>
        /// Where:<br></br>true -> confirmed (OK),<br></br>false -> canceled (CANCEL),
        /// </summary>
        public event Action<bool> Decision;
    }
}
