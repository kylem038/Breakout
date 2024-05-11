using Godot;

public partial class Ball : RigidBody2D
{
	// Velocity range
	// We don't want the ball moving too fast or too slow
	public float MinVelocity = 150.0f;
    public float MaxVelocity = 350.0f;

	public void Place(Vector2 position)
	{
		Position = position;
	}

	public void ClampVelocity()
	{
		if (LinearVelocity.X > 0)
		{
			Vector2 currentVelocity = LinearVelocity;

			// Set the new velocity
			LinearVelocity = new Vector2(
				x: Mathf.Clamp(currentVelocity.X, MinVelocity, MaxVelocity),
				y: Mathf.Clamp(currentVelocity.Y, -MaxVelocity, MaxVelocity)
			);
		} 
		else 
		{
			Vector2 currentVelocity = LinearVelocity;

			// Set the new velocity
			LinearVelocity = new Vector2(
				x: Mathf.Clamp(currentVelocity.X, -MaxVelocity, -MinVelocity),
				y: Mathf.Clamp(currentVelocity.Y, -MaxVelocity, MaxVelocity)
			);
		}
	}

	public void IncreaseVelocity(float increaseAmount)
	{
		// Get the current velocity
        Vector2 currentVelocity = LinearVelocity;

        // Normalize the velocity to preserve direction
        currentVelocity = currentVelocity.Normalized() * (currentVelocity.Length() + increaseAmount);

        // Set the new velocity
        LinearVelocity = new Vector2(
			x: Mathf.Clamp(currentVelocity.X, -350, 350),
			y: Mathf.Clamp(currentVelocity.Y, -350, 350)
		);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
