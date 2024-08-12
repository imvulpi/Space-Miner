using Godot;
using GruInject.API.Attributes;

public partial class DependencyNode : GraphElement
{
	[Inject] public DependencyTestClass TestClass { get; set; }
    public override void _EnterTree()
    {
		InstanceInitializator.CurrentInstanceInitializator.InitializeInstance(this);
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
