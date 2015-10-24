using UnityEngine;
using UnityEngine.UI;



public abstract class ShipCommand : Command {
	protected ShipCommand(CommandCreator commandCreator, int boxPreFabIndex, string label) {
		this.commandBoxPreFab = commandCreator.availableBoxes[boxPreFabIndex];
		this.indentLevel = 0;
		this.label = label;
		this.repetitionCounter = 0;
		this.repetitionMax = 1;
	}

	protected PlayerController getController() {
		GameObject spaceShip = GameObject.FindWithTag("Player");
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		return spaceShipController;
	}

	protected bool finishedShipCommand(){
		repetitionCounter++;
		if (repetitionCounter < repetitionMax) {
			return false;
		} else {
			repetitionCounter = 0;
			return true;
		}
	}
}


public class ShieldCommand : ShipCommand {
	public ShieldCommand(CommandCreator creator) : base(creator, 0, "Shield") { }
	
	public override bool execute(ref int programCounter) {
		if (getController().activateShield())
			return finishedShipCommand();
		else return false;
	}
}

public class ShootCommand : ShipCommand {
	public ShootCommand(CommandCreator creator) : base(creator, 0, "Shoot") { }

	public override bool execute(ref int programCounter) {
		if (getController().shoot())
			return finishedShipCommand();
		else return false;
	}
}

public class MoveForwardCommand : ShipCommand {
	public MoveForwardCommand(CommandCreator creator) : base(creator, 1, "Move Forward") { }

	public override bool execute(ref int programCounter) {
		if (getController().moveForward())
			return finishedShipCommand();
		else return false;
	}
}

public class MoveBackwardCommand : ShipCommand {
	public MoveBackwardCommand(CommandCreator creator) : base(creator, 2, "Move Backward") { }

	public override bool execute(ref int programCounter) {
		if (getController().moveBackward())
			return finishedShipCommand();
		else return false;
	}
}

public class MoveLeftwardsCommand : ShipCommand {
	public MoveLeftwardsCommand(CommandCreator creator) : base(creator, 3, "Move Leftwards") { }

	public override bool execute(ref int programCounter) {
		if (getController().moveLeftwards())
			return finishedShipCommand();
		else return false;
	}
}

public class MoveRightwardsCommand : ShipCommand {
	public MoveRightwardsCommand(CommandCreator creator) : base(creator, 4, "Move Rightwards") { }

	public override bool execute(ref int programCounter) {
		if (getController().moveRightwards())
			return finishedShipCommand();
		else return false;
	}
}

public class TurnClockwiseCommand : ShipCommand {
	public TurnClockwiseCommand(CommandCreator creator) : base(creator, 5, "Turn Clockwise") { }

	public override bool execute(ref int programCounter) {
		if (getController().turnClockwise())
			return finishedShipCommand();
		else return false;
	}
}

public class TurnCounterclockwiseCommand : ShipCommand {
	public TurnCounterclockwiseCommand(CommandCreator creator): base(creator, 6, "Turn Counterclockwise") { }

	public override bool execute(ref int programCounter) {
		if (getController().turnCounterclockwise())
			return finishedShipCommand();
		else return false;
	}
}