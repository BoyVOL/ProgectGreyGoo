using Godot;
using System;

public partial class Icon : Sprite2D, IFlockable2D
{
    public Vector2 TargetVector { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        TargetVector = new Vector2(GD.Randf(), GD.Randf());
        base._PhysicsProcess(delta);
    }

}
