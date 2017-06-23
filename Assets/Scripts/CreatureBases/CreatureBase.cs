using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour {

	public Species species;

	public Vector2 homeCoords;
	public float spawnRate;
	public int population;
	public Creature creature;
	public TileManager map;

	private BaseResources resourceManager;
	// Use this for initialization
	void Start () {
		initializeAttributes ();
		InvokeRepeating ("spawn", spawnRate, spawnRate);
	}

	void spawn () {
		if (resourceManager.foodValue >= resourceManager.creatureCost) {
			resourceManager.removeFood (resourceManager.creatureCost);
			spawnCreature ();
		}
	}

	void spawnCreature () {
		Vector2 spawnPosition = homeCoords;

		Creature newCreature = (Creature) Instantiate(creature, spawnPosition, transform.rotation);
		initializeCreature (newCreature);
		population++;
	}

	private void initializeAttributes() {
		resourceManager = gameObject.GetComponent<BaseResources> ();
		homeCoords = transform.position;
		population = 0;
		spawnRate = 3;
	}

	private void initializeCreature(Creature newCreature) {
		Creature creature = newCreature.GetComponent<Creature> ();
		CreatureGather creatureGather = newCreature.GetComponent<CreatureGather> ();
		CreatureCombat creatureCombat = newCreature.GetComponent<CreatureCombat> ();
		CreatureMovement creatureMovement = newCreature.GetComponent<CreatureMovement> ();
		CreatureHealth creatureHealth = newCreature.GetComponent<CreatureHealth> ();

		newCreature.transform.parent = this.transform;

		creature.creatureTeam = species.speciesNumber;
		creature.creatureColor = species.speciesColor;

		creatureGather.baseLocation = homeCoords;
		creatureGather.foodCapacity = species.attributes ["foodCapacity"];
		creatureGather.foodCollectRate = species.attributes ["foodCollectRate"];
		creatureGather.foodCollectAmount = species.attributes ["foodCollectAmount"];

		creatureHealth.health = species.attributes ["health"];
		creatureHealth.maxAge = species.attributes ["maxAge"];

		creatureCombat.attackSpeed = species.attributes ["attackSpeed"];
		creatureCombat.projectileSpeed = species.attributes ["projectileSpeed"];
		creatureCombat.damage = species.attributes ["damage"];
		creatureCombat.attackRange = species.attributes ["attackRange"];

		creatureMovement.moveInterval = (float) species.attributes ["moveInterval"];
		creatureMovement.moveSpeed = (float) species.attributes ["moveSpeed"];
		creatureMovement.moveRange = (float) species.attributes ["moveRange"];
		creatureMovement.map = map;
	}

}
