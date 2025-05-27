#if TOOLS
using Godot;
using System;

[Tool]
public partial class FlockBehaviour : EditorPlugin
{
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
        var script = GD.Load<Script>("res://addons/FlockBehaviour/FlockNode/FlockNode.cs");
        var texture = GD.Load<Texture2D>("res://addons/FlockBehaviour/FlockNode/FlockIcon.png");
        AddCustomType("FlockNode", "Node", script, texture);
		GD.Print("Flock Behaviour Loaded");
	}

	public override void _ExitTree()
	{
		GD.Print("Flock Behaviour Unloaded");
		// Clean-up of the plugin goes here.
	}
}
#endif
