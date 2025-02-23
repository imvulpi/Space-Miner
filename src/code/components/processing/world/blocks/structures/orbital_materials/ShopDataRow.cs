using Godot;

namespace SpaceMiner.src.code.components.user.blocks.structures.orbital_materials
{
    public partial class ShopDataRow : Control
    {
        [Export] public Label ItemLabel { get; set; }
        [Export] public Label SellPrice { get; set; }
        [Export] public Label UserOwns { get; set; }
        [Export] public LineEdit AmountInput { get; set; }
        [Export] public Button AmountAll { get; set; }
        [Export] public Button SellButton { get; set; }
    }
}
