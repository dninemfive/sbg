using Godot;
using d9.sbg.examples.baghchal;

namespace d9.sbg.examples.godot;
public partial class Root : Node
{
	public UI UI => GetNode<UI>("UI");
	public BaghChalGame? Game { get; private set; } = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BaghChalGame game = new(new BaghChalAgent_Random(), new BaghChalAgent_Random());
	}
	private void UpdateUI()
	{
		if (Game is null)
			return;
		BaghChalState state = Game.CurrentState;
		List<(string name, int value)> variables =
		[
			(nameof(state.CapturedSheep), state.CapturedSheep),
			(nameof(state.UnplacedSheep), state.UnplacedSheep),
			(nameof(Game.Turn), Game.Turn)
		];
		foreach ((string name, int value) in variables)
			UI.GetNode<Label>($"{name}Label").Text = $"{name}: {value}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
