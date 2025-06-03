using Godot;
using System;

public partial class IflockableTester : Sprite2D, IFlockable2D
{
    public Vector2 TargetVector { get; set; }

    [Export]
    public float VModule;

    [Export]
    public Vector2 Speed { get; private set; }

    [Export]
    public double AvoidRadius { get; private set; } = 10;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 OldPos = Position;
        Position += TargetVector * VModule * (float)delta;
        Speed = (Position - OldPos) / (float)delta;
    }
}