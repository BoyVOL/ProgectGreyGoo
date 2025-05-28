using Godot;
using System;
using System.Collections.Generic;


public partial class FlockNode : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame. Test
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Godot.Collections.Array<Node> Test = this.GetChildren(false);
		foreach (Node2D node in Test)
		{
			GD.Print(node.Position);
		}
    }

}
