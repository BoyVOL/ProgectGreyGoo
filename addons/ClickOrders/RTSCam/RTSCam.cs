using Godot;
using System;

public partial class RTSCam : Camera2D
{
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        GD.Print(@event);
    }

}
