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
		Godot.Collections.Array<Node> Children = this.GetChildren(false);
		foreach (Node2D node in Children)
		{
			if (node is IFlockable2D)
			{
				((IFlockable2D)node).TargetVector = new Vector2(GD.Randf() * 2 - 1, GD.Randf() * 2 - 1);
			}
		}
		GD.Print(GetCenter(Children));
	}

	/// <summary>
	/// Возвращает Центральную точку всех объектов в ноде.
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns>средний вектор всех позиций нод</returns>
	protected Vector2 GetCenter(Godot.Collections.Array<Node> children = null)
	{
		Vector2 GeometricCenter = Vector2.Zero;
		int IflockCount = 0;
		foreach (Node2D node in children)
		{
			if (node is IFlockable2D)
			{
				GeometricCenter += node.Position;
				IflockCount++;
			}
		}
		GeometricCenter /= IflockCount;
		return GeometricCenter;
	}

	/// <summary>
	/// Метод для возврата средней скорости всех Iflockable нод
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns></returns>
	protected Vector2 GetMedianSpeed(Godot.Collections.Array<Node> children = null)
	{
		return Vector2.Zero;
	}

	/// <summary>
	/// Метод для возврата нод, обладающих интерфейсом Iflockable
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns>отдельную коллекцию со всеми Iflockable</returns>
	protected Godot.Collections.Array<Node> GetIFlockables(Godot.Collections.Array<Node> children = null)
	{
		Godot.Collections.Array<Node> result = new Godot.Collections.Array<Node>();
		foreach (Node2D node in children)
		{
			if (node is IFlockable2D)
			{
				result.Add(node);
			}
		}
		return result;
	}
}

/// <summary>
/// Интерфейс для взаимодействия FlockNode и дочерних нод
/// </summary>
public interface IFlockable2D
{
	/// <summary>
	/// Нормализированный вектор для указания направления, в котором надо двигаться объекту чтобы оставаться в стае
	/// </summary>
	Vector2 TargetVector { get; set; }

	/// <summary>
	/// Вектор скорости объекта, который нужно знать головной ноде
	/// </summary>
	Vector2 Speed { get; set; }
}
