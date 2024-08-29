using Godot;
using SpaceMiner.src.code.components.user.ui.components.other.game_save_item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.ui.components.other
{
    public partial class GameSaveItem : Control, IGameSaveItem
    {
        public event Action<object> DeleteSaveEvent;
        public event Action<object> LoadSaveEvent;
        [Export] public Label NameLabel { get; set; }
        [Export] public Label PathLabel { get; set; }
        public string FullPath { get; set; }
        [Export] public BaseButton DeleteButton { get; set; }
        [Export] public BaseButton LoadButton { get; set; }
        public override void _Ready()
        {
            DeleteButton.Pressed += DeleteButton_Pressed;
            LoadButton.Pressed += LoadButton_Pressed;
        }

        private void LoadButton_Pressed()
        {
            LoadSaveEvent?.Invoke(this);
        }

        private void DeleteButton_Pressed()
        {
            DeleteSaveEvent?.Invoke(this);
        }
    }
}
