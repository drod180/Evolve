using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour {

	public string speciesName = "Temp";
	public int speciesNumber;
	public Color speciesColor;
	public int populationLimit;
	public Vector2 startingPoint;
	public CreatureBase creatureBase;
	public HashSet<string> traits;
	public TileManager map;
	public Dictionary<string, int> attributes = new Dictionary<string, int> 
	{
		{"attackSpeed", 3},
		{"projectileSpeed", 5},
		{"damage", 10},
		{"attackRange", 5},
		{"foodCapacity", 5},
		{"foodCollectRate", 3},
		{"foodCollectAmount", 1},
		{"health", 50},
		{"maxAge", 50},
		{"moveInterval", 3},
		{"moveSpeed", 4},
		{"moveRange", 5}
	};

	private int population;
	// Use this for initialization
	void Start () {
		initializeAttributes ();
		spawnCreatureBase (startingPoint);
	}

	public void spawnCreatureBase (Vector2 spawnPosition) {
		CreatureBase newCreatureBase = (CreatureBase) Instantiate(creatureBase, spawnPosition, transform.rotation);
		newCreatureBase.species = this;
		newCreatureBase.transform.parent = this.transform;
		newCreatureBase.map = map;
	}

	private void initializeAttributes () {
		population = 0;
	}
}


//TODO- Add all of the creature attributes to the species for every time a newCreatureBase is added.