using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate bool CommandCallback();

public class Command {
	private CommandCallback callback;
	private string label;
	private GameObject commandBoxPreFab;
	
	public Command(CommandCallback callback, string label, GameObject boxPreFab) {
		this.callback = callback;
		this.label = label;
		this.commandBoxPreFab = boxPreFab;
	}
	
	public override string ToString() {
		return label;
	}

	public bool execute() {
		return callback();
	}

	public GameObject getCommandBoxPreFab() {
		return commandBoxPreFab;
	}
}



public class CommandCreator : MonoBehaviour {
	public GameObject spaceShip;
	private CommandInterpreter interpreter;
	public GameObject panel;
	private Dictionary<string, Command> actions;
	public GameObject[] availableBoxes;
	
	void Start() {
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		interpreter = panel.GetComponent<CommandInterpreter>();
		
		actions = new Dictionary<string, Command>();
		actions.Add("Shoot", 
		            new Command(spaceShipController.shoot, "Shoot", availableBoxes[0]));
		actions.Add("Move Forward",
		            new Command(spaceShipController.moveForward, "Move Forward", availableBoxes[1]));
		actions.Add("Move Backward",
		            new Command(spaceShipController.moveBackward, "Move Backward", availableBoxes[2]));
		actions.Add("Move Leftwards",
		            new Command(spaceShipController.moveLeftwards, "Move Leftwards", availableBoxes[3]));
		actions.Add("Move Rightwards",
		            new Command(spaceShipController.moveRightwards, "Move Rightwards", availableBoxes[4]));
		actions.Add("Turn Clockwise",
		            new Command(spaceShipController.turnClockwise, "Turn Clockwise", availableBoxes[5]));
		actions.Add("Turn Counterclockwise",
		            new Command(spaceShipController.turnCounterClockwise, "Turn Counterclockwise", availableBoxes[6]));
	}
	
	public void handleEvent(string eventType) {
		Rect panelRect = panel.transform.GetComponent<RectTransform>().rect;
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		if(panelRect.Contains(mousePos))
			interpreter.addCommand(actions[eventType]);
	}
}
