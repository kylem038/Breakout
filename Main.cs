using Godot;

public partial class Main : Node
{
	// Viewport
	// W 640 H 320
	[Export]
    public PackedScene BallScene { get; set; }

	[Export]
	public PackedScene BlockScene {get; set;}

	private int _score = 0;
	private int _highScore = 0;
	private int _health = 4;
	private bool _hitCeiling = false;
	private bool _spawnBlocks = true;

	// We want a 1px gutter between each block
	// Blocks are 48px wide
	private int[] columnPositions = {
		1, 50, 99, 148, 197, 246, 295, 344, 393, 442, 491, 540, 589
	};

	// We want a 1px gutter between each block
	// Blocks are 8px in height
	// +19 to account for score at top
	private int[] rowPositions = { 20, 29, 38, 47, 56, 65, 74 };

	private Vector2 getBlockSpawnPosition(int column, int row)
	{
		return new Vector2(column, row);
	}

	Color[] colors = {
		Color.Color8(247, 47, 150, 255), // Pink
		Color.Color8(175, 65, 84, 255), // Light Red
		Color.Color8(211, 84, 0, 255), // Orange
		Color.Color8(255, 246, 143, 255), // Yellow
		Color.Color8(22, 160, 133, 255), // Green
		Color.Color8(30, 81, 123, 255), // Blue
		Color.Color8(159, 90, 253, 255), // Purple
	};

	public void ScorePoint()
	{
		HUD hud = GetNode<HUD>("HUD");
		_score++;
		hud.UpdateScore(_score);
	}

	private void RemoveBall(ref Node2D body)
	{
		body.QueueFree();
	}

	private void OnBottomBoundaryBodyEntered(Node2D body)
	{
		RemoveBall(ref body);
		HUD hud = GetNode<HUD>("HUD");
		_health -= 1;
		hud.UpdateHealth(_health);
		if (_health != 0)
		{
			SpawnBall();
		}
		if (_health == 0)
		{
			GameOver();
		}
	}

	private void OnTopBoundaryBodyEntered(Node2D body)
	{
		if (body is Ball)
		{
			if (!_hitCeiling)
			{
				_hitCeiling = true;
				// Reduce paddle width by 75%
				Player player = GetNode<Player>("Player");
				player.ReducePaddleWidth();
			}
		}
	}

	private void SpawnBall()
	{
		Ball ball = BallScene.Instantiate<Ball>();

		Marker2D ballStartPosition = GetNode<Marker2D>("BallStartPosition");

		ball.Place(ballStartPosition.Position);
		ball.LinearVelocity = new Vector2(GD.RandRange(-300, 300), -150);

		CallDeferred("add_child", ball);
	}

	private void SpawnBlocks()
	{
		for (int i = 0; i < rowPositions.Length; i++)
		{
			foreach (int column in columnPositions)
			{
				// Instance a new node at each position
				Block block = BlockScene.Instantiate<Block>();
				// Set the position of the new node
				block.Position = getBlockSpawnPosition(column, rowPositions[i]);
				// Connect Score signal
				block.Score += ScorePoint;
				// Add the new node to the scene
				AddChild(block);
				// Add color to block
				block.SetColor(colors[i]);
			}
		}
	}

	private void NewGame()
	{
		// Set score to 0
		HUD hud = GetNode<HUD>("HUD");
		_score = 0;
		hud.UpdateScore(_score);

		// Set health to 4
		_health = 4;
		hud.UpdateHealth(_health);

		// Set player position back to default
		Player player = GetNode<Player>("Player");
		Marker2D startPosition = GetNode<Marker2D>("PlayerStartPosition");
		player.Position = startPosition.Position;

		// Spawn blocks
		_spawnBlocks = true;
	}

	private void GameOver()
	{
		// Despawn all blocks
		GetTree().CallGroup("blocks", Node.MethodName.QueueFree);
		// Show Game over
		HUD hud = GetNode<HUD>("HUD");
		hud.ShowGameOver();
		// Update high score if necessary
		if (_score > _highScore)
		{
			_highScore = _score;
			hud.UpdateHighScore(_highScore);
		}

		NewGame();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{ 
		GetNode<AudioStreamPlayer>("Music").Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Using a flag here to manage spawning blocks
		// If I use it elsewhere it tries to do things in the middle of the physics process
		// Doing it here ensures the physics process is complete before acting
		if(_spawnBlocks)
		{
			SpawnBlocks();
			_spawnBlocks = false;
		}
	}
}
