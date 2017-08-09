using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHealth : MonoBehaviour {

	public int health;
	public int age;
	public int maxAge;
	public HashSet<string> creatureTraits;

	private int ageRate;

	// Use this for initialization
	void Awake () {
		age = 0;
		ageRate = 10;
		InvokeRepeating ("updateAge", ageRate, ageRate);
	}
	
	// Update is called once per frame
	public void healthUpdate () {
		checkStatus ();
	}

	public void takeDamage (int damage) {
		health -= damage;
		destroyCreature ();
	}

	private void checkStatus () {
		if (health <= 0) {
			destroyCreature ();
		}
	}
		

	private void updateAge () {
		age++;
		destroyCreature ();
	}

	private void destroyCreature () {
		if (age == maxAge || health <= 0) {
			Destroy (gameObject);
		}
	}

}
