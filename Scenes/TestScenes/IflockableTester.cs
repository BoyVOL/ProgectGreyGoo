using Godot;
using System;

public partial class IflockableTester : Sprite2D, IFlockable2D
{
    public Vector2 TargetVector { get; set; }

    [Export]
    public float VModule;

    [Export]
    public Vector2 Speed { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Vector2 OldPos = this.Position;
        this.Position += TargetVector * VModule;
        Speed = Position - OldPos;

    }

}