using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour {

	public GameObject creature;
	public Vector2 homeCoords;
	public float spawnRate;
	private int population;
	// Use this for initialization
	void Start () {
		population = 0;
		spawnRate = 10;
		InvokeRepeating ("Spawn", spawnRate, spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 	private void Spawn () {
		Vector2 spawnPosition = homeCoords;

		GameObject newCreature = (GameObject) Instantiate(creature, spawnPosition, transform.rotation);
		population++;
	}
}
