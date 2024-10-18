using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship.basic_controller
{
    public class BasicSpaceshipController : ISpaceshipController<CharacterBody2D>
    {
        public BasicSpaceshipController() { }
        public BasicSpaceshipController(float acceleration, float deceleration, float rotationSpeed)
        {
            Acceleration = acceleration;
            Deceleration = deceleration;
            RotationSpeed = rotationSpeed;
        }

        public float Acceleration { get; set; }
        public float Deceleration { get; set; }
        public float RotationSpeed { get; set; }
        public float MaxSpeed { get; set; }

        private float currentSpeed = 0.0f;
        private float currentAcceleration = 0.0f;
        public void Addtional(CharacterBody2D entity, double delta)
        {
            return;
        }

        public void Move(CharacterBody2D entity, double delta)
        {
            Vector2 positionMovement = GetPostitionChange((float)delta, entity.Rotation);
            positionMovement.X = positionMovement.X * currentSpeed * (float)delta;
            positionMovement.Y = positionMovement.Y * currentSpeed * (float)delta;

            UpdateSpeed((float)delta);
            KinematicCollision2D collision2D = entity.MoveAndCollide(positionMovement);
            if(collision2D != null)
            {
                float speedLoss = 1 * currentSpeed * (float)Math.Cos(collision2D.GetAngle());
                currentSpeed -= speedLoss;
            }
        }

        private void UpdateSpeed(float delta)
        {
            if (currentSpeed > 0 && !Input.IsKeyPressed(Key.W))
            {
                currentSpeed -= Acceleration/2 * delta;
            }
            else if (currentSpeed < 0 && !Input.IsKeyPressed(Key.S))
            {
                if (currentSpeed <= Acceleration * delta)
                {
                    currentSpeed = 0;
                }
                else
                {
                    currentSpeed += Acceleration * delta;
                }
            }
        }

        private Vector2 GetPostitionChange(float delta, float rotation)
        {
            Vector2 directionVector = new(-MathF.Sin(rotation), MathF.Cos(rotation));
            if (Input.IsKeyPressed(Key.W))
            {
                IncreaseSpeed(Acceleration * delta);
            }

            if (Input.IsKeyPressed(Key.S))
            {
                IncreaseSpeed(-Acceleration * delta);
            }

            return directionVector;
        }

        private void IncreaseSpeed(float increase)
        {
            if (increase > 0 && currentSpeed + increase > MaxSpeed)
            {
                currentSpeed = MaxSpeed;
            }
            else if (increase < 0 && currentSpeed - increase < -MaxSpeed)
            {
                currentSpeed = -MaxSpeed;
            }
            else
            {
                currentSpeed += increase;
            }
        }

        private float desiredRotation = 0.0f;
        public void Rotate(CharacterBody2D entity, double delta)
        {
            desiredRotation = GetRotationChange(entity.RotationDegrees, (float)delta);
            if (desiredRotation != entity.Rotation)
            {
                entity.Rotation = (float)Mathf.LerpAngle(entity.Rotation, desiredRotation, RotationSpeed * delta);
            }
        }

        private float GetRotationChange(float rotation, float delta)
        {
            float newRotation = rotation;
            Random random = new Random();
            int randomDriftRotation = random.Next(1, 3);
            if (Input.IsKeyPressed(Key.A))
            {
                if (MathF.Round(rotation) == 180)
                {
                    newRotation = -180;
                }
                newRotation -= 1 * RotationSpeed;

                if (Input.IsKeyPressed(Key.Space))
                {
                    newRotation -= 1 * randomDriftRotation * RotationSpeed;
                    if (currentSpeed > 0)
                    {
                        IncreaseSpeed(-Deceleration * 1.4f * delta);
                    }
                }
            }

            if (Input.IsKeyPressed(Key.D))
            {
                if (MathF.Round(rotation) == -180)
                {
                    newRotation = 180;
                }
                newRotation += 1 * RotationSpeed;

                if (Input.IsKeyPressed(Key.Space))
                {
                    newRotation += 1 * randomDriftRotation * RotationSpeed;
                    if (currentSpeed > 0)
                    {
                        IncreaseSpeed(-Deceleration * 1.4f * delta);
                    }
                }
            }

            float newRotationRadians = newRotation * MathF.PI / 180;
            return newRotationRadians;
        }
    }
}
