using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Command {
	Nope,
	GoForwards,
	GoBackwards,
	GoLeftwards,
	GoRightwards,
	Shoot,
	TurnClockwise,
	TurnCounterclockwise
};

public enum TurnDirection {
	Clockwise,
	Counterclockwise
};

public class CommandInterpreter : MonoBehaviour {
	private uint countDown;
	private readonly uint executionTime = 25;

	private Command currentCommand;
	public ArrayList commandList;
	private int nextCommandIndex;
	private readonly int maxCommands = 21;
	private bool startedSimulation;

	private Dictionary<Command, Vector2> direction;
	public GameObject spaceShip;
	private PlayerController spaceShipController;
	private Rigidbody2D spaceShipBody;
	private float seno;
	private float coseno;

	private readonly int columns = 3;
	private readonly int margin = 15;

	void Start() {
		spaceShipController = spaceShip.GetComponent<PlayerController>();
		spaceShipBody = spaceShip.GetComponent<Rigidbody2D> ();
		commandList = new ArrayList();
		resetSimulation();
	}

	private void resetSimulation() {
		nextCommandIndex = 0;
		countDown = 0;
		startedSimulation = false;
	}

	private void addCommand(Command command) {
		if(commandList.Count < maxCommands)
			commandList.Add(command);
	}

	public void addShootCommand() {
		addCommand(Command.Shoot);
	}
	
	public void addGoForwardsCommand() {
		addCommand(Command.GoForwards);
	}
	
	public void addGoBackwardsCommand() {
		addCommand(Command.GoBackwards);
	}
	
	public void addGoLeftwardsCommand() {
		addCommand(Command.GoLeftwards);
	}
	
	public void addGoRightwardsCommand() {
		addCommand(Command.GoRightwards);
	}

	public void addTurnClockwiseCommand() {
		addCommand(Command.TurnClockwise);
	}

	public void addTurnCounterClockwiseCommand() {
		addCommand(Command.TurnCounterclockwise);
	}
	
	private void interpretCommand() {
		if (countDown == 0) {
			countDown = executionTime;
			currentCommand = getNextCommand();
		}
		if (!currentCommand.Equals(Command.Nope)) {
			seno = Mathf.Sin (Mathf.Deg2Rad *(spaceShipBody.rotation - 90f));
			coseno = Mathf.Cos (Mathf.Deg2Rad * (spaceShipBody.rotation - 90f));
			if(currentCommand.Equals(Command.Shoot)) {
				if (spaceShipController.shoot())
					countDown = 0;
			}
			else if (currentCommand.Equals(Command.TurnClockwise)) {
				countDown--;
				spaceShipController.turnSpaceship(TurnDirection.Clockwise);
			} else if (currentCommand.Equals(Command.TurnCounterclockwise)) {
				countDown--;
				spaceShipController.turnSpaceship(TurnDirection.Counterclockwise);
			} 
			else if (currentCommand.Equals(Command.GoForwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(-coseno, -seno), ((float) countDown)/executionTime);
			}
			else if (currentCommand.Equals(Command.GoBackwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(coseno, seno), ((float) countDown)/executionTime);
			}
			else if (currentCommand.Equals(Command.GoLeftwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(seno, -coseno), ((float) countDown)/executionTime);
			}
			else if (currentCommand.Equals(Command.GoRightwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(-seno, coseno), ((float) countDown)/executionTime);
			}
		}
		else resetSimulation();
	}
	
	private Command getNextCommand() {
		if (nextCommandIndex < commandList.Count) {
			Command nextCommand = (Command) commandList[nextCommandIndex];
			nextCommandIndex++;
			return nextCommand;
		}
		else return Command.Nope;
	}
	
	public void execute() {
		if (startedSimulation)
			interpretCommand();
	}

	public void startSimulation() {
		if (!startedSimulation)
			startedSimulation = true;
	}

	public void OnGUI() {
		string label = "";
		Command command;
		for (int index = 0; index < commandList.Count; index++) {
			command = (Command) commandList[index];
			label = command.ToString();
			
			GUI.Box(new Rect(margin + (index % columns) * 105, margin + (index / columns) * 55, 100, 50), label);		
		}
	}
}
