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
	GoForward,
	GoBack,
	GoToLeft,
	GoToRight,
	Shoot
};

public class CommandInterpreter {
	private uint countDown;
	private static uint executionTime = 200;
	private Command currentCommand;
	private LinkedList<Command> commandList;
	private PlayerController parent;

	public CommandInterpreter(PlayerController playerController) {
		commandList = new LinkedList<Command>();
		countDown = 0;
		parent = playerController;
	}

	private void addCommand() {
		if(Input.GetKeyDown(KeyCode.Space))
		   commandList.AddLast(Command.Shoot);
		if(Input.GetKeyDown(KeyCode.DownArrow))
			commandList.AddLast(Command.GoBack);
		if(Input.GetKeyDown(KeyCode.UpArrow))
			commandList.AddLast(Command.GoForward);
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			commandList.AddLast(Command.GoToLeft);
		if(Input.GetKeyDown(KeyCode.RightArrow))
			commandList.AddLast(Command.GoToRight);
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
				parent.moveSpaceship(currentCommand);
				countDown--;
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
	public float speed;
	private Rigidbody2D body;
	public Boundary boundary;
	public float tilt;
	public GameObject Shot;
	public Transform ShotSpawn;
	public static float fireRate = 0.25f;
	public GameObject GameScreen;
	private CommandInterpreter interpreter;
	private float nextFire;
	private static AudioSource shootSound;
	private bool startedSimulation;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		interpreter = new CommandInterpreter(this);
		shootSound = GetComponent<AudioSource>();
		nextFire = 0.0f;
		startedSimulation = false;
	}

	void FixedUpdate() {

	//	call moveSpaceship() instead


		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		body.velocity = movement * speed;
		body.position = new Vector2 (Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax));
		body.rotation = body.velocity.x * (-tilt);
	}


	void Update() {
		if (!startedSimulation && Input.GetKeyDown(KeyCode.S))
			startedSimulation = true;
		interpreter.execute(startedSimulation);
	}
		
	public void moveSpaceship(Command direction) {
	/*	
	 	float fraction = ((float) count)/time;
		Vector2 movement = new Vector2(1.0f, 0.0f);
		
		speed *= fraction;
		count -= 10;
		body.velocity = movement * speed;
		body.rotation = body.velocity.x * (-tilt);
		body.position = new Vector2 (Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), 
		                             Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax));
		 */
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
