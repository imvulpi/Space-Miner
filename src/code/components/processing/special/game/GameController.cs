using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.user.special;
using System;
using SpaceMiner.src.code.components.processing.world.entities.player;
using SpaceMiner.src.code.components.processing.data.systems.world;
using SpaceMiner.src.code.components.commons.other.DI;
using DryIoc;

public partial class GameController : Node, IGameController
{
    [Export] public GameOverlayController GameOverlayController { get; set; }
    public IGameSettings GameSettings { get; set; }
    public PlayerEntity PlayerEntity { get; set; }
    public WorldManager WorldManager { get; set; }
    private PlayerEntityHandler PlayerEntityHandler { get; set; }
    public void Initialize()
    {
        try
        {
            PlayerEntityHandler ??= new PlayerEntityHandler(GameSettings);
            GameOverlayController.Initialize();
            GameOverlayController.GameMenuNode.SaveEvent += Save;
            PlayerEntity playerEntity = PlayerEntityHandler.ConstructPlayer();
            PlayerEntity = playerEntity;
            PlayerEntity.ConnectToNode = this;
            playerEntity.Initialize();
            WorldManager = new(GameSettings, this, playerEntity);
            WorldManager.Initialize();

            // Register DI
            DIContainer.Container.RegisterInstance<GameController>(this);
            DIContainer.Container.RegisterInstance<PlayerEntity>(playerEntity);
            DIContainer.Container.RegisterInstance<IGameSettings>(GameSettings);
            DIContainer.Container.RegisterInstance<WorldManager>(WorldManager);
            DIContainer.Container.RegisterInstance<GameOverlayController>(GameOverlayController);
        }
        catch (GameException ex)
        {
            ExceptionHandler.HandleException(ex, true);
        }
        catch(Exception ex)
        {
            ExceptionHandler.HandleException(ex, true);
        }
    }

    private void Save(object sender, EventArgs e)
    {
        Save();
    }
    private void Save()
    {
        PlayerEntityHandler.SavePlayer(PlayerEntity);
    }

    public override void _Process(double delta)
    {
        try
        {
            WorldManager.Manage();
        }
        catch (GameException ex)
        {
            if (ex.ErrorType != null && ex.ErrorType == PrettyErrorType.Critical)
            {
                ExceptionHandler.HandleException(ex, true);
            }
        }
    }
}
