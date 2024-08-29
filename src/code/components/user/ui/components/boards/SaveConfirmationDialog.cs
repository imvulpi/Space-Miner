using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.ui.components.boards
{
    public partial class SaveConfirmationDialog : Control, IConfirmationDialog
    {
        public event Action<bool> Decision;
        [Export] public Button ConfirmButton { get; set; }
        [Export] public Button CancelButton { get; set; }
        [Export] public Label Label { get; set; }
        [Export] public string SaveNamePlaceholder = "<{SaveName}>";
        [Export] public string LabelText = "Are you sure you want to load <{SaveName}>";
        public string SaveName { get; set; }
        public override void _Ready()
        {
            ConfirmButton.Pressed += ConfirmButton_Pressed;
            CancelButton.Pressed += CancelButton_Pressed;
            Label.Text = LabelText.Replace("<{SaveName}>", SaveName);
        }

        private void CancelButton_Pressed()
        {
            Decision?.Invoke(false);
        }

        private void ConfirmButton_Pressed()
        {
            Decision?.Invoke(true);
        }
    }
}
