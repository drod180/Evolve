using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private List<Point> pointList;
	private Vector2 desiredPosition;

	private CreatureTarget finalTarget;
	private Vector2 nextTarget;

	CreatureGather creatureGather;

	// Use this for initialization
	void Awake () {
		targetList = new Stack<CreatureTarget> ();
		pointList = new List<Point> ();
		creatureGather = gameObject.GetComponent<CreatureGather> ();
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
	public bool removeMoveLocation (int prior) {
		if (targetList.Count > 0 && prior == targetList.Peek ().priority) {
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
		if (removeMoveLocation (prior)) {
			return addMoveLocation (location, prior);
		}
		return false;
	}

	// Move to next position
	private void move () {

		//Get/Check the final target
		if (targetList.Count == 0 || finalTarget.position != targetList.Peek ().position) { 
			if (targetList.Count == 0) {
				getNewRandomPosition ();
			}

			finalTarget = targetList.Peek ();
			pointList = getPathToTarget (transform.position, finalTarget.position);
			Debug.Log ("````````");
			Debug.Log("Start: <color=green>" + transform.position + "</color>");
			Debug.Log("Target: <color=red>" + finalTarget.position + "</color>");

			for (int i = 0; i < pointList.Count; i++) {
				Debug.Log (pointList[i]);
			}
			Debug.Log ("````````");
		}

		//Get/Check next target
		if (pointList.Count > 0) {
			Vector2 nextPoint = new Vector2 (pointList [0].column, pointList [0].row);
			if (nextTarget != nextPoint) {
				Debug.Log ("~~~~~~~~~~~~~");
				Debug.Log (nextTarget);
				Debug.Log (nextPoint);
				nextTarget = nextPoint;
			}
		} else {
			nextTarget = finalTarget.position;
		}


		//Move towards target
		transform.position = Vector2.MoveTowards (transform.position, nextTarget, moveSpeed * Time.deltaTime);
		faceObject ((Vector3)nextTarget);

		//check if at target
		if (pointList.Count > 0 && arrivedAtLocation (nextTarget)) {
			Debug.Log ("Arrived at Next Target!");
			pointList.RemoveAt (0);
		}

		if (arrivedAtLocation (finalTarget.position)) {
			Debug.Log ("Arrived at final Location!");
			removeMoveLocation (finalTarget.priority);
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
		

	private List<Point> getPathToTarget (Vector2 startingPoint, Vector2 destination) {
		Point start = new Point ((int)startingPoint.y, (int)startingPoint.x);
		Point end = new Point ((int)destination.y, (int)destination.x);

		return map.pathingGrid.getPath (start, end);
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
		if (position.x < 0 || 
			position.x > map.mapSize.x || 
			position.y < 0 || 
			position.y > map.mapSize.y || 
			map.mapValues[(int)position.x, (int)position.y] != 'G') {

			return false;
		}
		return true;
	}
		
}
