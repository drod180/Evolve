using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour {

	public float moveInterval;
	public float moveSpeed;
	private Vector2 desiredPosition;
	private float lastPositionTime;

	// Use this for initialization
	void Start () {
		moveInterval = 2;
		moveSpeed = 5;
		desiredPosition.x = Random.Range (-5.0f, 5.0f);
		desiredPosition.y = Random.Range (-5.0f, 5.0f);
		lastPositionTime = Time.time;
	}
	
	// Update is called once per frame
	public void moveUpdate () {
		getNewPosition ();
		move ();
	}

	private void move () {
		transform.position = Vector2.MoveTowards (transform.position, desiredPosition, moveSpeed * Time.deltaTime);
	}

	private void getNewPosition () {
		if (Time.time > lastPositionTime + moveInterval) {
			lastPositionTime = Time.time;

			Vector2 newPosition;
			newPosition.x = Random.Range (-5.0f, 5.0f);
			newPosition.y = Random.Range (-5.0f, 5.0f);

			desiredPosition = newPosition;
		}
	}
}
