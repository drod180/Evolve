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
		destroyCreatureCheck ();
	}

	private void checkStatus () {
		if (health <= 0) {
			destroyCreatureCheck ();
		}
	}
		

	private void updateAge () {
		age++;
		destroyCreatureCheck ();
	}

	private void destroyCreatureCheck () {
		if (age == maxAge || health <= 0) {
			Destroy (gameObject);
		}
	}

}
