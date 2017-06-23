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
	}
}

public class MoveTraitMarathoner: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Marathoner");
	}
}


//Gathering traits
public class GatherTraitHerbivore: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Herbivore");
	}
}

public class GatherTraitOmnivore: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Omnivore");
	}
}

public class GatherTraitCarnivore: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Carnivore");
	}
}

public class GatherTraitScavanger: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Scavanger");
	}
}

public class GatherTraitGrazer: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Grazer");
	}
}

//Combat Traits
public class CombatTraitClaws: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Claws");
	}
}

public class CombatTraitSpit: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Spit");
	}
}

public class CombatTraitCharge: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Swim");
	}
}

public class CombatTraitPerception: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Charge");
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
	}
}

public class CombatTraitVenom: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Venom");
	}
}

//Defense Traits
public class DefenseTraitShell: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Shell");
	}
}

public class DefenseTraitSpikes: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Spikes");
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
	}
}

//Generic Traits
public class GenericTraitEfficient: Trait {
	public override void addTrait(Species species) {
		species.traits.Add ("Efficient");
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
