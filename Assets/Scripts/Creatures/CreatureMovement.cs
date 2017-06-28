using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class CreatureMovement : MonoBehaviour {

	public class CreatureTarget
	{
		public Vector2 position;
		public int priority;

		public CreatureTarget(Vector2 pos, int prior) {
			position = pos;
			priority = prior;
		}
	}
	public float moveInterval;
	public float moveSpeed;
	public float moveRange;

	public TileManager map;

	public Stack<CreatureTarget> targetList;

	private List<GridPos> pointList;
	private Vector2 desiredPosition;

	private Vector2 finalTarget;
	private Vector2 nextTarget;

	CreatureGather creatureGather;

	// Use this for initialization
	void Awake () {
		targetList = new Stack<CreatureTarget> ();
		pointList = new List<GridPos> ();
		creatureGather = gameObject.GetComponent<CreatureGather> ();
	}

	void Start () {
		getNewRandomPosition ();
	}

	// Update is called once per frame
	public void moveUpdate () {
		move ();
	}

	/*
	 * Priorities: 0 - Wander, 1 - Food, 2 - Home Base
	 * Returns true if location was added, false if not
	 */
	public bool addMoveLocation (Vector2 location, int priority = 0) {
		if (targetList.Count == 0 || priority > targetList.Peek ().priority) {
			CreatureTarget newCreatureTarget = new CreatureTarget (location, priority);
			targetList.Push (newCreatureTarget);
			return true;
		}
		return false;
	}

	/*
	 * Priorities: 0 - Wander, 1 - Food, 2 - Home Base
	 * Returns true if location was removed, false if not
	 */
	public bool removeMoveLocation (int prior, bool forceRemoval = false) {
		if (targetList.Count > 0 && (prior == targetList.Peek ().priority || forceRemoval)) {
			targetList.Pop ();
			return true;
		}

		return false;
	}

	/*
	 * Priorities: 0 - Wander, 1 - Food, 2 - Home Base
	 * Returns true if location was updated, false if not
	 */
	public bool updateMoveLocation (Vector2 location, int prior) {
		removeMoveLocation (prior);
		return addMoveLocation (location, prior);
	}

	// Move to next position
	private void move () {
		//CONSIDER MODIFYING FINAL TARGET TO NOT BE AN OBJECT. NEED TO SIMPLIFY THIS FUNCTION AS IT GETS CALLED OFTEN
		//NEED TO FIX BACKGROUND TO BE STATIC IMAGE AS OPPOSED TO INDIVIDUAL TILES.
		int targetPriority = 0;
		//Get/Check the final target
		if (targetList.Count == 0 || finalTarget != targetList.Peek ().position) { 
			if (targetList.Count == 0) {
				getNewRandomPosition ();
			}

			if (targetList.Peek () != null) {
				finalTarget = targetList.Peek ().position;
				targetPriority = targetList.Peek ().priority;
				pointList.Clear ();
				pointList = getPathToTarget (transform.position, finalTarget);
			}

			if (pointList.Count == 0) {
				removeMoveLocation (1, true);
			}
		}

		//Get/Check next target
		if (pointList.Count > 0) {
			Vector2 nextPoint = new Vector2 (pointList [0].x, pointList [0].y);
			if (nextTarget != nextPoint) {
				nextTarget = nextPoint;
			}
		}

		//Move towards target
		transform.position = Vector2.MoveTowards (transform.position, nextTarget, moveSpeed * Time.deltaTime);
		faceObject ((Vector3)nextTarget);

		//check if at target
		if (pointList.Count > 0 && arrivedAtLocation (nextTarget)) {
			pointList.RemoveAt (0);
		}

		//Remove location if we arrive
		if (arrivedAtLocation (finalTarget)) {
			removeMoveLocation (targetPriority);
		}
			
	}

	private void getNewRandomPosition () {
		Vector2 currentPosition = transform.position;
		Vector2 newPosition = new Vector2 (-1, -1);

		while (!validPosition(newPosition)) {
			newPosition.x = Mathf.Round(Random.Range (currentPosition.x - moveRange, currentPosition.x + moveRange));
			newPosition.y = Mathf.Round(Random.Range (currentPosition.y - moveRange, currentPosition.y + moveRange));
		}
		addMoveLocation (newPosition);
	}
		

	private List<GridPos> getPathToTarget (Vector2 startingPoint, Vector2 destination) {
		GridPos start = new GridPos ((int)startingPoint.x, (int)startingPoint.y);
		GridPos end = new GridPos ((int)destination.x, (int)destination.y);
		map.jpParam.Reset (start, end);

		return JumpPointFinder.FindPath (map.jpParam);
	}

	//Determines if current location is target location or not
	private bool arrivedAtLocation (Vector2 targetLocation) {
		return targetLocation == new Vector2 (transform.position.x, transform.position.y);
	}

	private void faceObject (Vector3 target) {
		Vector3 vectorToTarget = target - transform.position;
		transform.LookAt(transform.position + new Vector3(0,0,1), vectorToTarget);
	}

	private bool validPosition(Vector2 position) {
		if ((int)position.x < 0 || 
			(int)position.x > (int)map.mapSize.x - 1|| 
			(int)position.y < 0 || 
			(int)position.y > (int)map.mapSize.y - 1|| 
			map.mapValues[(int)position.x, (int)position.y] != 'G') {

			return false;
		}
		return true;
	}
		


	//Tests
	private void getPathToTargetTest() {
//		List<GridPos> testList = getPathToTarget (new Vector2(0,0), new Vector2(map.mapSize.x - 1, map.mapSize.y - 1));
//
//		Debug.Log ("<color=red>Path to target (0, 0) to (" + (map.mapSize.x - 1)+ ", " + (map.mapSize.y - 1)+ ")</color>");
//		for (int i = 0; i < testList.Count; i++) {
//			Debug.Log (testList[i]);
//			Vector2 spawnPosition = new Vector2 (testList [i].x, testList [i].y);
//			GameObject newTile = (GameObject)Instantiate (debugTile1, spawnPosition, transform.rotation);
//			newTile.transform.parent = this.transform;
//		}
//		Debug.Log ("<color=red>~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~</color>");
//
//		testList = getPathToTarget (new Vector2(0,0), new Vector2(0, (map.mapSize.y - 1)));
//
//		Debug.Log ("<color=green>Path to target (0, 0) to 0, " + (map.mapSize.y - 1)+ "</color>");
//		for (int i = 0; i < testList.Count; i++) {
//			Debug.Log (testList[i]);
//			Vector2 spawnPosition = new Vector2 (testList [i].x, testList [i].y);
//			GameObject newTile = (GameObject)Instantiate (debugTile2, spawnPosition, transform.rotation);
//			newTile.transform.parent = this.transform;
//		}
//		Debug.Log ("<color=green>~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~</color>");
//
//		testList = getPathToTarget (new Vector2(0,0), new Vector2((map.mapSize.x - 1), 0));
//
//		Debug.Log ("<color=blue>Path to target 0, 0 to (" + (map.mapSize.x - 1)+ ", 0)</color>");
//		for (int i = 0; i < testList.Count; i++) {
//			Debug.Log (testList[i]);
//			Vector2 spawnPosition = new Vector2 (testList [i].x, testList [i].y);
//			GameObject newTile = (GameObject)Instantiate (debugTile3, spawnPosition, transform.rotation);
//			newTile.transform.parent = this.transform;
//		}
//		Debug.Log ("<color=blue>~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~</color>");

	}
}
