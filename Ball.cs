using Godot;
using System;

public partial class Ball : RigidBody2D
{
	public void Place(Vector2 position)
	{
		Position = position;
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
