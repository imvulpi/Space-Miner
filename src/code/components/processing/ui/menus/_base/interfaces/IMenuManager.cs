using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.ui.menu.interfaces
{
    /// <summary>
    /// Menu manager which  allows for management with priorities<br></br>
    /// If priority menus exist they will be opened as a MainAction instead of the MainMenu action.<br></br>
    /// This allows prioritizing specific menus in specific occasions, but should be deleted afterwards to funciton properly 
    /// </summary>
    public interface IMenuManager
    {
        /// <summary>
        /// Node which menus should connect to by default.
        /// </summary>
        public Node ConnectNode { get; set; }

        /// <summary>
        /// The current menu that is opened
        /// </summary>
        public IMenu CurrentMenu { get; set; }

        /// <summary>
        /// Menus and their prorities<br></br>
        /// </summary>
        public Dictionary<IMenu, int> RegisteredMenus { get; set; }

        /// <summary>
        /// Connect the menu so that when it's opened it can show up (depending on priority it might not)
        /// </summary>
        /// <param name="menu">The menu</param>
        /// <param name="priority">The priority</param>
        public void ConnectMenu(IMenu menu);

        /// <summary>
        /// Discornects the menu, usually this means it asigns nulls to some paramters
        /// </summary>
        /// <param name="menu">The menu</param>
        public void DisconnectMenu(IMenu menu);
    }
}
