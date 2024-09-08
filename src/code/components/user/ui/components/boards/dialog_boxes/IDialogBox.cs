using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.ui.components.boards.dialog_boxes
{
    public interface IDialogBox
    {
        string Title { get; set; }
        string Message { get; set; }
    }
}
