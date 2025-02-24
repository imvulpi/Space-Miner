using DryIoc;
using Godot;
using SpaceMiner.src.code.components.commons.other.DI;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.ui.hud;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using SpaceMiner.src.code.components.processing.ui.menus._base.menu_manager_pro;
using SpaceMiner.src.code.components.processing.world.entities.player;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System;
using System.ComponentModel;

public partial class GameOverlayController : CanvasLayer
{
	public MenuManager MenuManager;
    [Export] PackedScene GameMenuScene { get; set; }
    [Export] PackedScene CargoMenuScene { get; set; }
    [Export] PackedScene HUDScene { get; set; }
    [Export] CanvasLayer HUDLayer { get; set; }
    public IHudController HudController { get; set; }
    public GameMenuController GameMenuNode { get; set; }
    private IMenu MainMenu { get; set; }
    public CargoMenu CargoMenuNode { get; set; }
    private IMenu CargoMenu { get; set; }
    private PlayerEntity Player { get; set; }
    public void Initialize()
    {
        MenuManager = new() { ConnectNode = this };
        GameMenuNode = GameMenuScene.Instantiate<GameMenuController>();
        MainMenu = new Menu()
        {
            DisconectOnClose = false,
            MenuNode = GameMenuNode,
        };
        MenuManager.ConnectMenu(MainMenu);

        CargoMenuNode = CargoMenuScene.Instantiate<CargoMenu>();
        CargoMenu = new Menu()
        {
            DisconectOnClose = false,
            MenuNode = CargoMenuNode,
        };
        MenuManager.ConnectMenu(CargoMenu);
        HudController = new SpaceshipHudController(HUDLayer, HUDScene);
        HudController.Initialize();

        Timer timer = new()
        {
            WaitTime = 0.5,
            Autostart = true
        };
        timer.Timeout += () =>
        {
            if (DIContainer.Container.IsRegistered<PlayerEntity>())
            {
                Player = DIContainer.Container.Resolve<PlayerEntity>();
                timer.GetParent().RemoveChild(timer);
                timer.Dispose();
            }
        };
        AddChild(timer);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("Esc"))
        {
            if (MenuManager.CurrentMenu != null)
            {
                MenuManager.CurrentMenu.Close();
            }
            else
            {
                MainMenu.Open();
            }
        }
        else if (Input.IsActionJustPressed("Inventory"))
        {
            if(MenuManager.CurrentMenu == null && Player != null)
            {
                if (Player.MovementNode is ICargoSpaceship cargoSpaceship)
                {
                    CargoMenuNode.CargoList = cargoSpaceship.CargoModule.CargoList;
                    CargoMenu.Open();
                    CargoMenuNode.Initialize();
                }
            }
            else
            {
                MenuManager.CurrentMenu?.Close();
            }
        }
    }
}
