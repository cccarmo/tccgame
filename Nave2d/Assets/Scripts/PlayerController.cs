using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public enum Command {
	Nope,
	GoForwards,
	GoBackwards,
	GoLeftwards,
	GoRightwards,
	Shoot
};

public class CommandInterpreter {
	private uint countDown;
	private readonly uint executionTime = 25;
	private Command currentCommand;
	private LinkedList<Command> commandList;
	private PlayerController parent;
	private Dictionary<Command, Vector2> direction;

	public CommandInterpreter(PlayerController playerController) {
		commandList = new LinkedList<Command>();
		countDown = 0;
		parent = playerController;

		direction = new Dictionary<Command, Vector2>();
		direction.Add(Command.GoForwards, new Vector2(0.0f, 1.0f));
		direction.Add(Command.GoBackwards, new Vector2(0.0f, -1.0f));
		direction.Add(Command.GoLeftwards, new Vector2(-1.0f, 0.0f));
		direction.Add(Command.GoRightwards, new Vector2(1.0f, 0.0f));
	}

	private void addCommand() {
		if(Input.GetKeyDown(KeyCode.Space))
		   commandList.AddLast(Command.Shoot);
		if(Input.GetKeyDown(KeyCode.UpArrow))
			commandList.AddLast(Command.GoForwards);
		if(Input.GetKeyDown(KeyCode.DownArrow))
			commandList.AddLast(Command.GoBackwards);
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			commandList.AddLast(Command.GoLeftwards);
		if(Input.GetKeyDown(KeyCode.RightArrow))
			commandList.AddLast(Command.GoRightwards);
	}

	private void interpretCommand() {
		if (countDown == 0) {
			countDown = executionTime;
			currentCommand = getNextCommand();
		}
		if (!currentCommand.Equals(Command.Nope)) {
			if(currentCommand.Equals(Command.Shoot)) {
				if (parent.executeShoot())
					countDown = 0;
			}
			else {
				countDown--;
				parent.moveSpaceship(direction[currentCommand], ((float) countDown)/executionTime);
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

public class PlayerController : MonoBehaviour {
	private Rigidbody2D body;
	public Boundary boundary;

	public readonly float fixedSpeed = 5.0f;
	public readonly float tilt = 3.0f;
	public readonly float fireRate = 0.25f;
	private float nextFire;
	public GameObject Shot;
	public Transform ShotSpawn;

	public GameObject GameScreen;
	private CommandInterpreter interpreter;
	private AudioSource shootSound;
	private bool startedSimulation;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		interpreter = new CommandInterpreter(this);
		shootSound = GetComponent<AudioSource>();
		nextFire = 0.0f;
		startedSimulation = false;
		body.position = new Vector2(boundary.xMin, boundary.yMin);
	}
	
	void Update() {
		if (!startedSimulation && Input.GetKeyDown(KeyCode.S))
			startedSimulation = true;
		interpreter.execute(startedSimulation);
	}
	
	public void moveSpaceship(Vector2 direction, float intensity) {
		body.velocity = direction * fixedSpeed * intensity;
		body.rotation = body.velocity.x * (-tilt);
		body.position = new Vector2 (Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), 
		                             Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax));
	}

	public bool executeShoot() {
		if (Time.time > nextFire) {
			shootSound.Play();
			nextFire = Time.time + fireRate;
			GameObject newShot = GameObject.Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation) as GameObject;
			newShot.transform.parent = GameScreen.transform;
			return true;
		}
		else return false;
	}
}
