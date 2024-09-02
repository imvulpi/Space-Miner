using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class SettingsMenuTest : Control
{
	[Export] public PackedScene SettingsScene { get; set; }
	private MenuManager MenuManager = new();
	public override void _Ready()
	{
		MenuManager.MainMenuAction = (IMenuManager manager) =>
		{
			DefaultMenu menu = new()
			{
				ConnectToNode = this,
				MenuNode = SettingsScene.Instantiate(),
			};

			manager.RegisterMenu(menu);
			GD.Print(menu.MenuNode.Name);
            if (menu.MenuNode is IMenuContainer container)
			{
				GD.Print("Is a container");
				container.Menu = menu;
				container.MenuManager = MenuManager;
			}
			menu.Open();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MenuManager.ManageMenus();
	}
}
