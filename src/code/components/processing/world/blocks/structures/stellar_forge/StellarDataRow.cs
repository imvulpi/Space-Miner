using Godot;

namespace SpaceMiner.src.code.components.user.blocks.structures.stellaf_forge
{
    public partial class StellarDataRow : Control
    {
        [Export] public Label ItemLabel { get; set; }
        [Export] public Label BuyPrice { get; set; }
        [Export] public Label UserOwns { get; set; }
        [Export] public Button BuyButton { get; set; }
    }
}
