using Godot;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using SpaceMiner.src.code.components.processing.ui.menus._base.menu_manager_pro;

public partial class SettingsMenuTest : Control
{
	[Export] public PackedScene SettingsScene { get; set; }
	private MenuManager MenuManager = new();
	public IMenu Menu { get; set; }
	public override void _Ready()
	{
        Menu menu = new()
        {
            ConnectToNode = this,
            MenuNode = SettingsScene.Instantiate(),
        };
		Menu = menu;
        MenuManager.MainMenuAction = (IMenuManager manager) =>
		{
			manager.ConnectMenu(menu);
			menu.Open();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Esc"))
		{
			Menu.Open();
		}
	}
}
