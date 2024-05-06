using Godot;
using System;

namespace d9.sbg.examples;
public partial class UI : CanvasLayer
{
	[Signal]
	public delegate void AdvanceGameEventHandler();
	private void OnAdvanceGameButtonPressed()
	{
	}
}
