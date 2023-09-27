using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }
	
	private int _score;

	public override void _Ready()
	{
	}

	public void GameOver()
	{
		GD.Print("On Player Hit");
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		
		GetNode<Hud>("HUD").ShowGameOver();
	}

	public void NewGame()
	{
		_score = 0;

		var player = GetNode<Player>("Player");
		var StartPosition = GetNode<Marker2D>("StartPosition");
		player.Start(StartPosition.Position);
		
		GetNode<Timer>("StartTimer").Start();

		var hud = GetNode<Hud>("HUD");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");
		
		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
	}
	
	private void OnMobTimerTimeout()
	{
		Mob mob = MobScene.Instantiate<Mob>();

		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		mob.Position = mobSpawnLocation.Position;

		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		AddChild(mob);
	}

	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	private void OnScoreTimerTimeout()
	{
		_score++;
		GetNode<Hud>("HUD").UpdateScore(_score);
	}
}
