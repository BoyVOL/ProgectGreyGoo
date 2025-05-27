#if TOOLS
using Godot;
using System;

[Tool]
public partial class FlockBehaviour : EditorPlugin
{
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
		GD.Print("Flock Behaviour Loaded");
	}

	public override void _ExitTree()
	{
		GD.Print("Flock Behaviour Unloaded");
		// Clean-up of the plugin goes here.
	}
}
#endif
