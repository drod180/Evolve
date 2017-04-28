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


	public Stack<CreatureTarget> targetList;
	private Vector2 desiredPosition;
	CreatureGather creatureGather;
	// Use this for initialization
	void Awake () {
		moveInterval = 2;
		moveSpeed = 5;
		targetList = new Stack<CreatureTarget> ();
		Vector2 currentPosition = transform.position;
		desiredPosition.x = Random.Range (currentPosition.x - 5.0f, currentPosition.x + 5.0f);
		desiredPosition.y = Random.Range (currentPosition.y - 5.0f, currentPosition.y + 5.0f);
		addMoveLocation (desiredPosition);
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
		if (prior == targetList.Peek ().priority) {
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
		Vector2 movePos = targetList.Peek ().position;
		int movePriority = targetList.Peek ().priority;
		transform.position = Vector2.MoveTowards (transform.position, movePos, moveSpeed * Time.deltaTime);

		if (movePriority == 0 && arrivedAtLocation ()) {
			removeMoveLocation (0);
			getNewPosition ();
		} else if (movePriority == 1 && arrivedAtLocation () && creatureGather.foodGatheringSource == null) {
			removeMoveLocation (1);
		}
	}

	private void getNewPosition () {

		if (targetList.Count == 0) {
			Vector2 currentPosition = transform.position;
			Vector2 newPosition;
			newPosition.x = Random.Range (currentPosition.x - 5.0f, currentPosition.x + 5.0f);
			newPosition.y = Random.Range (currentPosition.y - 5.0f, currentPosition.y + 5.0f);

			addMoveLocation (newPosition);
		} 
	}

	private bool arrivedAtLocation () {
		return targetList.Peek ().position == new Vector2 (transform.position.x, transform.position.y);
	}
}
