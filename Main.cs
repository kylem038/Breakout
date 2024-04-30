using Godot;
using System;

public partial class Main : Node
{
	// Viewport
	// W 640 H 320
	[Export]
    public PackedScene BallScene { get; set; }

	[Export]
	public PackedScene BlockScene {get; set;}

	private int[] columnPositions = {
		64, 128, 192, 256, 320, 384, 448, 512
	};

	private Vector2 getBlockSpawnPosition(int column, int row)
	{
		return new Vector2(column, row);
	}

	private int[] rowPositions = { 64, 128, 196 };

	private void StartRound()
	{
		Ball ball = BallScene.Instantiate<Ball>();

		Marker2D ballStartPosition = GetNode<Marker2D>("BallStartPosition");

		ball.Place(ballStartPosition.Position);
		ball.LinearVelocity = new Vector2(GD.RandRange(-300, 300), -150);

		AddChild(ball);

		// Spawn the blocks
		SpawnBlocks();
	}

	private void SpawnBlocks()
	{
		foreach (int row in rowPositions)
		{
			foreach (int column in columnPositions)
			{
				// Instance a new node at each position
				Block block = BlockScene.Instantiate<Block>();
				// Set the position of the new node
				block.Position = getBlockSpawnPosition(column, row);
				// Add the new node to the scene
				AddChild(block);
			}
		}
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
