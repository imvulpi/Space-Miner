using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
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
                    GD.Print(settings.SaveName);
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
            Logger.Log(PrettyInfoType.GeneralInfo, $"{saveName} removal", $"Removing save.");
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR);
            string savePath = Path.Join(path, saveName);
            if (Directory.Exists(savePath))
            {
                Directory.Delete(savePath, true);
                Logger.Log(PrettyInfoType.Success, $"{saveName} removal", "Successfully removed");
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
            string save_dir_path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR);
            if (!Directory.Exists(save_dir_path))
            {
                Directory.CreateDirectory(save_dir_path);
            }

            string path = Path.Join(save_dir_path, settings.SaveName);
            if(!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        }
    }
}
