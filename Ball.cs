using Godot;
using System;

public partial class Ball : RigidBody2D
{
	public void Place(Vector2 position)
	{
		Position = position;
	}

	private void PlayerCollision()
	{
		// get current linear velocity
		Vector2 currentVelocity = LinearVelocity;

		LinearVelocity = new Vector2(currentVelocity.X, -currentVelocity.Y);
	}

	private void BlockCollision()
	{
		// get current linear velocity
		Vector2 currentVelocity = LinearVelocity;

		LinearVelocity = new Vector2(currentVelocity.X, -currentVelocity.Y);
	}

	private void ConnectSignals()
	{
		Player player = GetNode<Player>("/root/Main/Player");
		Block block = GetNode<Block>("/root/Main/Block");
		player.Hit += PlayerCollision;
		block.Hit += BlockCollision;
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
