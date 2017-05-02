using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour {

	public int speciesNumber;
	public Color speciesColor;
	private BaseResources resourceManager;
	private CreatureSpawner spawner;

	// Use this for initialization
	void Start () {
		resourceManager = gameObject.GetComponent<BaseResources> ();
		spawner = gameObject.GetComponent<CreatureSpawner> ();
		spawner.speciesNumber = speciesNumber;
		spawner.speciesColor = speciesColor;
		InvokeRepeating ("Spawn", spawner.spawnRate, spawner.spawnRate);
	}

	void Spawn () {
		if (resourceManager.foodValue >= resourceManager.creatureCost) {
			resourceManager.removeFood (resourceManager.creatureCost);
			spawner.Spawn ();
		}
	}
}
