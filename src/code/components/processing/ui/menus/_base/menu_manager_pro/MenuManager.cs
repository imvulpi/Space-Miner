using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceMiner.src.code.components.processing.ui.menus._base.menu_manager_pro
{
    public class MenuManager : IMenuManager
    {
        public Dictionary<IMenu, int> RegisteredMenus { get; set; } = new Dictionary<IMenu, int>();
        public Action<IMenuManager> MainMenuAction { get; set; }
        public Node ConnectNode { get; set; }
        public IMenu CurrentMenu { get; set; }
        private List<IMenu> OpenedMenus = new();
        public void DisconnectMenu(IMenu menu)
        {
            RegisteredMenus.Remove(menu);
            if (menu.MenuNode.GetParent() != null && menu.ConnectToNode.GetParent() != null) menu.MenuNode.GetParent().RemoveChild(menu.MenuNode);
            if (menu.MenuNode is IMenuInject container)
            {
                if (container.MenuManager == this)
                {
                    container.MenuManager = null;
                    container.Menu = null;
                }
            }
            if(menu.ConnectToNode == ConnectNode)
            {
                menu.ConnectToNode = null;
            }

            menu.OpenAction -= HandleMenuOpenAction;
            menu.CloseAction -= HandleMenuCloseAction;
            menu.Disconnect -= DisconnectMenu;
        }

        public void ConnectMenu(IMenu menu)
        {
            if (menu.MenuNode is IMenuInject container)
            {
                container.MenuManager = this;
                container.Menu = menu;
            }
            if(menu.ConnectToNode == null)
            {
                if (ConnectNode == null)
                {
                    GD.Print("a");
                    throw new GameException(commons.errors.PrettyErrorType.Invalid, "ConnectNode is null in MenuManager", "The connect node shouldn't be null.");
                }
                else
                {
                    menu.ConnectToNode ??= ConnectNode;
                }
            }
            RegisteredMenus.Add(menu, menu.Priority);
            menu.OpenAction = HandleMenuOpenAction;
            menu.CloseAction = HandleMenuCloseAction;
            menu.Disconnect = DisconnectMenu;
        }

        private void HandleMenuOpenAction(IMenu menu, Node connectNode)
        {
            if (RegisteredMenus.TryGetValue(menu, out int menuPriority))
            {
                if (RegisteredMenus.Values.Max() <= menuPriority)
                {
                    connectNode.AddChild(menu.MenuNode);
                    OpenedMenus.Add(menu);
                    CurrentMenu = menu;
                }
            }
        }

        private void HandleMenuCloseAction(IMenu menu)
        {
            if (menu.DisconectOnClose) DisconnectMenu(menu);
            CurrentMenu = null;
            OpenedMenus.Remove(menu);
            if(OpenedMenus.Count > 0)
            {
                CurrentMenu = OpenedMenus.Last();
            }
            if (menu.MenuNode.GetParent() == null) return;
            menu.ConnectToNode.RemoveChild(menu.MenuNode);
        }
    }
}
