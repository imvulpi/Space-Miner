using SpaceMiner.src.code.components.processing.ui.menu.interfaces;

namespace SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest
{
    /// <summary>
    /// A interface for MenuManager and menu. Menu managers can use it to inject menus with those parameters when the menu is registered.<br></br>
    /// Should only be used if a class is a menu or will be used as one.<br></br>
    /// If those properties are null that means it's either
    /// <list>
    /// <item><description>1. Not a menu</description></item>
    /// <item><description>2. Hasn't been correctly registered</description></item>
    /// <item><description>3. The menu manager you are using doesnt support it</description></item>
    /// </list>
    /// </summary>
    public interface IMenuInject
    {
        public IMenuManager MenuManager { get; set; }
        public IMenu Menu { get; set; }
    }
}
