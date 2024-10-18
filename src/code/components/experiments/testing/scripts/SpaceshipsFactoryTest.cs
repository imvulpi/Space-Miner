using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.data.game.spaceships.factory;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
using SpaceMiner.src.code.components.user.entities.spaceships.prospector;
using System.IO;
using System.Threading;

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
        Spaceship spaceship2 = spaceshipFactory.GetSpaceship(prospectorSpaceship);
        AddChild(spaceship);
		AddChild(spaceship2);
        spaceship2.SetProcess(false);
        spaceship2.SetPhysicsProcess(false);


        /*		GD.Print("Spaceship was added");
                Thread.Sleep(1000);
                RemoveChild(spaceship);
                GD.Print("Removed spaceship");
                GD.Print("Serializing spaceship to a file");
                string spaceshipPath = OsPath.Join(ExternalPaths.TEMP_DIR, "spaceship.bin");
                using (FileStream fs = File.OpenWrite(spaceshipPath))
                {
                    Serializer.Serialize(fs, spaceship);
                }
                GD.Print("Serialized spaceship to a file");

                if (File.Exists(spaceshipPath))
                {
                    GD.Print("File successfully created");
                    GD.Print("Deserializing spaceship from a file");
                    using FileStream fs = File.OpenRead(spaceshipPath);
                    Spaceship deserializedSpaceship = Serializer.Deserialize<Spaceship>(fs);
                    if (deserializedSpaceship != null) {
                        GD.Print("Spaceship successfully deserialized");
                        Spaceship recreatedSpaceship = spaceshipFactory.GetSpaceship(prospectorSpaceship);
                        AddChild(recreatedSpaceship);
                        recreatedSpaceship.SetPhysicsProcess(false);
                        GD.Print("Spaceship was added");
                    }
                    else
                    {
                        GD.Print("Deserialization failed");
                    }
                }
                else
                {
                    GD.Print("File creation failed :(");
                }*/
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
