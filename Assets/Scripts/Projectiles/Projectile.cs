using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int moveSpeed = 0;
	public int damage = 0;
	public int maxLifeSpan = 0;
	public Creature target;

	private void Start () {
		Destroy (gameObject, maxLifeSpan);
	}
	// Update is called once per frame
	private void Update () {
		if (target == null) {
			Destroy (gameObject);
		} else {
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {

		if (other.transform == target.transform) {
			CreatureHealth creature = other.GetComponent<CreatureHealth> ();
			if (creature){
				damageCreature (creature);
				Destroy (gameObject);
			}
		}
	}

	private void damageCreature (CreatureHealth creature) {
		creature.health -= damage;
	}
}
