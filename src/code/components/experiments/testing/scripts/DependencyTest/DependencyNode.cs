using Godot;
using GruInject.API.Attributes;
using GruInject.API.Initializators;
using SpaceMiner.src.code.components.processing.ui.menu;

public partial class DependencyNode : GraphElement
{
	[Inject] public DependencyTestClass TestClass { get; set; }
    public override void _EnterTree()
    {
		InstanceInitializator.CurrentInstanceInitializator.InitializeNodeInstance(this);
    }

    public override void _Ready()
	{
		if (TestClass != null)
		{
			GD.Print($"Injection succeeded | Name values is {TestClass.Name} / should it be: {(TestClass.Name == "space" ? "yes" : "no")}");
		}
		else
		{
			GD.Print("Injection fail");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
