using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.ui.menu.interfaces
{
    public interface IMenuManager
    {
        public List<IMenu> Menus { get; set; }
        public Action<IMenuManager> MainMenuAction { get; set; }
        public void ManageMenus();
        public void RegisterMenu(IMenu menu);
        public void UnregisterMenu(IMenu menu);
    }
}
