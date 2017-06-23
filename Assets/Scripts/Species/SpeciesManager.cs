using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesManager : MonoBehaviour {

	public Species species;
	public int startingPopulationLimit = 100;
	public TileManager map;
	private List<Species> speciesList = new List<Species>();
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
	}
		
	public void createAllSpecies (Vector2[] startingPoints) {
		for (int i = 0; i < startingPoints.Length; i++) {
			createSpecies (i, startingPoints [i]);
		}
	}

	private Color getSpeciesColor (int number) {
		return new Color(speciesColors[number][0], speciesColors[number][1], speciesColors[number][2], 1);
	}

	private void createSpecies (int number, Vector2 startingPoint) {
		Species newSpecies = (Species) Instantiate(species, startingPoint, transform.rotation);
		newSpecies.speciesNumber = number;
		newSpecies.speciesColor = getSpeciesColor(number);
		newSpecies.populationLimit = startingPopulationLimit;
		newSpecies.startingPoint = startingPoint;
		newSpecies.transform.parent = this.transform;
		newSpecies.map = map;
		speciesList.Add (newSpecies);
	}


}
