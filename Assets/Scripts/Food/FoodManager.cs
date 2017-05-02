using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

	public Food food;
	public string foodType = "fruit";

	private int spawnRate = 0;
	private int spawnRange = 0;
	private int population = 0;
	private int populationLimit = 0;
	private int startingAmount = 0;
	private int decayRate = 0;

	private Dictionary<string, Dictionary<string, int>> foodSpawnValues = new Dictionary<string, Dictionary<string, int>>
	{
		{ "fruit", new Dictionary<string, int>
			{ 
				{"spawnRate", 15}, 
				{"spawnRange", 10}, 
				{"populationLimit", 15}, 
				{"startingAmount", 25},
				{"decayRate", 20}
			}
		},{ "plant", new Dictionary<string, int>
			{ 
				{"spawnRate", 10}, 
				{"spawnRange", 20}, 
				{"populationLimit", 30}, 
				{"startingAmount", 10},
				{"decayRate", 30}
			}
		},{ "fungus", new Dictionary<string, int>
			{ 
				{"spawnRate", 5}, 
				{"spawnRange", 5}, 
				{"populationLimit", 30}, 
				{"startingAmount", 5},
				{"decayRate", 10}
			}
		},{ "bacteria", new Dictionary<string, int>
			{ 
				{"spawnRate", 3}, 
				{"spawnRange", 3}, 
				{"populationLimit", 40}, 
				{"startingAmount", 3},
				{"decayRate", 5}
			}
		}
	};


	// Use this for initialization
	void Start () {
		initializeValues (foodType);
		population = 0;
		InvokeRepeating ("Spawn", spawnRate, spawnRate);
	}

	public void reducePopulation (int value) {
		population -= value;
	}

	// Update is called once per frame
	private void Spawn () {
		if (population < populationLimit) {
			Vector2 currentPosition = transform.position;
			Vector2 newPosition;
			newPosition.x = Random.Range (currentPosition.x - (float)spawnRange, currentPosition.x + (float)spawnRange);
			newPosition.y = Random.Range (currentPosition.y - (float)spawnRange, currentPosition.y + (float)spawnRange);
			Food newFood = (Food)Instantiate (food, newPosition, transform.rotation);
			newFood.manager = this;
			newFood.foodAmount = startingAmount;
			newFood.decayRate = decayRate;

			population++;
		}
	}

	private void initializeValues (string foodType) {
		if (foodSpawnValues.ContainsKey(foodType)) {
			spawnRate = foodSpawnValues [foodType] ["spawnRate"];
			spawnRange = foodSpawnValues [foodType] ["spawnRange"];
			populationLimit = foodSpawnValues [foodType] ["populationLimit"];
			startingAmount = foodSpawnValues [foodType] ["startingAmount"];
			decayRate = foodSpawnValues [foodType] ["decayRate"];
		} else {
			Debug.LogError ("Invalid food type");
		}
	}



	private void destroySelf () {
		Destroy (gameObject);
	}
}
