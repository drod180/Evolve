using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResources : MonoBehaviour {

	public int foodValue;
	public int creatureCost;
	public int health;

	// Use this for initialization
	void Awake () {
		health = 1000;
		foodValue = 10;
		creatureCost = 10;
	}

	public void addFood (int value) {
		foodValue += value;
	}

	public void removeFood (int value) {
		foodValue = value < foodValue ? foodValue - value : 0;
	}
}
