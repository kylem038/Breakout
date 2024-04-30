using Godot;

public partial class Player : RigidBody2D
{
	[Signal]
	public delegate void HitEventHandler();

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("player_left"))
		{
			LinearVelocity = new Vector2(-200, 0);
		}

		else if (Input.IsActionPressed("player_right"))
		{
			LinearVelocity = new Vector2(200, 0);
		}
		else
		{
			LinearVelocity = Vector2.Zero;
		}


	}
}
