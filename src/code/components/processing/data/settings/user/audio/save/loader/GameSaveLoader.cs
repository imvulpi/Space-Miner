using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.settings.game;
using System;
using System.IO;
using System.Threading;

namespace SpaceMiner.src.code.components.processing.data.game.save
{
    public class GameSaveLoader : IGameSaveLoader
    {
        public GameSaveLoader(GameSaveSettings settings) {
            GameSaveSettings = settings;
            GameSaveSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, GameSaveSettings.SaveName);
        }
        private bool AlreadyLoaded = false;
        public GameSaveSettings GameSaveSettings { get; set; }
        public void Load(SceneTree sceneTree)
        {
            GameSaveSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, GameSaveSettings.SaveName);
            PackedScene gameScene = ResourceLoader.Load<PackedScene>(InternalPaths.GAME_SCENE);

            if (new SceneManagerHelper().FindSceneManager(sceneTree.Root) is SceneManager sm && !AlreadyLoaded)
            {
                Node sceneNode = sm.SwitchScene(gameScene);
                GameController gameController = sceneNode as GameController;
                gameController.GameSettings = GameSaveSettings;
                gameController.Initialize();
            }
            else
            {
                throw new GameException(PrettyErrorType.Critical, "No SceneManager in scene", "There should always be a scene manager in a scene");
            }
        }
    }
}
