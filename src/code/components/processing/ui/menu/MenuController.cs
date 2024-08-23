using Godot;
using SpaceMiner.src.code.components.commons.godot.scenes;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;

public partial class MenuController : Node
{
	public IMenuManager MenuManager { get; set; }
	public string MainScenePath = "res://src/code/components/user/ui/menus/game/scenes/game_menu.tscn";
	public override void _Ready()
	{
        if (!ResourceLoader.Exists(MainScenePath))
        {
			GD.PushError("Main scene path in MenuController does not exist. (Maybe moved?)");
        }

        MenuManager = new MenuManager
        {
            MainMenuAction = CreateMainMenu
        };
    }

	public void CreateMainMenu(IMenuManager menuManager)
	{
		DefaultMenu main = new DefaultMenu()
		{
			ConnectToNode = this,
			MenuNode = new SceneHelper().GetNodeFromScene(MainScenePath),
		};
		if(main.MenuNode is IMenuContainer container)
		{
			container.Menu = main;
			container.MenuManager = menuManager;
		}
		MenuManager.RegisterMenu(main);
		main.Open();
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		MenuManager.ManageMenus();
	}
}
