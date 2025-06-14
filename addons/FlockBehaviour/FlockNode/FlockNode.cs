using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// Класс для реализации алгоритма поведения стаи птиц
/// </summary>
public partial class FlockNode : Node
{	
	[Export]
	float SeparationCoefficient = 10000;
	[Export]
	float CohesionCoefficient = (float)0.01;
	[Export]
	float AlignmentCoefficient = 10;
	protected delegate void IFlockOperator(Node2D Param1);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame. Test
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void _EnterTree()
	{
		base._EnterTree();
		Godot.Collections.Array<Node> Children = this.GetChildren(false);
		foreach (Node2D node in Children)
		{
			if (node is IFlockable2D)
			{
				((IFlockable2D)node).TargetVector = new Vector2(GD.Randf() * 2 - 1, GD.Randf() * 2 - 1);
			}
		}
    }


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Godot.Collections.Array<Node> Children = this.GetChildren(false);
		Separation(Children,SeparationCoefficient);
		Cohesion(Children,CohesionCoefficient);
		Alignment(Children, AlignmentCoefficient);
		NormaliseAll(Children);
	}

	/// <summary>
	/// Возвращает Центральную точку всех объектов в ноде.
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns>средний вектор всех позиций нод</returns>
	protected Vector2 GetCenter(Godot.Collections.Array<Node> children)
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
	/// Метод, добавляющий компоненту скорости для предотвращения сближения объектов
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <param name="Coefficient">Коэффициент важности данного правила</param>
	protected void Separation(Godot.Collections.Array<Node> children,float Coefficient)
	{
		foreach (Node2D node1 in children)
		{
			if (node1 is IFlockable2D)
			{
				float SqrAvoid = (float)(((IFlockable2D)node1).AvoidRadius*((IFlockable2D)node1).AvoidRadius);
				Vector2 AvoidVector = Vector2.Zero;
				foreach (Node2D node2 in children)
				{
					if (node2 is IFlockable2D)
					{
						float Distance = (node1.Position - node2.Position).LengthSquared();
						if (Distance > 0 && Distance < SqrAvoid)
						{
							AvoidVector += ((node1.Position - node2.Position).Normalized() / Distance);
						}
					}
				}
				((IFlockable2D)node1).TargetVector +=  AvoidVector * Coefficient;
			}
		}
	}

	/// <summary>
	/// Добавление компоненту выравнивания относительно остальных в стае
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <param name="Coefficient">Коэффициент важности данного правила</param>
	protected void Alignment(Godot.Collections.Array<Node> children, float Coefficient)
	{
		foreach (Node2D node1 in children)
		{
			if (node1 is IFlockable2D)
			{
				foreach (Node2D node2 in children)
				{
					Vector2 AlignVector = Vector2.Zero;
					if (node2 is IFlockable2D)
					{
						float Distance = (node1.Position - node2.Position).LengthSquared();
						if (Distance != 0)
						{
							AlignVector += ((IFlockable2D)node2).Speed / Distance;
						}
					}
					((IFlockable2D)node1).TargetVector += AlignVector * Coefficient;
				}
			}
		}
	}

	/// <summary>
	/// Метод, который добавляет компоненту стремления к центру масс
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <param name="Coefficient">Коэффициент важности данного правила</param>
	protected void Cohesion(Godot.Collections.Array<Node> children, float Coefficient)
	{
		Vector2 Center = GetCenter(children);

		foreach (Node2D node in children)
		{
			if (node is IFlockable2D)
			{
				Vector2 Difference = (Center - node.Position);
				((IFlockable2D)node).TargetVector += Difference.Normalized()*(float)Math.Sqrt(Difference.Length())*Coefficient;
			}
		}
	}


	/// <summary>
	/// Метод, ограничивающий целевой вектор IFlockable2D не больше 1
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns></returns>
	protected void NormaliseAll(Godot.Collections.Array<Node> children)
	{
		foreach (Node2D node in children)
		{
			if (node is IFlockable2D)
			{
				if (((IFlockable2D)node).TargetVector.LengthSquared() > 1)
				{
					((IFlockable2D)node).TargetVector = ((IFlockable2D)node).TargetVector.Normalized();
				}
			}
		}
	}

	/// <summary>
	/// Метод для возврата средней скорости всех Iflockable нод
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns></returns>
	protected Vector2 GetMedianSpeed(Godot.Collections.Array<Node> children)
	{
		Vector2 MidSpeed = Vector2.Zero;
		int IflockCount = 0;
		foreach (Node2D node in children)
		{
			if (node is IFlockable2D)
			{
				MidSpeed += ((IFlockable2D)node).Speed;
				IflockCount++;
			}
		}
		MidSpeed /= IflockCount;
		return MidSpeed;
	}

	/// <summary>
	/// Метод для возврата нод, обладающих интерфейсом Iflockable
	/// </summary>
	/// <param name="children">Список детей ноды, чтобы не выгружать список по новой каждый раз</param>
	/// <returns>отдельную коллекцию со всеми Iflockable</returns>
	protected Godot.Collections.Array<Node> GetIFlockables(Godot.Collections.Array<Node> children)
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
	/// Вектор множитель скорости относительно максимально йскорости объекта
	/// </summary>
	Vector2 TargetVector { get; set; }

	/// <summary>
	/// Текущий вектор скорости объекта
	/// </summary>
	Vector2 Speed { get; }

	/// <summary>
	/// Вектор потенциаьно максимальной скорости объекта
	/// </summary>
	float MaxSpeed { get; }
	
	/// <summary>
	/// Значение радиуса, ближе которого объекты стараются не подпускать к себе соседей
	/// </summary>
	double AvoidRadius { get; }
}
