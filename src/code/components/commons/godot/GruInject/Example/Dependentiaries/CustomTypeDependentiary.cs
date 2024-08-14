using Godot;
using GruInject.API.Attributes;
using GruInject.API.Initializators;

namespace GruInject.Example;
public partial class CustomTypeDependentiary : CpuParticles2D
{
    // This method has to be overriden in every custom type class (like this) for the GruInjector to work
    // if a specific type of a Node is used very often as a dependentiary then it's highly suggested to create a custom Gru node type.
    // The upside is that the method is always the same AND you can freely change the types without needing to change the method.
    public override void _EnterTree()
    {
        InstanceInitializator.CurrentInstanceInitializator.InitializeNodeInstance(this);
    }

    [Inject] public DIAutoSpawnExample _MyService;
    [Inject] public DIServiceToRegisterExample _ServiceToRegister;
    [Inject] public DIServiceToRegisterExample _ServiceToRegister1;
    [Inject] public DIServiceToSingleRegisterExample _serviceSingle;
    [Inject] public DIServiceToSingleRegisterExample _serviceSingle1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("\nCustom Type\n");
        GD.Print($"I am starting {typeof(CustomTypeDependentiary)} and my field {typeof(DIAutoSpawnExample)} is initialized? : {_MyService != null}");
        GD.Print($"I am starting {typeof(CustomTypeDependentiary)} and my field {typeof(DIServiceToRegisterExample)} 0 is initialized? : {_ServiceToRegister != null}");
        GD.Print($"I am starting {typeof(CustomTypeDependentiary)} and my field {typeof(DIServiceToRegisterExample)} 1 is initialized? : {_ServiceToRegister1 != null}");
        GD.Print($"I am starting {typeof(CustomTypeDependentiary)} and my field {typeof(DIServiceToSingleRegisterExample)} 0 is initialized? : {_serviceSingle != null}");
        GD.Print($"I am starting {typeof(CustomTypeDependentiary)} and my field {typeof(DIServiceToSingleRegisterExample)} 1 is initialized? : {_serviceSingle1 != null}");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
