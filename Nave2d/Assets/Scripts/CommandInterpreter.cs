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

public enum TurnDirections {
	Clockwise,
	Counterclockwise
};

public class CommandInterpreter : MonoBehaviour {
	private uint countDown;
	private readonly uint executionTime = 25;
	private Command currentCommand;
	private LinkedList<Command> commandList;
	private Dictionary<Command, Vector2> direction;
	public GameObject spaceShip;
	private PlayerController spaceShipController;
	private Rigidbody2D spaceShipBody;
	private float seno;
	private float coseno;

	void Start () {
		spaceShipController = spaceShip.GetComponent<PlayerController> ();
		spaceShipBody = spaceShip.GetComponent<Rigidbody2D> ();
		commandList = new LinkedList<Command>();
		countDown = 0;
	}

	public void addCommand() {
		if(Input.GetKeyDown(KeyCode.Space))
			addShootCommand();
		if(Input.GetKeyDown(KeyCode.UpArrow))
			addGoForwardsCommand();
		if(Input.GetKeyDown(KeyCode.DownArrow))
			addGoBackwardsCommand();
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			addGoLeftwardsCommand();
		if(Input.GetKeyDown(KeyCode.RightArrow))
			addGoRightwardsCommand();
	}
	
	public void addShootCommand() {
		commandList.AddLast(Command.Shoot);
	}
	
	public void addGoForwardsCommand() {
		commandList.AddLast(Command.GoForwards);
	}
	
	public void addGoBackwardsCommand() {
		commandList.AddLast(Command.GoBackwards);
	}
	
	public void addGoLeftwardsCommand() {
		commandList.AddLast(Command.GoLeftwards);
	}
	
	public void addGoRightwardsCommand() {
		commandList.AddLast(Command.GoRightwards);
	}

	public void addTurnClockwiseCommand() {
		commandList.AddLast(Command.TurnClockwise);
	}

	public void addTurnCounterclockwisewardsCommand() {
		commandList.AddLast(Command.TurnCounterclockwise);
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
				spaceShipController.turnSpaceship(TurnDirections.Clockwise);
			} else if (currentCommand.Equals(Command.TurnCounterclockwise)) {
				countDown--;
				spaceShipController.turnSpaceship(TurnDirections.Counterclockwise);
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
	}
	
	private Command getNextCommand() {
		if (commandList.Count > 0) {
			Command nextCommand = commandList.First.Value;
			commandList.RemoveFirst();
			return nextCommand;
		}
		else return Command.Nope;
	}
	
	public void execute(bool startedSimulation) {
		if (startedSimulation)
			interpretCommand();
		else 
			addCommand();
	}
}
