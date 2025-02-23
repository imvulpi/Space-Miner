using Godot;
using GruInject.API.Attributes;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Mark for rework
/// </summary
namespace SpaceMiner.src.code.components.processing.ui.menu
{
    [RegisterSingleton]
    public class MenuManager : IMenuManager
    {
        public List<IMenu> Menus { get; set; }
        public Action<IMenuManager> MainMenuAction { get; set; }
        public MenuManager() { 
            Menus = new List<IMenu>();
        }

        public void ManageMenus()
        {
            if (Input.IsActionJustPressed("Esc"))
            {
                if (Menus.Count == 0)
                {
                    MainMenuAction?.Invoke(this);
                }
                else
                {
                    IMenu menu = Menus[Menus.Count - 1];
                    if (menu.EscActionDelegate.Invoke(this) == false)
                    {
                        menu.Close();
                        Menus.Remove(menu);
                    }
                }
            }
        }

        public void RegisterMenu(IMenu menu)
        {
            Menus.Add(menu);
            menu.Manager = this;
        }

        public void UnregisterMenu(IMenu menu)
        {
            Menus.RemoveAll(item => item == menu);
        }
    }
}
