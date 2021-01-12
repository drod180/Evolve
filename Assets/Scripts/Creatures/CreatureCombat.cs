using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCombat : MonoBehaviour {

	public int attackSpeed;
	public GameObject projectile;
	public int projectileSpeed;
	public int damage;
	public int attackRange;
	public bool fighting;
	public Creature combatOpponent;
	public HashSet<string> creatureTraits;

	public Species species;

	private CreatureMovement creatureMovement;
	// Use this for initialization
	void Awake () {
		creatureMovement = gameObject.GetComponent<CreatureMovement> ();
	}
		
	public IEnumerator attackingOpponent (Creature opponent) {
		fighting = true;
		combatOpponent = opponent;
		while (fighting) {
			yield return new WaitForSeconds (attackSpeed);
			attackOpponent (opponent);
		}
	}

	private void attackOpponent (Creature opponent) {
		if (opponent == null) {
			fighting = false;
			creatureMovement.removeMoveLocation (3);
			species.addEvolvePoints(1);
		} else {
			creatureMovement.updateMoveLocation (opponent.transform.position, 3);
			GameObject newProjectile = (GameObject) Instantiate(projectile, transform.position, transform.rotation);
			Projectile tempProj = newProjectile.GetComponent<Projectile> ();
			tempProj.moveSpeed = projectileSpeed;
			tempProj.damage = damage;
			tempProj.maxLifeSpan = attackRange / projectileSpeed;
			tempProj.target = opponent;
		}

	}
}
