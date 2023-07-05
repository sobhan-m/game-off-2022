using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
	private ParticleSystem particles;
	private void Awake()
	{
		if (!TryGetComponent<ParticleSystem>(out particles))
		{
			throw new MissingComponentException("No Particle System attached.");
		}
	}

	private void Update()
	{
		if (!particles.IsAlive())
		{
			Destroy(this.gameObject);
		}
	}
}
