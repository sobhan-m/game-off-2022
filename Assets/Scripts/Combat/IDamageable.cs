using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	public Health RetrieveHealth();
	public void Die();
}
