using Godot;
using System;

public partial class IflockableTester : Sprite2D, IFlockable2D
{
    public Vector2 TargetVector { get; set; }

    public float MaxSpeed { get => VModule; }

    [Export]
    public float VModule =1000;

    [Export]
    public Vector2 Speed { get; protected set; }

    [Export]
    public double AvoidRadius { get; protected set; } = 1400000;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 OldPos = Position;
        Position += TargetVector * VModule * (float)delta;
        Speed = (Position - OldPos) / (float)delta;
    }
}