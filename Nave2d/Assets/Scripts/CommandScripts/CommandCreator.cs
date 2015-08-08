using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CommandCreator : MonoBehaviour {
	public GameObject spaceShip;
	private CommandInterpreter interpreter;
	public GameObject panel;
	private Dictionary<string, Command> actions;
	public GameObject[] availableBoxes;
	
	
	private bool KFunctionTrue() {
		return true;
	}
	
	private bool KFlowFunctionTrue(ref int programCounter) {
		return true;
	}
	
	void Start() {
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		interpreter = panel.GetComponent<CommandInterpreter>();
		
		actions = new Dictionary<string, Command>();

		//// Ship Commands
		actions.Add("Shoot", 
		            new Command(spaceShipController.shoot, KFlowFunctionTrue, "Shoot", availableBoxes[0], false));
		actions.Add("Move Forward",
		            new Command(spaceShipController.moveForward, KFlowFunctionTrue, "Move Forward", availableBoxes[1], false));
		actions.Add("Move Backward",
		            new Command(spaceShipController.moveBackward, KFlowFunctionTrue, "Move Backward", availableBoxes[2], false));
		actions.Add("Move Leftwards",
		            new Command(spaceShipController.moveLeftwards, KFlowFunctionTrue, "Move Leftwards", availableBoxes[3], false));
		actions.Add("Move Rightwards",
		            new Command(spaceShipController.moveRightwards, KFlowFunctionTrue, "Move Rightwards", availableBoxes[4], false));
		actions.Add("Turn Clockwise",
		            new Command(spaceShipController.turnClockwise, KFlowFunctionTrue, "Turn Clockwise", availableBoxes[5], false));
		actions.Add("Turn Counterclockwise",
		            new Command(spaceShipController.turnCounterClockwise, KFlowFunctionTrue, "Turn Counterclockwise", availableBoxes[6], false));

		//// Flow Commands
		actions.Add("Scoped Repetition",
		            new Command(KFunctionTrue, KFlowFunctionTrue, "Scoped Repetition", availableBoxes[7], true));
	}
	
	public void handleEvent(string eventType) {
		Rect panelRect = panel.transform.GetComponent<RectTransform>().rect;
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		if(panelRect.Contains(mousePos))
			interpreter.addCommand(actions[eventType]);
	}
}
