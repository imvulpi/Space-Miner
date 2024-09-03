using Godot;
using SpaceMiner.src.code.components.commons.errors;

public partial class ErrorHolder : Node
{
    public static string ErrorHolderNodeName { get; private set; }
    public override void _EnterTree()
    {
        ErrorHolderNodeName = Name;
        ErrorDisplayer.Init(GetTree());
    }
}
