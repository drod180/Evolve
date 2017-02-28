using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour {

	public float moveInterval;
	public float moveSpeed;

	private Stack<Vector2> targetList;
	private Vector2 desiredPosition;
	private float lastPositionTime;

	// Use this for initialization
	void Awake () {
		moveInterval = 2;
		moveSpeed = 5;
		desiredPosition.x = Random.Range (-5.0f, 5.0f);
		desiredPosition.y = Random.Range (-5.0f, 5.0f);
		lastPositionTime = Time.time;
	}
	
	// Update is called once per frame
	public void moveUpdate () {
		if (Time.time > lastPositionTime + moveInterval) {
			lastPositionTime = Time.time;
			getNewPosition ();
		}
		move ();
	}

	/*
	 * Priorities: 0 - Wander, 1 - Food, 2 - Home Base
	 */
	public void addMoveLocation (Vector2 location, int priority = 0) {
		targetList.Push (location);
	}

	public void removeMoveLocation () {
		targetList.Pop ();
	}

	// Move to next position
	private void move () {
		transform.position = Vector2.MoveTowards (transform.position, desiredPosition, moveSpeed * Time.deltaTime);
	}

	private void getNewPosition () {

		if (targetList.Count == 0) {

			Vector2 newPosition;
			newPosition.x = Random.Range (-5.0f, 5.0f);
			newPosition.y = Random.Range (-5.0f, 5.0f);

			desiredPosition = newPosition;
		} else {
			desiredPosition = targetList.Peek ();
		}
	}
}
