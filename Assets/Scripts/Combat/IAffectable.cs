using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffectable
{
	public void StoreEffect(MissileEffect missileEffect);
	public void ProcessEffects();
}
