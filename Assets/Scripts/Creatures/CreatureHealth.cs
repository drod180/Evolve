using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHealth : MonoBehaviour {

	public int health;
	public int healthDecayAmount;
	public int healthDecayRate;

	private float lastHealthUpdateTime;
	// Use this for initialization
	void Start () {
		health = 100;
		healthDecayAmount = 10;
		healthDecayRate = 5;
		lastHealthUpdateTime = Time.time;
	}
	
	// Update is called once per frame
	public void HealthUpdate () {
		updateHealth ();
		checkStatus ();
	}

	private void checkStatus () {
		if (health <= 0) {
			destroyCreature ();
		}
	}

	private void destroyCreature () {
		Destroy (gameObject);
	}

	private void updateHealth () {
		if (Time.time > lastHealthUpdateTime + healthDecayRate) {
			lastHealthUpdateTime = Time.time;

			health -= healthDecayAmount;
		}
	}
		

}
