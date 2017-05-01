using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHealth : MonoBehaviour {

	public int health;
	public int age;
	public int maxAge;
	public int ageRate;

	// Use this for initialization
	void Awake () {
		health = 100;
		age = 0;
		maxAge = 100;
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
