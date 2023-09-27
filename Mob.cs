using Godot;
using System;

public partial class Mob : RigidBody2D
{
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}
	
	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
	}
}
