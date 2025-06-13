#if TOOLS
using Godot;
using System;

[Tool]
public partial class ClickOrders : EditorPlugin
{
	public override void _EnterTree()
	{
        var script = GD.Load<Script>("res://addons/ClickOrders/RTSCam/RTSCam.cs");
        var texture = GD.Load<Texture2D>("res://addons/ClickOrders/RTSCam/RTSCam.png");
        AddCustomType("RTSCam", "Camera2D", script, texture);
		GD.Print("Click Orders Loaded");
	}

	public override void _ExitTree()
	{
		GD.Print("Click Orders Unloaded");
	}
}
#endif
