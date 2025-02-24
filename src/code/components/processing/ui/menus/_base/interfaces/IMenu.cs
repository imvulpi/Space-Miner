using Godot;
using System;

namespace SpaceMiner.src.code.components.processing.ui.menu.interfaces
{
    public interface IMenu
    {
        void Open(Node connectNode = null);
        void Close();
        void Delete();
        Node MenuNode { get; set; }
        Node ConnectToNode { get; set; }
        int Priority { get; set; }
        bool DisconectOnClose { get; set; }
        public Action<IMenu, Node> OpenAction { get; set; }
        public Action<IMenu> CloseAction { get; set; }
        public Action<IMenu> Disconnect { get; set; }
        /// <summary>
        /// Use this function to execute code before closing or to stop closing from finishing in some cases.<br></br>
        /// return True to intercept and stop from closing
        /// </summary>
        public Func<bool> InterceptClose { get; set; }
    }
}