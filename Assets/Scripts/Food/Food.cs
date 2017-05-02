using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public int foodAmount = 0;
	public int decayRate = 10;
	public FoodManager manager;
	public Vector2 foodLocation;

	// Use this for initialization
	void Awake () {
		foodLocation = transform.position;
		InvokeRepeating ("decay", decayRate, decayRate);
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

	private void decay () {
		giveFood (1);
	}

	private void destroySelf () {
		Destroy (gameObject);
		manager.reducePopulation(1);
	}
}
