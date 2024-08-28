using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.settings.game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.save
{
    public class GameSaveHelper
    {
        public GameSaveHelper() { 
        
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

        public void NewGame(GameSaveSettings settings, Node connectNode)
        {
            string path = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, settings.SaveName);
            if (!DirectoryHelper.ValidateDirectory(path, true))
            {
                GD.PushError(new PrettyError(PrettyErrorType.Critical, $"{path}", "Directory validation failed, can't continue with the game creation."));
            }

            Node gameNode = GetEmptyWorld(settings);

            if (gameNode != null)
            {
                connectNode.AddChild(gameNode);
            }
        }

        public Node GetEmptyWorld(GameSaveSettings settings)
        {
            switch (settings.WorldType)
            {
                case WorldType.Prebuild:
                    return SetPremade(settings);
                case WorldType.Generated:
                    throw new NotImplementedException();
                default:
                    break;
            }
            return null;
        }

        private Node SetPremade(GameSaveSettings settings)
        {
            if (Godot.FileAccess.FileExists(InternalPaths.PREMADE_SCENE))
            {
                string premadeSceneString = Godot.FileAccess.GetFileAsString(InternalPaths.PREMADE_SCENE);
                string destPath = Path.Join(OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, settings.SaveName);
                if (DirectoryHelper.ValidateDirectory(destPath))
                {
                    File.WriteAllText(Path.Join(destPath, "game.tscn"), premadeSceneString);
                    return ResourceLoader.Load<PackedScene>(InternalPaths.PREMADE_SCENE).Instantiate();
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
