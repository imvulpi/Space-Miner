using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.user.ui.components.boards;
using SpaceMiner.src.code.components.user.ui.components.other;
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
	
	private int saveCount = 0;
	private int cumulativeHeight = 0;
    private readonly List<GameSaveItem> saveNodes = new();
	public override void _Ready()
	{
		try
		{
			string[] savesDirsPaths = new GameSaveHelper().GetAllSavesDirectories();
			CreateAllSaveItems(savesDirsPaths);
		}
		catch (Exception ex)
		{
			ExceptionHandler.HandleException(ex, true);
		}
	}

	private void CreateAllSaveItems(string[] savesPaths)
	{
        foreach (string savePath in savesPaths)
        {
            string[] pathSplit = savePath.Split(new char[] { '/', '\\' });
            string saveName = pathSplit[pathSplit.Length - 1];
            CreateSaveItem(saveName, savePath);
        }
    }

    private void CreateSaveItem(string saveName, string fullPath)
	{
        GameSaveItem saveNode = CreateGameSaveItem(saveName, fullPath);
		SetupGameSaveItem(saveNode);

		saveCount += 1;
        cumulativeHeight = (int)saveNode.Position.Y;
        CheckSaveHolderHeight();
    }

	private GameSaveItem CreateGameSaveItem(string saveName, string fullPath)
	{
        GameSaveItem saveNode = SaveItem.Instantiate<GameSaveItem>();
        saveNode.Name = saveName;
        saveNode.NameLabel.Text = saveName;
        saveNode.PathLabel.Text = $"./saves/{saveName}";
        saveNode.FullPath = fullPath;
        saveNode.DeleteSaveEvent += SaveNode_OnDeleteGameSaveItem;
        saveNode.LoadSaveEvent += SaveNode_LoadSaveEvent;
		return saveNode;
    }

	private void SetupGameSaveItem(GameSaveItem saveItem)
	{
        SavesHolder.AddChild(saveItem);
        saveNodes.Add(saveItem);
        saveItem.Position = new Vector2(saveItem.Position.X, saveItem.Size.Y * saveCount);
        saveItem.SetSize(new Vector2(SavesHolder.Size.X, saveItem.Size.Y));
    }

    // TODO: seperate more
    private void SaveNode_LoadSaveEvent(object obj)
    {
		Node LoadConfirmationMenu = LoadSaveConfirmationMenu.Instantiate();

		if(LoadConfirmationMenu is BinaryDecisionDialogBox confirmationDialog && obj is GameSaveItem saveObj)
        {
            SetupLoadSaveConfirmationDialog(confirmationDialog, saveObj);
        }
		else
		{
            throw new GameException(PrettyErrorType.Invalid, "LoadConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
		}
    }

    private void SetupLoadSaveConfirmationDialog(BinaryDecisionDialogBox dialog, GameSaveItem saveItem)
    {
        dialog.SaveName = saveItem.NameLabel.Text;
        dialog.Title = "Load save?";
        dialog.Message = $"Do you want to load: {saveItem.NameLabel.Text} Save?";
        AddChild(dialog);

        dialog.Decision += (bool shouldLoad) =>
        {
            if (shouldLoad)
            {
                GameSaveSettings settings = new GameSaveSettings()
                {
                    SaveName = saveItem.NameLabel.Text,
                };

                try
                {
                    new GameSaveManager(settings).Load(GetTree());
                }catch(Exception ex)
                {
                    ExceptionHandler.HandleException(ex, true);
                }
            }
            else
            {
                RemoveChild(dialog);
                dialog.QueueFree();
            }
        };
    }

    private void SaveNode_OnDeleteGameSaveItem(object obj)
    {
        Node DeleteConfirmationMenu = DeleteSaveConfirmationMenu.Instantiate();
		if(DeleteConfirmationMenu is BinaryDecisionDialogBox confirmationMenu && obj is GameSaveItem saveObj)
		{
			SetupDeleteSaveConfirmationDialog(confirmationMenu, saveObj);
		}
		else
        {
            throw new GameException(PrettyErrorType.Invalid, "DeleteConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
        }
    }

	private void SetupDeleteSaveConfirmationDialog(BinaryDecisionDialogBox dialog, GameSaveItem saveItem)
	{
        dialog.SaveName = saveItem.NameLabel.Text;
        dialog.Title = "Delete save?";
        dialog.Message = $"Do you wish to delete: {saveItem.NameLabel.Text} Save?\nThis action can't be reversed";
        AddChild(dialog);
        dialog.Decision += (bool shouldDelete) =>
        {
            if (shouldDelete)
            {
                try
                {
                    DeleteSaveItem(saveItem);
                    RepositionNodes();
                    CheckSaveHolderHeight();
                    RemoveChild(dialog);
                    saveItem.QueueFree();
                    dialog.QueueFree();
                }catch(Exception ex)
                {
                    ExceptionHandler.HandleException(ex, true);
                }
            }
            else
            {
                RemoveChild(dialog);
                dialog.QueueFree();
            }
        };
    }

    private void DeleteSaveItem(GameSaveItem saveItem)
    {
        new GameSaveHelper().DeleteSave(saveItem.NameLabel.Text);
        cumulativeHeight -= (int)saveItem.Size.Y;
        SavesHolder.RemoveChild(saveItem);
        saveNodes.Remove(saveItem);
    }

    private void RepositionNodes()
	{
		int nodeCount = 0;
		foreach (var node in saveNodes)
		{
            node.Position = new Vector2(node.Position.X, node.Size.Y * nodeCount);
            nodeCount++;
        }
    }

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
