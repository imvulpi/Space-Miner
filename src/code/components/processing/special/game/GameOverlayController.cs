using Godot;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using SpaceMiner.src.code.components.processing.ui.menus._base.menu_manager_pro;
using System;

public partial class GameOverlayController : CanvasLayer
{
	private MenuManager MenuManager;
    [Export] PackedScene GameMenuScene { get; set; }
    private IMenu MainMenu { get; set; }
    public override void _Ready()
    {
        MenuManager = new() { ConnectNode = this };
        MainMenu = new Menu()
        {
            DisconectOnClose = false,
            MenuNode = GameMenuScene.Instantiate(),
            ConnectToNode = this
        };
        MenuManager.ConnectMenu(MainMenu);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("Esc"))
        {
            if(MenuManager.CurrentMenu != null)
            {
                MenuManager.CurrentMenu.Close();
            }
            else
            {
                MainMenu.Open();
            }
        }
    }
}
