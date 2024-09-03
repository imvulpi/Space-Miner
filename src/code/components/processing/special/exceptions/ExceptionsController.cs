using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using System;

public partial class ExceptionsController : Node
{
    public override void _EnterTree()
    {
        GameExceptionHandler.Init(GetTree());
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
