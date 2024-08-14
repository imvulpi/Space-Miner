using Godot;
using GruInject.API.Attributes;
using GruInject.API.Nodes;

namespace GruInject.Example;
public partial class GruNodeDependentiary : GruNode
{
	[Inject] public DIAutoSpawnExample _MyService;
	[Inject] public DIServiceToRegisterExample _ServiceToRegister;
	[Inject] public DIServiceToRegisterExample _ServiceToRegister1;
	[Inject] public DIServiceToSingleRegisterExample _serviceSingle;
	[Inject] public DIServiceToSingleRegisterExample _serviceSingle1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("\nGruNode\n");
		GD.Print($"I am starting {typeof(GruNodeDependentiary)} and my field {typeof(DIAutoSpawnExample)} is initialized? : {_MyService!=null}");
		GD.Print($"I am starting {typeof(GruNodeDependentiary)} and my field {typeof(DIServiceToRegisterExample)} 0 is initialized? : {_ServiceToRegister!=null}");
		GD.Print($"I am starting {typeof(GruNodeDependentiary)} and my field {typeof(DIServiceToRegisterExample)} 1 is initialized? : {_ServiceToRegister1!=null}");
		GD.Print($"I am starting {typeof(GruNodeDependentiary)} and my field {typeof(DIServiceToSingleRegisterExample)} 0 is initialized? : {_serviceSingle!=null}");
		GD.Print($"I am starting {typeof(GruNodeDependentiary)} and my field {typeof(DIServiceToSingleRegisterExample)} 1 is initialized? : {_serviceSingle1!=null}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
