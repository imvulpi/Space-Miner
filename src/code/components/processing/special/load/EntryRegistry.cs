using Godot;
using ProtoBuf.Meta;
using SpaceMiner.src.code.components.commons.godot.support;
using System;

public partial class EntryRegistry : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        RuntimeTypeModel.Default.Add(typeof(Vector2), false).SetSurrogate(typeof(Vector2Surrogate));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
