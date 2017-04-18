using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

	public GameObject food;
	public float spawnRate;
	// Use this for initialization
	void Start () {
		spawnRate = 10;
		InvokeRepeating ("Spawn", spawnRate, spawnRate);
	}
	
	// Update is called once per frame
	void Spawn () {
		Vector2 newPosition;
		newPosition.x = Random.Range (-5.0f, 5.0f);
		newPosition.y = Random.Range (-5.0f, 5.0f);
		GameObject newFood = (GameObject) Instantiate(food, newPosition, transform.rotation);
		//Stops warning for now - DELETE
		if (newFood) {
		}
	}
}
