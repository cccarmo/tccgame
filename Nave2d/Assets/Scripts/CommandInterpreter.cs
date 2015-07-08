using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Command {
	Nope,
	GoForwards,
	GoBackwards,
	GoLeftwards,
	GoRightwards,
	Shoot
};

public class CommandInterpreter : MonoBehaviour {
	private uint countDown;
	private readonly uint executionTime = 25;
	private Command currentCommand;
	private LinkedList<Command> commandList;
	public PlayerController spaceship;
	private Dictionary<Command, Vector2> direction;
	
	public CommandInterpreter() {
		commandList = new LinkedList<Command>();
		countDown = 0;
		
		direction = new Dictionary<Command, Vector2>();
		direction.Add(Command.GoForwards, new Vector2(0.0f, 1.0f));
		direction.Add(Command.GoBackwards, new Vector2(0.0f, -1.0f));
		direction.Add(Command.GoLeftwards, new Vector2(-1.0f, 0.0f));
		direction.Add(Command.GoRightwards, new Vector2(1.0f, 0.0f));
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
	
	private void interpretCommand() {
		if (countDown == 0) {
			countDown = executionTime;
			currentCommand = getNextCommand();
		}
		if (!currentCommand.Equals(Command.Nope)) {
			if(currentCommand.Equals(Command.Shoot)) {
				if (spaceship.shoot())
					countDown = 0;
			}
			else {
				countDown--;
				spaceship.moveSpaceship(direction[currentCommand], ((float) countDown)/executionTime);
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
		if(startedSimulation)
			interpretCommand();
		else addCommand();
	}
}
