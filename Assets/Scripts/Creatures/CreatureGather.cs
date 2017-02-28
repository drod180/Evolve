using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGather : MonoBehaviour {

	public int currentFood;
	public int foodCapacity;
	public int foodCollectRate;
	public int foodCollectAmount;
	public Vector2 foodLocation;

	// Use this for initialization
	void Awake () {
		currentFood = 0;
		foodCapacity = 5;
		foodCollectRate = 3;
		foodCollectAmount = 1;
	}

	public void gatherFood (Food foodSource) {
		//Collect either foodCollectAmount or remaining capacity which ever is smaller.
		int collectValue = foodCollectAmount > foodCapacity - currentFood ? foodCapacity - currentFood : foodCollectAmount;
		currentFood += foodSource.giveFood (collectValue);
	}

	//All bool to allow for giving up all food defaulted to false
	public int giveFood (int value, bool all = false) {
		if (all) {
			value = currentFood;
		}

		if (currentFood > value) {
			currentFood -= value;
			return value;
		} else {
			int temp = currentFood;
			currentFood = 0;
			return temp;
		}
	}
}
 