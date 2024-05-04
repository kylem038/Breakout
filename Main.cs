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

	// We want a 1px gutter between each block
	// Blocks are 48px wide
	private int[] columnPositions = {
		1, 50, 99, 148, 197, 246, 295, 344, 393, 442, 491, 540, 589
	};

	private Vector2 getBlockSpawnPosition(int column, int row)
	{
		return new Vector2(column, row);
	}

	// We want a 1px gutter between each block
	// Blocks are 8px in height
	private int[] rowPositions = { 1, 10, 19, 28, 37, 46, 55 };

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
