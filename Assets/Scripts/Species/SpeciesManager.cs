using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesManager : MonoBehaviour {

	public string speciesName = "Temp";
	public int speciesNumber = 0;
	public Color speciesColor;
	public CreatureBase creatureBase;

	private int population;
	private int populationLimit;
	private Dictionary<int, float[]> speciesColors = new Dictionary<int, float[]> 
	{
		{ 0, new float[3] { 0, 0, 1 } },
		{ 1, new float[3] { 0, 1, 1 } },
		{ 2, new float[3] { 0, 1, 0 } },
		{ 3, new float[3] { 1, 0, 1 } },
		{ 4, new float[3] { 1, 0, 0 } },
		{ 5, new float[3] { 1, 0.92f, 0.016f } },
		{ 6, new float[3] { 1, 0.5f, 0 } },
		{ 7, new float[3] { 0.6f, 0, 1 } },
		{ 8, new float[3] { 0.1f, 0.2f, 0 } },
	};

	// Use this for initialization
	void Start () {
		initializeValues ();
		spawnCreatureBase (transform.position);
	}

	private Color getSpeciesColor (int number) {
		return new Color(speciesColors[number][0], speciesColors[number][1], speciesColors[number][2], 1);
	}

	private void initializeValues () {
		populationLimit = 100;
		population = 0;
		speciesName = "Temporary";
		speciesColor = getSpeciesColor (speciesNumber);
	}

	public void spawnCreatureBase (Vector2 spawnPosition) {
		CreatureBase newCreatureBase = (CreatureBase) Instantiate(creatureBase, spawnPosition, transform.rotation);
		newCreatureBase.speciesNumber = speciesNumber;
		newCreatureBase.speciesColor = speciesColor;
	}
}
