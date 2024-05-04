using Godot;

public partial class Block : StaticBody2D
{
	[Signal]
	public delegate void HitEventHandler();

	private int _increaseSpeed = 7;

	// Reference to the TextureRect child node
    private TextureRect textureRect;

	public void SetColor(Color color)
    {
		textureRect.Modulate = color;
    }

	private void OnBodyEntered(Node2D body)
	{
		Ball ball = GetNodeOrNull<Ball>("/root/Main/Ball");
		ball?.IncreaseVelocity(_increaseSpeed);
		QueueFree();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		textureRect = GetNodeOrNull<TextureRect>("TextureRect");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
