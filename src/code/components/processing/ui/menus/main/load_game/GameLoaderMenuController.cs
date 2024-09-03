using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
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
	
	private int saveCount = 0;
	private int cumulativeHeight = 0;
    private List<GameSaveItem> saveNodes = new();
	public override void _Ready()
	{
		try
		{
			string[] savesDirsPaths = new GameSaveHelper().GetAllSavesDirectories();
			foreach (string saveDirPath in savesDirsPaths)
			{
				string[] pathSplit = saveDirPath.Split(new char[] { '/', '\\' });
				string saveName = pathSplit[pathSplit.Length - 1];
				CreateItem(saveName, saveDirPath);
			}
		}catch(Exception ex)
		{
			GD.Print(ex);
		}
	}	
    private void CreateItem(string saveName, string fullPath)
	{
		GameSaveItem saveNode = SaveItem.Instantiate<GameSaveItem>();
		saveNode.Name = saveName;
		saveNode.NameLabel.Text = saveName;
		saveNode.PathLabel.Text = $"./saves/{saveName}";
        saveNode.FullPath = fullPath;
		saveNode.DeleteSaveEvent += SaveNode_OnDeleteGameSaveItem;
        saveNode.LoadSaveEvent += SaveNode_LoadSaveEvent;

		SavesHolder.AddChild(saveNode);
        saveNodes.Add(saveNode);

        saveNode.Position = new Vector2(saveNode.Position.X, saveNode.Size.Y * saveCount);
		saveNode.SetSize(new Vector2(SavesHolder.Size.X, saveNode.Size.Y));
        
		saveCount += 1;
        cumulativeHeight = (int)saveNode.Position.Y;
        CheckSaveHolderHeight();
    }

    private void SaveNode_LoadSaveEvent(object obj)
    {
		Node LoadConfirmationMenu = LoadSaveConfirmationMenu.Instantiate();

		if(LoadConfirmationMenu is SaveConfirmationDialog confirmationMenu && obj is GameSaveItem saveObj)
        {
			confirmationMenu.SaveName = saveObj.NameLabel.Text;
            AddChild(LoadConfirmationMenu);

            confirmationMenu.Decision += (bool shouldLoad) =>
			{
				if (shouldLoad)
				{
					GameSaveSettings settings = new GameSaveSettings()
					{
						SaveName = saveObj.NameLabel.Text,
					};
					new GameSaveManager(settings).Load(GetTree());
				}
				else
				{
					RemoveChild(LoadConfirmationMenu);
					LoadConfirmationMenu.QueueFree();
				}
			};
		}
		else
		{
            throw new GameException(PrettyErrorType.Invalid, "LoadConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
		}
    }

    private void SaveNode_OnDeleteGameSaveItem(object obj)
    {
        GD.Print("DELETE");
        Node DeleteConfirmationMenu = DeleteSaveConfirmationMenu.Instantiate();

		if(DeleteConfirmationMenu is SaveConfirmationDialog confirmationMenu && obj is GameSaveItem saveObj)
		{
			confirmationMenu.SaveName = saveObj.NameLabel.Text;
            AddChild(DeleteConfirmationMenu);
            confirmationMenu.Decision += (bool shouldDelete) =>
			{
				if (shouldDelete)
				{
					new GameSaveHelper().DeleteSave(saveObj.NameLabel.Text);
                    cumulativeHeight -= (int)saveObj.Size.Y;
                    SavesHolder.RemoveChild(saveObj);
                    saveNodes.Remove(saveObj);
                    saveObj.QueueFree();
                    RepositionNodes();
                    CheckSaveHolderHeight();
                    RemoveChild(DeleteConfirmationMenu);
                    DeleteConfirmationMenu.QueueFree();
                }
				else
				{
					RemoveChild(DeleteConfirmationMenu);
					DeleteConfirmationMenu.QueueFree();
				}
			};
		}
		else
        {
            throw new GameException(PrettyErrorType.Invalid, "DeleteConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
        }
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
