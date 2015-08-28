using UnityEngine;
using UnityEngine.UI;

public delegate bool CommandCallback();

public abstract class ShipCommand : Command {
	protected ShipCommand(CommandCreator commandCreator, int boxPreFabIndex, string label) {
		this.commandBoxPreFab = commandCreator.availableBoxes[boxPreFabIndex];
		this.indentLevel = 0;
		this.label = label;
	}

	protected PlayerController getController() {
		GameObject spaceShip = GameObject.FindWithTag("Player");
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		return spaceShipController;
	}
}

public class ShootCommand : ShipCommand {
	public ShootCommand(CommandCreator creator) : base(creator, 0, "Shoot") { }

	public override bool execute(ref int programCounter) {
		return getController().shoot();
	}
}

public class MoveForwardCommand : ShipCommand {
	public MoveForwardCommand(CommandCreator creator) : base(creator, 1, "Move Forward") { }

	public override bool execute(ref int programCounter) {
		return getController().moveForward();
	}
}

public class MoveBackwardCommand : ShipCommand {
	public MoveBackwardCommand(CommandCreator creator) : base(creator, 2, "Move Backward") { }

	public override bool execute(ref int programCounter) {
		return getController().moveBackward();
	}
}

public class MoveLeftwardsCommand : ShipCommand {
	public MoveLeftwardsCommand(CommandCreator creator) : base(creator, 3, "Move Leftwards") { }

	public override bool execute(ref int programCounter) {
		return getController().moveLeftwards();
	}
}

public class MoveRightwardsCommand : ShipCommand {
	public MoveRightwardsCommand(CommandCreator creator) : base(creator, 4, "Move Rightwards") { }

	public override bool execute(ref int programCounter) {
		return getController().moveRightwards();
	}
}

public class TurnClockwiseCommand : ShipCommand {
	public TurnClockwiseCommand(CommandCreator creator) : base(creator, 5, "Turn Clockwise") { }

	public override bool execute(ref int programCounter) {
		return getController().turnClockwise();
	}
}

public class TurnCounterclockwiseCommand : ShipCommand {
	public TurnCounterclockwiseCommand(CommandCreator creator): base(creator, 6, "Turn Counterclockwise") { }

	public override bool execute(ref int programCounter) {
		return getController().turnCounterclockwise();
	}
}