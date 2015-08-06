using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate bool CommandCallback();

public class Command {
	CommandCallback callback;
	string label;
	
	public Command(CommandCallback callback, string label) {
		this.callback = callback;
		this.label = label;
	}
	
	public override string ToString() {
		return label;
	}

	public bool execute() {
		return callback ();
	}
}



public class CommandCreator : MonoBehaviour {
	public GameObject spaceShip;
	private CommandInterpreter interpreter;
	public GameObject panel;
	private Dictionary<string, Command> actions;
	
	void Start() {
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		interpreter = panel.GetComponent<CommandInterpreter>();
		
		actions = new Dictionary<string, Command>();
		actions.Add("Shoot", new Command(spaceShipController.shoot, "Shoot"));
		actions.Add("Move Forward", new Command(spaceShipController.moveForward, "Move Forward"));
		actions.Add("Move Backward", new Command(spaceShipController.moveBackward, "Move Backward"));
		actions.Add("Move Leftwards", new Command(spaceShipController.moveLeftwards, "Move Leftwards"));
		actions.Add("Move Rightwards", new Command(spaceShipController.moveRightwards, "Move Rightwards"));
		actions.Add("Turn Clockwise", new Command(spaceShipController.turnClockwise, "Turn Clockwise"));
		actions.Add("Turn Counterclockwise", new Command(spaceShipController.turnCounterClockwise, "Turn Counterclockwise"));
	}
	
	public void handleEvent(string eventType) {
		Rect panelRect = panel.transform.GetComponent<RectTransform>().rect;
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		if(panelRect.Contains(mousePos))
			interpreter.addCommand(actions[eventType]);
	}
}
