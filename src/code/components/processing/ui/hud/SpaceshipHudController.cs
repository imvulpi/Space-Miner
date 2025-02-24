using DryIoc;
using Godot;
using Godot.Collections;
using SpaceMiner.src.code.components.commons.other.DI;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.ui.hud;
using SpaceMiner.src.code.components.processing.world.entities.player;
using System;

public partial class SpaceshipHudController : IHudController
{
    public SpaceshipHudController(CanvasLayer hudCanvasLayer, PackedScene hudScene)
    {
        HUDCanvasLayer = hudCanvasLayer;
        SpaceshipHUDScene = hudScene;
    }

    public CanvasLayer HUDCanvasLayer { get; set; }
    public PackedScene SpaceshipHUDScene { get; set; }
    public SpaceshipHUD SpaceshipHUD { get; set; }
    private PlayerEntity Player { get; set; }
    public void Clear()
    {
        Array<Node> children = HUDCanvasLayer.GetChildren();
        foreach (Node item in children)
        {
            HUDCanvasLayer.RemoveChild(item);
        }
    }
    public void Initialize()
    {
        SpaceshipHUD = SpaceshipHUDScene.Instantiate<SpaceshipHUD>();
        HUDCanvasLayer.AddChild(SpaceshipHUD);
        DIContainer.Container.RegisterInstance<SpaceshipHUD>(SpaceshipHUD);
        Timer timer = new()
        {
            Autostart = true,
            WaitTime = 0.5
        };
        timer.Timeout += () =>
        {
            if (DIContainer.Container.IsRegistered<PlayerEntity>())
            {
                Player = DIContainer.Container.Resolve<PlayerEntity>();
                SpaceshipHUD.GetBalance = () =>
                {
                    return Player.PlayerData.Balance;
                };

                SpaceshipHUD.GetCargoCurrentCapacity = () =>
                {
                    if (Player.MovementNode is ICargoSpaceship spaceship)
                    {
                        return spaceship.CargoModule.CurrentCapacity;
                    }
                    return 0;
                };

                SpaceshipHUD.GetCargoMaxCapacity = () =>
                {
                    if (Player.MovementNode is ICargoSpaceship spaceship)
                    {
                        return spaceship.CargoModule.MaxCapacity;
                    }
                    return 0;
                };

                if (Player.MovementNode is ICargoSpaceship spaceship)
                {
                    spaceship.CargoModule.CargoChanged += (object o, EventArgs e) =>
                    {
                        SpaceshipHUD.UpdateCargo();
                    };
                }

                Player.PlayerData.BalanceChanged += (object o, float money) =>
                {
                    SpaceshipHUD.UpdateBalance();
                };

                SpaceshipHUD.UpdatesHUD();
                timer.Stop();
                timer.GetParent().RemoveChild(timer);
                timer.Dispose();
            }
        };
        HUDCanvasLayer.AddChild(timer);
        timer.Start();
    }
    public void Display()
    {
        SpaceshipHUD.UpdatesHUD();
    }
}
