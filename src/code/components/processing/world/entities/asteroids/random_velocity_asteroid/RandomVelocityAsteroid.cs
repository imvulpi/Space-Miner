using Godot;
using SpaceMiner.src.code.components.user.entities.entities;
using System;

public partial class RandomVelocityAsteroid : AsteroidRigidBodyEntity
{
    private Random Random = new Random();
    private float timePassed = 0;
    private const float UpdateWaitTime = 1;
    public override void _Process(double delta)
	{
        if (timePassed > UpdateWaitTime)
        {
            Move();
            Rotate();
            timePassed = 0;
        }
        timePassed += (float)delta;
    }

    public override void Move()
    {
        LinearVelocity = new Vector2(Random.Next(-1, 2), Random.Next(-1, 2)) * MovementSpeed;
    }
}
