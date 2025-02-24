using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.game.spaceships.factory;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.user.entities.spaceships.prospector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships
{
    public class SpaceshipCoupler
    {
        public SpaceshipCoupler()
        {
            SpaceshipFactory = CreateFactory();
        }

        public ISpaceshipFactory SpaceshipFactory { get; set; }
        public void Initialize()
        {
            SpaceshipFactory ??= CreateFactory();
        }

        private SpaceshipFactory CreateFactory()
        {
            SpaceshipFactory factory = new();
            ProspectorSpaceshipFactory prospectorFactory = new();
            factory.RegisterSpaceshipFactory("spaceminer.prospector", prospectorFactory);
            return factory;
        }

        public Spaceship GetSavedSpaceship(IGameSettings gameSettings)
        {
            gameSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, gameSettings.SaveName);
            string spaceshipPath = Path.Join(gameSettings.Path, ExternalPaths.PLAYER_DATA_DIR, ExternalPaths.SPACESHIP_FILE);
            if (File.Exists(spaceshipPath))
            {
                GD.Print("Exists");
                //using FileStream fs = File.OpenRead(spaceshipPath);
                using var fs = new FileStream(spaceshipPath, FileMode.Open);

                Spaceship deserializedSpaceship = Serializer.Deserialize<Spaceship>(fs);
                GD.Print($"spaceship: {deserializedSpaceship}");
                if (deserializedSpaceship != null)
                {
                    Spaceship recreatedSpaceship = SpaceshipFactory.GetSpaceship(deserializedSpaceship);
                    return recreatedSpaceship;
                }
            }
            return null;
        }

        public void SaveSpaceship(IGameSettings gameSettings, Spaceship spaceship)
        {
            gameSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, gameSettings.SaveName);
            string playerDataDir = Path.Join(gameSettings.Path, ExternalPaths.PLAYER_DATA_DIR);
            string spaceshipPath = Path.Join(playerDataDir, ExternalPaths.SPACESHIP_FILE);
            Directory.CreateDirectory(playerDataDir);
            if (!File.Exists(spaceshipPath))
            {
                using var file = (new FileStream(spaceshipPath, FileMode.CreateNew));
            }

            using var fs = new FileStream(spaceshipPath, FileMode.Truncate);
            Serializer.Serialize(fs, spaceship);    
        }
    }
}
