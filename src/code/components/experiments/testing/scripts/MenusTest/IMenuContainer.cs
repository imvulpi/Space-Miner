using SpaceMiner.src.code.components.processing.ui.menu.interfaces;

namespace SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest
{
    public interface IMenuContainer
    {
        public IMenuManager MenuManager { get; set; }
        public IMenu Menu { get; set; }
    }
}
