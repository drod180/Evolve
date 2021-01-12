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
		initializeCreature();
	}

	void Start () {
		setupCreature ();
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

	public void updateAllValues () {
		updateMovementValues ();
		updateCombatValues ();
		updateGatherValues ();
		updateHealthValues ();
	}

	public void updateMovementValues () {
		creatureMovement.moveInterval = species.attributes ["moveInterval"];
		creatureMovement.moveSpeed = species.attributes ["moveSpeed"];
		creatureMovement.moveRange = species.attributes ["moveRange"];
	}

	public void updateCombatValues () {
		creatureCombat.attackSpeed = species.attributes["attackSpeed"];
		creatureCombat.projectileSpeed = species.attributes["projectileSpeed"];
		creatureCombat.damage = species.attributes["damage"];
		creatureCombat.attackRange = species.attributes["attackRange"];
	}

	public void updateGatherValues () {
		creatureGather.foodCapacity = species.attributes ["foodCapacity"];
		creatureGather.foodCollectRate = species.attributes["foodCollectionRate"];
		creatureGather.foodCollectAmount = species.attributes["foodCollectAmount"];
	}

	public void updateHealthValues () {
		creatureHealth.health = species.attributes["health"];
		creatureHealth.maxAge = species.attributes["maxAge"];
		creatureHealth.armor = species.attributes["armor"];
		creatureHealth.damageReturn = species.attributes["damageReturn"];
	}
		
	private void initializeCreature() {
		creatureHealth = gameObject.GetComponent<CreatureHealth> ();
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
		creatureGather = gameObject.GetComponent<CreatureGather> ();
		creatureCombat = gameObject.GetComponent<CreatureCombat> ();
	}

	private void setupCreature() {
		creatureHealth.creatureTraits = species.traits;
		creatureHealth.species = species;
		creatureMovement.creatureTraits = species.traits;
		creatureGather.creatureTraits = species.traits;
		creatureCombat.creatureTraits = species.traits;
		creatureCombat.species = species;

	}
		
}
