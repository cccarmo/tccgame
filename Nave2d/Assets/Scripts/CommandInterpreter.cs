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
	private float sin;
	private float cosin;

	private GUIStyle highlightStyle, ordinaryStyle;

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
			sin = Mathf.Sin (Mathf.Deg2Rad *(spaceShipBody.rotation - 90f));
			cosin = Mathf.Cos (Mathf.Deg2Rad * (spaceShipBody.rotation - 90f));
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
				spaceShipController.moveSpaceship(new Vector2(-cosin, -sin), ((float) countDown)/executionTime);
			}
			else if (currentCommand.Equals(Command.GoBackwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(cosin, sin), ((float) countDown)/executionTime);
			}
			else if (currentCommand.Equals(Command.GoLeftwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(sin, -cosin), ((float) countDown)/executionTime);
			}
			else if (currentCommand.Equals(Command.GoRightwards)) {
				countDown--;
				spaceShipController.moveSpaceship(new Vector2(-sin, cosin), ((float) countDown)/executionTime);
			}
		}
		else resetSimulation(); // restart stage as well
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
		else {
			startedSimulation = false;
			resetSimulation(); // restart stage as well
		}
	}

	private void defineGUIStyles() {
		if(highlightStyle == null && ordinaryStyle == null) {
			highlightStyle = new GUIStyle(GUI.skin.button);
			highlightStyle.alignment = TextAnchor.MiddleCenter;
			highlightStyle.hover = highlightStyle.normal;
			ordinaryStyle = new GUIStyle(GUI.skin.box);
			ordinaryStyle.alignment = TextAnchor.MiddleCenter;
		}
	}

	public void OnGUI() {
		Command command;
		string label = "";
		int highlight = nextCommandIndex - 1, margin = 15, columns = 3;

		defineGUIStyles();
		for (int index = 0; index < commandList.Count; index++) {
			command = (Command) commandList[index];
			label = command.ToString();

			if(index == highlight)
				GUI.Box(new Rect(margin + (index % columns) * 105, margin + (index / columns) * 55, 100, 50), label, highlightStyle);
			else
				GUI.Box(new Rect(margin + (index % columns) * 105, margin + (index / columns) * 55, 100, 50), label, ordinaryStyle);

			// Maybe we should use windows instead to implement removal: GUI.Window(id, Rect, function, label);
		}
	}
}
