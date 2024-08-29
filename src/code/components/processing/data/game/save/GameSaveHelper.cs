using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.settings.game.couplers;
using System;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.game.save
{
    public class GameSaveHelper
    {
        public GameSaveHelper() {}

        public string[] GetAllSavesDirectories()
        {
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR);
            return Directory.GetDirectories(path);
        }

        public string GetLatestSaveName()
        {
            string[] paths = GetAllSavesDirectories();
            DateTime latest = new(0);
            string latestSaveName = ""; 
            foreach (string path in paths) { 
                string settingPath = Path.Combine(path, ExternalPaths.SAVES_SETTING);
                if (File.Exists(settingPath))
                {
                    GameSaveSettings settings = new GameSaveSettings()
                    {
                        SaveName = path.Split(new char[] { '\\', '/' })[^1]
                    };
                    GameSettingCoupler coupler = new GameSettingCoupler();
                    coupler.Load(settings);
                    if(settings.LastPlayed.CompareTo(latest) > 0)
                    {
                        latest = settings.LastPlayed;
                        latestSaveName = settings.SaveName;
                    }
                }
            }
            return latestSaveName;
        }

        public bool CheckSaveNameAvailability(string name)
        {
            if (!name.IsValidFileName())
            {
               return false;
            }
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, name);
            if (Directory.Exists(path))
            {
                return false;
            }
            return true;
        }

        public void DeleteSave(string saveName)
        {
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR);
            string savePath = Path.Join(path, saveName);
            if (Directory.Exists(savePath))
            {
                Directory.Delete(savePath, true);
            }
            else
            {
                GD.PushError(new PrettyError(PrettyErrorType.Failed, $"{saveName} Removal", "Could not remove the save, because it does not exist"));
            }
        }

        public void CreateSave(GameSaveSettings settings, Node connectNode)
        {
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, settings.SaveName);
            if (!DirectoryHelper.ValidateDirectory(path, false))
            {
                GD.PushError(new PrettyError(PrettyErrorType.Critical, $"{path}", "Directory validation failed, can't continue with the game creation."));
            }

            Node gameNode = SetGameScene(settings);

            if (gameNode != null)
            {
                connectNode.AddChild(gameNode);
            }
        }

        private Node SetGameScene(GameSaveSettings settings)
        {
            if (Godot.FileAccess.FileExists(InternalPaths.GAME_SCENE))
            {
                string gameSceneString = Godot.FileAccess.GetFileAsString(InternalPaths.GAME_SCENE);
                string destPath = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, settings.SaveName);
                if (DirectoryHelper.ValidateDirectory(destPath))
                {
                    File.WriteAllText(Path.Join(destPath, "game.tscn"), gameSceneString);
                    return ResourceLoader.Load<PackedScene>(InternalPaths.GAME_SCENE).Instantiate();
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
