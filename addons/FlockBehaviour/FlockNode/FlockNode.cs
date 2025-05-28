using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// Класс для реализации алгоритма поведения стаи птиц
/// </summary>
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
			if (node is IFlockable2D) {
				GD.Print(node.Position);
				GD.Print(((IFlockable2D)node).TargetVector);
			}
		}
	}

}

public interface IFlockable2D
{
	Vector2 TargetVector{ get; set; }
}
