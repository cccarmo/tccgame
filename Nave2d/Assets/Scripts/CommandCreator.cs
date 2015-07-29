using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate bool Command();

public class CommandCreator : MonoBehaviour {
	public GameObject spaceShip;
	private CommandInterpreter interpreter;
	public GameObject panel;
	private Dictionary<string, Command> actions;

	void Start() {
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		interpreter = panel.GetComponent<CommandInterpreter>();

		actions = new Dictionary<string, Command>();
		actions.Add("Shoot", new Command(spaceShipController.shoot));
		actions.Add("Move Forward", new Command(spaceShipController.moveForward));
		actions.Add("Move Backward", new Command(spaceShipController.moveBackward));
		actions.Add("Move Leftwards", new Command(spaceShipController.moveLeftwards));
		actions.Add("Move Rightwards", new Command(spaceShipController.moveRightwards));
		actions.Add("Turn Clockwise", new Command(spaceShipController.turnClockwise));
		actions.Add("Turn Counterclockwise", new Command(spaceShipController.turnCounterClockwise));
	}

	public void handleEvent(string eventType) {
		// FIXME: only if mouse position is inside panel area
		interpreter.addCommand(actions[eventType]);
	}
}
