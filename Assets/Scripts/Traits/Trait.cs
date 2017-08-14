using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trait : MonoBehaviour {

	public abstract void addTrait (Species species);
}

//Movement Traits

//Gives creature the ability to fly and avoid terrain as well as avoid non-reach attacks
public class MoveTraitFly: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Fly");
	}
}

public class MoveTraitSwim: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Swim");
	}
}

public class MoveTraitClimb: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Climb");
	}
}

public class MoveTraitSprinter: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Sprinter");
		species.updateAttribute ("moveSpeed", 3, "full");
		species.updateAttribute ("moveRange", -2, "full");
	}
}

public class MoveTraitMarathoner: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Marathoner");
		species.updateAttribute ("moveSpeed", -1, "full");
		species.updateAttribute ("moveRange", 3, "full");
	}
}


//Gathering traits
public class GatherTraitHerbivore: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Herbivore");
		species.updateAttribute ("foodCollectRate", 1, "full");
		species.updateAttribute ("foodCapacity", 2, "full");
		species.updateAttribute ("foodCollectAmount", 1, "full");
	}
}

public class GatherTraitOmnivore: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Omnivore");
		species.updateAttribute ("foodCollectRate", 1, "full");
		species.updateAttribute ("foodCapacity", -1, "full");
	}
}

public class GatherTraitCarnivore: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Carnivore");
		species.updateAttribute ("foodCollectRate", -1, "full");
		species.updateAttribute ("foodCapacity", 1, "full");
		species.updateAttribute ("foodCollectAmount", -1, "full");
	}
}

public class GatherTraitScavanger: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Scavanger");
		species.updateAttribute ("foodCollectRate", 0.5f, "percentage");
	}
}

public class GatherTraitGrazer: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Grazer");
		species.updateAttribute ("foodCapacity", 0.3f, "percentage");
		species.updateAttribute ("foodCollectAmount", 0.75f, "percentage");
	}
}

//Combat Traits
public class CombatTraitClaws: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Claws");
		species.updateAttribute ("damage", 3, "full");
		species.updateAttribute ("attackSpeed", -2, "full");
		species.updateAttribute ("attackRange", -1, "full");
	}
}

public class CombatTraitSpit: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Spit");
		species.updateAttribute ("damage", 1, "full");
		species.updateAttribute ("attackRange", 3, "full");
		species.updateAttribute ("projectileSpeed", 2, "full");
	}
}

public class CombatTraitCharge: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Charge");
		species.updateAttribute ("damage", 4, "full");
		species.updateAttribute ("attackRange", -2, "full");
	}
}

public class CombatTraitPerception: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Perception");
	}
}

public class CombatTraitFrenzy: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Frenzy");
	}
}

public class CombatTraitReach: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Reach");
		species.updateAttribute ("attackRange", 3, "full");
		species.updateAttribute ("projectileSpeed", 2, "full");
	}
}

public class CombatTraitVenom: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Venom");
		species.updateAttribute ("damage", 2, "full");
	}
}

//Defense Traits
public class DefenseTraitShell: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Shell");
		species.updateAttribute ("armor", 3, "full");
	}
}

public class DefenseTraitSpikes: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Spikes");
		species.updateAttribute ("armor", 1, "full");
		species.updateAttribute ("damageReturn", 3, "full");
	}
}

public class DefenseTraitFlee: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Flee");
	}
}

public class DefenseTraitHide: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Hide");
	}
}

public class DefenseTraitRoar: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Roar");
		species.updateAttribute ("health", 10, "full");
	}
}

public class DefenseTraitCamouflage: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Camouflage");
	}
}

public class DefenseTraitPoision: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Posion");
		species.updateAttribute ("damageReturn", 1, "full");
	}
}

//Generic Traits
public class GenericTraitEfficient: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Efficient");
		species.updateAttribute ("foodCollectRate", 2, "full");
		species.updateAttribute ("moveInterval", -2, "full");
	}
}

public class GenericTraitLiveBirth: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("LiveBirth");
	}
}

public class GenericTraitNesting: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Nesting");
	}
}

public class GenericTraitPack: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Pack");
	}
}

public class GenericTraitSolitary: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Solitary");
	}
}

public class GenericTraitHive: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Hive");
	}
}

public class GenericTraitTerritorial: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Territorial");
	}
}

public class GenericTraitInvasive: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Invasive");
	}
}

public class GenericTraitInstinctive: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Instictive");
	}
}

public class GenericTraitIntelligent: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Intelligent");
	}
}

public class GenericTraitMigratory: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Migratory");
	}
}
