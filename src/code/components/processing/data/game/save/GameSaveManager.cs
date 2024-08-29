using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.settings.game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    GD.PushError(new PrettyError(PrettyErrorType.Critical, $"{error}", "Can not load the game"));
                    throw new Exception();
                }
            }
            else
            {
                string errorMessage = new PrettyError(PrettyErrorType.ResourceNotFound, $"{GameSaveSettings.Path}", "Save directory not found, could not load!").ToString();
                GD.PushError(errorMessage);
                throw new Exception(errorMessage);
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
                    string errorMessage = new PrettyError(PrettyErrorType.OperationFailed, $"{gameNode.Name}/{packError}", $"Could not save because packing failed").ToString();
                    GD.PushError(errorMessage);
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                string errorMessage = new PrettyError(PrettyErrorType.ResourceNotFound, $"{GameSaveSettings.Path}", "Save directory not found, could not load!").ToString();
                GD.PushError(errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
