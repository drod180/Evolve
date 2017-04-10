using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

	public int creatureTeam = 0;

	private CreatureHealth creatureHealth;
	private CreatureMovement creatureMovement;
	private CreatureGather creatureGather;
	private CreatureCombat creatureCombat;
	// Use this for initialization
	void Start () {
		creatureHealth = gameObject.GetComponent<CreatureHealth> ();
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
		creatureGather = gameObject.GetComponent<CreatureGather> ();
		creatureCombat = gameObject.GetComponent<CreatureCombat> ();
	}

	// Update is called once per frame
	void Update () {
		creatureHealth.healthUpdate ();
		creatureMovement.moveUpdate ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		Vector2 otherPos = new Vector2 (other.transform.position.x, other.transform.position.y);
		if (other.tag == "Food") {
			Food food = other.GetComponent<Food> ();
			creatureMovement.addMoveLocation (food.foodLocation, 1);
			StartCoroutine (creatureGather.gatheringFood (food));
		} else if (other.tag == "Base") {
			if (otherPos == creatureGather.baseLocation) {
				int foodValue = creatureGather.giveFood (1, true);
				other.GetComponent<BaseResources> ().addFood (foodValue);
				creatureMovement.removeMoveLocation (2);
			}
		} else if (other.tag == "Creature") {
			Creature creature = other.GetComponent<Creature> ();
			if (!creatureCombat.fighting && creature.creatureTeam != creatureTeam) {
				creatureMovement.addMoveLocation (creature.transform.position, 3);
				StartCoroutine (creatureCombat.attackingOpponent (creature));
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
	}

		
}
