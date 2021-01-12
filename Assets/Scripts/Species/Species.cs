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
	public List<CreatureBase> creatureBaseList;
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
		{"armor", 1},
		{"damageReturn", 0},
		{"moveInterval", 4},
		{"moveSpeed", 3},
		{"moveRange", 4}
	};

	public int evolvePoints;

	public int population;

	private int evolveCount;
	// Use this for initialization
	void Start () {
		initializeSpecies ();
		spawnCreatureBase (startingPoint);
	}

	public void spawnCreatureBase (Vector2 spawnPosition) {
		CreatureBase newCreatureBase = (CreatureBase) Instantiate(creatureBase, spawnPosition, transform.rotation);
		newCreatureBase.species = this;
		newCreatureBase.transform.parent = this.transform;
		newCreatureBase.map = map;
		creatureBaseList.Add(newCreatureBase);
	}

	//Update attribute by either a full value, a percentage of the previous value or complete replacement
	public void updateAttribute (string attribute, float value, string type) {
		if (attributes.ContainsKey (attribute)) {
			switch (type) {
			case "percent":
				attributes [attribute] += Mathf.CeilToInt(attributes [attribute] * value);
				break;
			case "full":
				attributes [attribute] += Mathf.CeilToInt(value);
				break;
			case "swap":
				attributes [attribute] = Mathf.CeilToInt(value);
				break;
			default:
				break;
			}
		}
	
	}

	private void initializeSpecies () {
		evolvePoints = 0;
		evolveCount = 0;
		population = 0;
		traits = new HashSet<string>();
	}

	public void addEvolvePoints (int points) {
		evolvePoints += points;
		if(evolvePoints > evolveCount / 5 && evolvePoints % 5 == 0) {
			spendEvolvePoints();
		}
	}

	public void spendEvolvePoints () {
		evolveCount++;

		if (evolveCount % 5 == 0) {
			evolveRandomTrait();
		} else {
			evolveRandomAttribute("random");
		}  
	}

	public void evolveRandomTrait() {
		Debug.Log("Species: " + speciesNumber + " Evolved trait ");
	}
	public void evolveRandomAttribute(string attribute) {

		if (attribute == "random") {
			List<string> attributeList = new List<string>(attributes.Keys);			
			attribute = attributeList[Mathf.FloorToInt(Random.Range(0.00f, (float)attributeList.Count - 0.01f))];

		}

		switch (attribute) {
			case "attackSpeed":
				updateAttribute(attribute, 1, "full");
				break;
			case "projectileSpeed":
				updateAttribute(attribute, 1, "full");
				break;
			case "damage":
				updateAttribute(attribute, 2, "full");
				break;
			case "attackRange":
				updateAttribute(attribute, 1, "full");
				break;
			case "foodCapacity":
				updateAttribute(attribute, 1, "full");
				break;
			case "foodCollectRate":
				updateAttribute(attribute, 1, "full");
				break;
			case "foodCollectAmount":
				updateAttribute(attribute, 1, "full");
				break;
			case "health":
				updateAttribute(attribute, 10, "full");
				break;
			case "maxAge":
				updateAttribute(attribute, 10, "full");
				break;
			case "armor":
				updateAttribute(attribute, 10, "full");
				break;
			case "damageReturn":
				updateAttribute(attribute, 1, "full");
				break;
			case "moveInterval":
				if(attributes["moveInterval"] > 1) {
					updateAttribute(attribute, -1, "full");
				}
				break;
			case "moveSpeed":
					updateAttribute(attribute, 1, "full");
				break;
			case "moveRange":
					updateAttribute(attribute, 1, "full");
				break;
			default:
				break;
		}

		Debug.Log("Species: " + speciesNumber + " Evolved attribute " + attribute);
	}
		
}
