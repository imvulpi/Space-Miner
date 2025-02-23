using Godot;
using SpaceMiner.src.code.components.processing.data.settings.game;

namespace SpaceMiner.src.code.components.processing.data.game.save
{
    public interface IGameSaveLoader
    {
        public GameSaveSettings GameSaveSettings { get; set; }
        void Load(SceneTree sceneTree);
    }
}
