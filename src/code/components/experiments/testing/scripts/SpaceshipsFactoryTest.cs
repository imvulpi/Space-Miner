using Godot;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.data.game.spaceships.factory;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
using SpaceMiner.src.code.components.user.entities.spaceships.prospector;

public partial class SpaceshipsFactoryTest : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Initialization of the spaceships factory tests");

		SpaceshipFactory spaceshipFactory = new SpaceshipFactory();
		ProspectorSpaceshipFactory prospectorSpaceshipFactory = new ProspectorSpaceshipFactory();
		spaceshipFactory.RegisterSpaceshipFactory("spaceminer.prospector", prospectorSpaceshipFactory);
		GD.Print("Testing with Prospector spaceship");
        ProspectorSpaceship prospectorSpaceship = new ProspectorSpaceship();
        prospectorSpaceship.CurrentCargo = 100;
		Spaceship spaceship = spaceshipFactory.GetSpaceship(prospectorSpaceship);
		AddChild(spaceship);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
