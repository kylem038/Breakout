using Godot;

public partial class Ball : RigidBody2D
{
	// Velocity range
	// We don't want the ball moving too fast or too slow
	public float MinVelocity = 100.0f;
    public float MaxVelocity = 400.0f;

	public void Place(Vector2 position)
	{
		Position = position;
	}

	private void PlayerCollision()
	{
		// get current linear velocity
		Vector2 currentVelocity = LinearVelocity;

		// Reverse direction no matter the direction the collision comes from (X or Y)
		currentVelocity *= -1;

		LinearVelocity = currentVelocity;
	}

	private void BlockCollision()
	{
		// get current linear velocity
		Vector2 currentVelocity = LinearVelocity;

		// Reverse direction no matter the direction the collision comes from (X or Y)
		currentVelocity *= -1;

		LinearVelocity = currentVelocity;
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

	private void ConnectSignals()
	{
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectSignals();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
