using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.settings.game;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.game.save
{
    public class GameSaveManager : IGameSaveManager
    {
        public GameSaveManager(GameSaveSettings settings) {
            GameSaveSettings = settings;
            GameSaveSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, GameSaveSettings.SaveName);
        }

        public GameSaveSettings GameSaveSettings { get; set; }
        public void Load(SceneTree sceneTree)
        {
            GameSaveSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, GameSaveSettings.SaveName);
            if (Directory.Exists(GameSaveSettings.Path))
            {
                string savePath = Path.Join(GameSaveSettings.Path, ExternalPaths.SAVE_FILE);
                PackedScene gameScene = ResourceLoader.Load<PackedScene>(savePath);
                Error error = sceneTree.ChangeSceneToPacked(gameScene);
                if (error != Error.Ok)
                {
                    throw new GameException(PrettyErrorType.Critical, $"{error}", $"Loading a save failed due to an engine error: {error}");
                }
            }
            else
            {
                throw new GameException(PrettyErrorType.ResourceNotFound, $"{GameSaveSettings.Path}", "Save directory not found, could not load!");
            }
        }

        public void Save(Node gameNode)
        {   
            GameSaveSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, GameSaveSettings.SaveName);
            if (Directory.Exists(GameSaveSettings.Path))
            {
                string savePath = Path.Join(GameSaveSettings.Path, ExternalPaths.SAVE_FILE);
                PackedScene packedScene = new PackedScene();
                Error packError = packedScene.Pack(gameNode);
                if (packError == Error.Ok)
                {
                    ResourceSaver.Save(packedScene, savePath);
                }
                else
                {
                    throw new GameException(PrettyErrorType.OperationFailed, $"{gameNode.Name}/{packError}", $"Could not save because packing failed");
                }
            }
            else
            {
                throw new GameException(PrettyErrorType.ResourceNotFound, $"{GameSaveSettings.Path}", "Save directory not found, could not load!");
            }
        }
    }
}
