using Godot;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.settings.user.couplers;
using SpaceMiner.src.code.components.user.special.player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.world.entities.player
{
    public class PlayerEntityHandler : IPlayerEntityHandler
    {
        public PlayerEntityHandler(IGameSettings gameSettings)
        {
            GameSettings = gameSettings;
        }

        public IGameSettings GameSettings { get; set; }
        private PlayerDataCoupler PlayerDataCoupler = new();
        private SpaceshipCoupler SpaceshipCoupler = new();
        public PlayerEntity ConstructPlayer()
        {
            PlayerEntity playerEntity = new();
            IPlayerData playerData = GetPlayerData();
            if(playerData.WorldLocation == WorldType.Universe)
            {
                Spaceship spaceship = ConstructSpaceship();
                playerEntity.MovementNode = ConstructSpaceship();
            }
            playerEntity.PlayerData = playerData;

            return playerEntity;
        }

        private Spaceship ConstructSpaceship()
        {
            SpaceshipCoupler ??= new();
            SpaceshipCoupler.Initialize();
            Spaceship spaceship = SpaceshipCoupler.GetSavedSpaceship(GameSettings);
            if (spaceship == null)
            {
                GD.Print("Spaceship is null!");
                ProspectorSpaceship prospectorSpaceship = new();
                spaceship = SpaceshipCoupler.SpaceshipFactory.GetSpaceship(prospectorSpaceship);
                SpaceshipCoupler.SaveSpaceship(GameSettings, prospectorSpaceship);
            }
            return spaceship;
        }

        private IPlayerData GetPlayerData()
        {
            PlayerEntity playerEntity = new();
            UserSettingCoupler settingCoupler = new();
            UserSettings userSettings = settingCoupler.Load(new UserSettings()) as UserSettings;
            IPlayerData playerData = PlayerDataCoupler.GetPlayerData(GameSettings.SaveName, userSettings.UUID);
            if (playerData != null)
            {
                GD.Print($"LOAD player Dat: {playerData}");
                playerEntity.PlayerData = playerData;
            }
            else
            {
                GD.Print($"NEW player Dat: {playerData}");
                PlayerData newPlayerData = new PlayerData()
                {
                    UUID = userSettings.UUID,
                    Nickname = userSettings.Username,
                    Balance = 0,
                    WorldLocation = WorldType.Universe
                };
                PlayerDataCoupler.SavePlayerData(GameSettings.SaveName, userSettings.UUID, newPlayerData);
                playerData = newPlayerData;
            }
            GD.Print($"player Dat: {playerData}");
            return playerData;
        }

        public void SavePlayer(PlayerEntity player)
        {
            if(player.MovementNode is ProspectorSpaceship spaceship)
            {
                GD.Print($"Saving spaceship {spaceship.Position} {spaceship.GlobalPosition} {spaceship.CargoCapacity}");
                SpaceshipCoupler.SaveSpaceship(GameSettings, spaceship);
            }
            PlayerDataCoupler.SavePlayerData(GameSettings.SaveName, player.PlayerData.UUID, (player.PlayerData as PlayerData));
        }
    }
}
