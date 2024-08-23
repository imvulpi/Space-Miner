using Godot;
using SpaceMiner.src.code.components.commons.godot.scenes;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class MenuControllerTest : Control
{
	public MenuManager MenuManager { get; set; }
	[Export] PackedScene MainMenuScene { get; set; }
	public override void _Ready()
	{
		MenuManager = new MenuManager
		{
			MainMenuAction = (IMenuManager manager) =>
			{
				DefaultMenu menu = new()
				{
					MenuNode = MainMenuScene.Instantiate(),
					ConnectToNode = this
				};
				manager.RegisterMenu(menu);
				menu.Open(this);
			}
        };
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		MenuManager.ManageMenus();
	}
}
