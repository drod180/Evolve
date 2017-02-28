using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

	private CreatureHealth creatureHealth;
	private CreatureMovement creatureMovement;
	private CreatureGather creatureGather;
	// Use this for initialization
	void Start () {
		creatureHealth = gameObject.GetComponent<CreatureHealth> ();
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
		creatureGather = gameObject.GetComponent<CreatureGather> ();
	}

	// Update is called once per frame
	void Update () {
		creatureHealth.HealthUpdate ();
		creatureMovement.moveUpdate ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log (other.tag);
	}
}
