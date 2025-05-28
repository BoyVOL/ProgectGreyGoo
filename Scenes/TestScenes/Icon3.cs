using Godot;
using System;

public partial class Icon3 : Sprite2D, IFlockable2D
{
    
    public Vector2 TargetVector { get; set; }
    [Export]
    public float VModule;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        this.Position += TargetVector * VModule;
    }
}
