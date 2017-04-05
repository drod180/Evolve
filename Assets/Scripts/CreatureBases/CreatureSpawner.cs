using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour {

	public GameObject creature;
	public Vector2 homeCoords;
	public float spawnRate;

	private int population;

	// Use this for initialization
	void Awake () {
		population = 0;
		spawnRate = 3;
		homeCoords = transform.position;
	}
		
 	public void Spawn () {
		Vector2 spawnPosition = homeCoords;

		GameObject newCreature = (GameObject) Instantiate(creature, spawnPosition, transform.rotation);
		population++;
		newCreature.GetComponent<CreatureGather> ().baseLocation = homeCoords;
	}
}
