using Godot;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

namespace SpaceMiner.src.code.components.processing.ui.menu
{
    public class Menu : IMenu
    {
        public Node MenuNode { get; set; }
        public Node ConnectToNode { get; set; }
        public int Priority { get; set; }
        public Action<IMenu, Node> OpenAction { get; set; }
        public Action<IMenu> CloseAction { get; set; }
        public Action<IMenu> Disconnect { get; set; }
        /// <summary>
        /// Use this function to execute code before closing or to stop closing from finishing in some cases.<br></br>
        /// return True to intercept and stop from closing
        /// </summary>
        public Func<bool> InterceptClose { get; set; }
        public bool DisconectOnClose { get; set; } = true;

        public void Close()
        {
            if (InterceptClose == null || InterceptClose.Invoke() == false)
            {
                CloseAction?.Invoke(this);
            }
        }

        public void Delete()
        {
            Disconnect?.Invoke(this);
            ConnectToNode.RemoveChild(MenuNode);
            MenuNode.Free();
        }

        public void Open(Node connectNode = null)
        {
            OpenAction?.Invoke(this, ConnectToNode);
        }
    }
}
