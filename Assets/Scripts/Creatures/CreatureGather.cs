using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGather : MonoBehaviour {

	public bool collecting;
	public int currentFood;
	public int foodCapacity;
	public int foodCollectRate;
	public int foodCollectAmount;
	public Food foodGatheringSource;
	public Vector2 baseLocation;

	private CreatureMovement creatureMovement;

	// Use this for initialization
	void Awake () {
		currentFood = 0;
		foodCapacity = 5;
		foodCollectRate = 3;
		foodCollectAmount = 1;
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
	}

	public IEnumerator gatheringFood (Food foodSource) {
		collecting = true;
		foodGatheringSource = foodSource;
		while (collecting) {
			yield return new WaitForSeconds (foodCollectRate);
			gatherFood (foodGatheringSource);
		}
	}

	public void completeGathering () {
		collecting = false;
		creatureMovement.addMoveLocation (baseLocation, 2);
	}

	private void gatherFood (Food foodSource) {
		if (foodSource == null || foodSource.foodAmount == 0) {
			creatureMovement.removeMoveLocation (1);
			completeGathering ();
		} else if (currentFood == foodCapacity) {
			completeGathering ();
		} else {
			//Collect either foodCollectAmount or remaining capacity which ever is smaller.
			int collectValue = foodCollectAmount > foodCapacity - currentFood ? foodCapacity - currentFood : foodCollectAmount;
			currentFood += foodSource.giveFood (collectValue);
		}
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
 