using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public int foodAmount;
	public Vector2 foodLocation;

	// Use this for initialization
	void Awake () {
		foodAmount = 30;
		foodLocation = transform.position;
	}

	public int giveFood (int value) {
		if (foodAmount > value) {
			foodAmount -= value;
			return value;
		} else {
			int temp = foodAmount;
			foodAmount = 0;
			Invoke ("destroySelf", 1);
			return temp;
		}

	}

	private void destroySelf () {
		Destroy (gameObject);
	}
}
