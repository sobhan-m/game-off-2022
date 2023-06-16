using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ability
{
	public AbilityType type { get; protected set; }

	abstract public void Activate();
	abstract public void Deactivate();

}
