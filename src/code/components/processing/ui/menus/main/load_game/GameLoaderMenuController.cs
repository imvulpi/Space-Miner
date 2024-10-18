using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.ui.menus.main.load_game;
using SpaceMiner.src.code.components.user.ui.components.boards;
using SpaceMiner.src.code.components.user.ui.components.other;
using SpaceMiner.src.code.components.user.ui.components.other.game_save_item;
using System;
using System.Collections.Generic;

public partial class GameLoaderMenuController : Control
{
	[Export] public PackedScene LoadSaveConfirmationMenu {  get; set; }
	[Export] public PackedScene DeleteSaveConfirmationMenu { get; set; }
	[Export] public PackedScene SaveItem {  get; set; }
	[Export] public Control SavesHolder {  get; set; }
	[Export] public VScrollBar ScrollBar { get; set; }
	[Export] public Control SavesWrapper { get; set; }
    private LoadSaveEventHandler LoadSaveEventHandler { get; set; }
    private DeleteSaveEventHandler DeleteSaveEventHandler { get; set; }
    private GameSaveHelper GameSaveHelper { get; set; } = new GameSaveHelper();
	private int saveCount = 0;
	private int cumulativeHeight = 0;
    private readonly List<GameSaveItem> saveNodes = new();
    public override void _Ready()
	{
        LoadSaveEventHandler = new LoadSaveEventHandler(LoadSaveConfirmationMenu, this);
        DeleteSaveEventHandler = new DeleteSaveEventHandler(DeleteSaveConfirmationMenu, this);
		try
		{
            // Creates save items for all saves is /saves directory
			string[] savesDirsPaths = new GameSaveHelper().GetAllSavesDirectories();
			CreateAllSaveItems(savesDirsPaths);
		}
		catch (Exception ex)
		{
			ExceptionHandler.HandleException(ex, true);
		}
	}

    /// <summary>
    /// Iterates over all saves to create boxes and set it up.
    /// </summary>
	private void CreateAllSaveItems(string[] savesPaths)
	{
        foreach (string savePath in savesPaths)
        {
            string[] pathSplit = savePath.Split(new char[] { '/', '\\' });
            string saveName = pathSplit[pathSplit.Length - 1];
            CreateSaveItem(saveName, savePath);
        }
    }

    /// <summary>
    /// Creates a save box and connects its events.
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    /// <param name="fullPath">Path to the save</param>
    private void CreateSaveItem(string saveName, string fullPath)
	{
        GameSaveItem saveNode = SetupSaveItem(saveName, fullPath);
		saveCount += 1;
        cumulativeHeight = (int)saveNode.Position.Y;
        CheckSaveHolderHeight();
    }

    /// <summary>
    /// Creates a new Game save item and sets everything in it<br></br>Sets up events, names, paths, and other
    /// </summary>
    /// <param name="saveName">name of the save</param>
    /// <param name="fullPath">path to the save</param>
    /// <returns>created save</returns>
	private GameSaveItem SetupSaveItem(string saveName, string fullPath)
	{
        GameSaveItem saveNode = SaveItem.Instantiate<GameSaveItem>();
        saveNode.Name = saveName;
        saveNode.NameLabel.Text = saveName;
        saveNode.PathLabel.Text = $"./saves/{saveName}";
        saveNode.FullPath = fullPath;
        saveNode.DeleteSaveEvent += (object obj) =>
        {
            GameSaveItem saveItem = DeleteSaveEventHandler.DeleteSaveEvent(obj);
            if(saveItem != null ) {
                DeleteSaveItem(saveItem);
            }
        };

        saveNode.LoadSaveEvent += LoadSaveEventHandler.LoadSaveEvent;

        SavesHolder.AddChild(saveNode);
        saveNodes.Add(saveNode);
        saveNode.Position = new Vector2(saveNode.Position.X, saveNode.Size.Y * saveCount);
        saveNode.SetSize(new Vector2(SavesHolder.Size.X, saveNode.Size.Y));

        return saveNode;
    }

    /// <summary>
    /// Deletes a save from system and save holders
    /// </summary>
    /// <param name="saveItem"></param>
    private void DeleteSaveItem(GameSaveItem saveItem)
    {
        GameSaveHelper.DeleteSave(saveItem.NameLabel.Text);
        cumulativeHeight -= (int)saveItem.Size.Y;
        SavesHolder.RemoveChild(saveItem);
        saveNodes.Remove(saveItem);
        RepositionNodes();
        CheckSaveHolderHeight();
    }

    /// <summary>
    /// Repositions all nodes to adjust possible spaces, ex. in case deleted nodes.
    /// </summary>
    private void RepositionNodes()
	{
		int nodeCount = 0;
		foreach (var node in saveNodes)
		{
            node.Position = new Vector2(node.Position.X, node.Size.Y * nodeCount);
            nodeCount++;
        }
    }

    /// <summary>
    /// Checks and changes sizes of saves wrapper and holder to fit boxes nicely without spaces.
    /// </summary>
	private void CheckSaveHolderHeight()
	{
		if(cumulativeHeight > SavesHolder.Size.Y)
		{
			SavesWrapper.SetSize(new Vector2(SavesWrapper.Size.X, cumulativeHeight));
			SavesHolder.SetSize(new Vector2(SavesHolder.Size.X, cumulativeHeight));
        }
        else if(SavesHolder.Size.Y > cumulativeHeight)
		{
            SavesWrapper.SetSize(new Vector2(SavesWrapper.Size.X, cumulativeHeight));
            SavesHolder.SetSize(new Vector2(SavesHolder.Size.X, cumulativeHeight));
        }
	}
}
