using Godot;

public partial class Player : RigidBody2D
{
	[Signal]
	public delegate void HitEventHandler();

    private float originalWidth = 80f;
	private float originalHeight = 8f;
    private TextureRect textureRect;
	private CollisionShape2D collisionShape2D;

	public void ReducePaddleWidth()
	{
		// Improvement: Animate the reduction in size
		textureRect.Size = new Vector2(originalWidth * 0.75F, originalHeight);
	}

	public override void _Ready()
	{
		collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        textureRect = GetNode<TextureRect>("TextureRect");
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
