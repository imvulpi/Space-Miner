using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
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
            PrettyLogger.Log(PrettyInfoType.GeneralInfo, $"{saveName} removal", $"Removing save.");
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR);
            string savePath = Path.Join(path, saveName);
            if (Directory.Exists(savePath))
            {
                Directory.Delete(savePath, true);
                PrettyLogger.Log(PrettyInfoType.Success, $"{saveName} removal", "Successfully removed");
            }
            else
            {
                throw new GameException(PrettyErrorType.OperationFailed, $"{saveName} removal", $"The save could not be removed, because {savePath} directory doesn't exist");
            }
        }

        /// <summary>
        /// This function creates the game scene
        /// </summary>
        /// <param name="settings">NOTE: Must contain SaveName</param>
        public void CreateSave(GameSaveSettings settings)
        {
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, settings.SaveName);
            DirectoryHelper.ValidateDirectory(path);
            PackedScene gameScene = GetGameScene(settings);
            ResourceSaver.Save(gameScene, Path.Join(path, ExternalPaths.SAVE_FILE));
        }

        private PackedScene GetGameScene(GameSaveSettings settings)
        {
            if (Godot.FileAccess.FileExists(InternalPaths.GAME_SCENE))
            {
                string gameSceneString = Godot.FileAccess.GetFileAsString(InternalPaths.GAME_SCENE);
                string destPath = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, settings.SaveName);
                DirectoryHelper.ValidateDirectory(destPath);
                if (gameSceneString != "" && Godot.FileAccess.GetOpenError() == Error.Ok)
                {

                    File.WriteAllText(Path.Join(destPath, "game.tscn"), gameSceneString);
                    return ResourceLoader.Load<PackedScene>(InternalPaths.GAME_SCENE);
                }
                else if(gameSceneString == "")
                {
                    // Godot error
                    Error fileAccessError = Godot.FileAccess.GetOpenError();
                    throw new GameException(PrettyErrorType.GeneralError, $"{InternalPaths.GAME_SCENE}/{fileAccessError}", "Could not get the contents of the game scene.");
                }
                else
                {
                    // Very rare, but could happen when data is corrupted, for example when freeing objects and deffering a call the data could become corrupted in some cases.
                    throw new GameException(PrettyErrorType.Critical, $"{InternalPaths.GAME_SCENE}", "The game scene could not be retrieved. (Report this)");
                }
            }
            else
            {
                throw new GameException(PrettyErrorType.ResourceNotFound, $"{InternalPaths.GAME_SCENE}", "Game scene is missing from the project. (Report this)");
            }
        }
    }
}
