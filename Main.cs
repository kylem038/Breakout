using Godot;
using System;

public partial class Main : Node
{

	[Export]
    public PackedScene BallScene { get; set; }

	private void StartRound()
	{
		Ball ball = BallScene.Instantiate<Ball>();

		Marker2D ballStartPosition = GetNode<Marker2D>("BallStartPosition");

		ball.Place(ballStartPosition.Position);
		ball.LinearVelocity = new Vector2(0, -1);

		AddChild(ball);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartRound();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
