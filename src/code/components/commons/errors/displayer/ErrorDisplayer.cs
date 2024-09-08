using Godot;
using SpaceMiner.src.code.components.user.ui.components.boards.dialogs.error_dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.displayer
{
    public static class ErrorDisplayer
    {
        public static ErrorHolder ErrorHolder { get; private set; }
        private static readonly IErrorDialogBoxDisplayer BasicDisplayer = new ErrorDialogBoxBasicDisplayer();
        public static void Init(SceneTree tree)
        {
            ErrorHolder = tree.Root.GetNode<ErrorHolder>(ErrorHolder.ErrorHolderNodeName);
            BasicDisplayer.ErrorHolder = ErrorHolder;
        }
        public static IErrorDialogBox Display(string message, string type)
        {
            return BasicDisplayer.Display(message, type);
        }
    }
}
