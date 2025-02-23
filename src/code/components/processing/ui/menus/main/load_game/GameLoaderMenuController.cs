using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using SpaceMiner.src.code.components.processing.ui.menus.main.load_game;
using SpaceMiner.src.code.components.user.ui.components.other.game_save_item;

/// <summary>
/// Game loader menu controller, Controls the loading/deleting of saves and it's positioning.
/// </summary>
public partial class GameLoaderMenuController : Control, IMenuInject
{
	[Export] public PackedScene ConfirmationDialog {  get; set; }
	[Export] public PackedScene SaveItem {  get; set; }
	[Export] public Control SavesHolder {  get; set; }
	[Export] public VScrollBar ScrollBar { get; set; }
	[Export] public Control SavesWrapper { get; set; }
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }

    private GameSaveHelper gameSaveHelper = new();
	public override void _Ready()
	{
        GameLoaderMenuView menuView = new GameLoaderMenuView(this, SavesWrapper, SavesHolder, SaveItem, ConfirmationDialog);
        menuView.GetAllSavesPaths = () =>
        {
            return gameSaveHelper.GetAllSavesDirectories();
        };
        menuView.Initialize();
        menuView.DeleteSave += DeleteSave;
        menuView.LoadSave += LoadSave;
	}

    private void LoadSave(object sender, IGameSaveItem saveItem)
    {
        GameSaveSettings settings = new GameSaveSettings()
        {
            SaveName = saveItem.SaveName,
            Path = saveItem.FullPath,
        };

        new GameSaveLoader(settings).Load(GetTree());
    }

    private void DeleteSave(object sender, IGameSaveItem saveItem)
    {
        gameSaveHelper.DeleteSave(saveItem.SaveName);
    }
}
