using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour {

	public int team;

	private BaseResources resourceManager;
	private CreatureSpawner spawner;

	// Use this for initialization
	void Start () {
		resourceManager = gameObject.GetComponent<BaseResources> ();
		spawner = gameObject.GetComponent<CreatureSpawner> ();
		spawner.team = team;
		InvokeRepeating ("Spawn", spawner.spawnRate, spawner.spawnRate);
	}

	void Spawn () {
		if (resourceManager.foodValue >= resourceManager.creatureCost) {
			resourceManager.removeFood (resourceManager.creatureCost);
			spawner.Spawn ();
		}
	}
}
