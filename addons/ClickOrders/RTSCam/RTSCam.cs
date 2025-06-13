using Godot;
using System;

public partial class RTSCam : Camera2D
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }


    [Export]
    float ZoomFactor = 1.1f;
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseButton)
        {
            InputEventMouseButton MBEvent = (InputEventMouseButton)@event;
            GD.Print(GlobalPosition);
            GD.Print(GetGlobalMousePosition());
            Scroll((InputEventMouseButton)@event);
        }
    }

    
    public Vector2 Centralised(Vector2 Position)
    {
        Rect2 port = GetViewportRect();
        Vector2 ViewportSize = port.End - port.Position;
        return Position - ViewportSize / 2;
    }

    public void Scroll(InputEventMouseButton @event)
    {
        if (@event.Pressed)
        {
            if (@event.ButtonIndex == MouseButton.WheelDown)
            {
                Zoom /= ZoomFactor;
            }
            if (@event.ButtonIndex == MouseButton.WheelUp)
            {
                Zoom *= ZoomFactor;
            }
        }
    }

}
