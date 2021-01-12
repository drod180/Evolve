using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHealth : MonoBehaviour {

	public int health;
	public int age;
	public int maxAge;
	public int armor;
	public int damageReturn;
	public HashSet<string> creatureTraits;

	public Species species;
	private int ageRate;

	// Use this for initialization
	void Awake () {
		age = 0;
		ageRate = 10;
		InvokeRepeating ("updateAge", ageRate, ageRate);
	}
	
	// Update is called once per frame
	public void healthUpdate () {
	}

	public void takeDamage (int damage) {
		health -= damage;
		if (health <= 0) {
			destroyCreature ();
		}
	}

	private void updateAge () {
		age++;
		if (age >= maxAge) {
			destroyCreature();
		}
	}

	private void destroyCreature () {
		Destroy (gameObject);
		species.population--;
	}

}
