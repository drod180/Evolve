using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour {

	private CreatureHealth creatureHealth;
	private CreatureMovement creatureMovement;
	// Use this for initialization
	void Start () {
		creatureHealth = gameObject.GetComponent<CreatureHealth> ();
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
	}

	// Update is called once per frame
	void Update () {
		creatureHealth.healthUpdate ();
		creatureMovement.moveUpdate ();
	}
		
}
