using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

	public int creatureTeam = 0;
	public Color creatureColor = new Color (1, 1, 1, 1);
	public Species species;

	private CreatureHealth creatureHealth;
	private CreatureMovement creatureMovement;
	private CreatureGather creatureGather;
	private CreatureCombat creatureCombat;

	// Use this for initialization
	void Awake () {
		setupCreature ();
	}

	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().color = creatureColor;
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
				BaseResources otherBase = other.GetComponent<BaseResources> ();
				if (otherBase) {
					otherBase.addFood (foodValue);
				}
				creatureMovement.removeMoveLocation (2);
			}
		} else if (other.tag == "Creature") {
			Creature creature = other.GetComponent<Creature> ();
			if (creature && !creatureCombat.fighting && creature.creatureTeam != creatureTeam) {
				creatureMovement.addMoveLocation (creature.transform.position, 3);
				StartCoroutine (creatureCombat.attackingOpponent (creature));
			}
		}
	}

	public void updateTraits () {
		creatureMovement.updateMovementTraits ();
	}

	private void setupCreature() {
		creatureHealth = gameObject.GetComponent<CreatureHealth> ();
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
		creatureGather = gameObject.GetComponent<CreatureGather> ();
		creatureCombat = gameObject.GetComponent<CreatureCombat> ();

		creatureHealth.creatureTraits = species.traits;
		creatureMovement.creatureTraits = species.traits;
		creatureGather.creatureTraits = species.traits;
		creatureCombat.creatureTraits = species.traits;

	}
		
}
