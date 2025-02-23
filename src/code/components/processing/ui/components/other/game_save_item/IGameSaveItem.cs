using Godot;
using System;
using System.Runtime.CompilerServices;


namespace SpaceMiner.src.code.components.user.ui.components.other.game_save_item
{
    public interface IGameSaveItem
    {
        public event Action<object> DeleteSaveEvent;
        public event Action<object> LoadSaveEvent;
        public Label NameLabel { get; set; }
        public Label PathLabel { get; set; }
        public string FullPath { get; set; }
        public BaseButton DeleteButton { get; set; }
    }
}
