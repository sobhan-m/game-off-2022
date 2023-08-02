using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
	[SerializeField] public AvailableAbilities available;
	private Dictionary<AbilityType, Ability> abilities;
	private PlayerInputActions actions;
	public Meter healCooldown { get; private set; }
	public Meter rageCooldown { get; private set; }
	public Meter rageDuration { get; private set; }
	private bool isRaging;
	public Meter shieldCooldown { get; private set; }
	public bool hasShield { get; private set; }
	public Meter entangleCooldown { get; private set; }
	public Meter entangleDuration { get; private set; }
	private bool isEntangling;

	private void Awake()
	{
		abilities = new Dictionary<AbilityType, Ability>();

		if (available.hasHeal)
		{
			PlayerHealthManager player = FindObjectOfType<PlayerHealthManager>();
			if (!player)
			{
				throw new MissingReferenceException("No players in scene.");
			}

			abilities.Add(AbilityType.CLERIC, new HealAbility(available.healAmount, player, available.healVFX));
			healCooldown = new Meter(0, available.healCooldown, available.healCooldown);
		}
		if (available.hasRage)
		{
			abilities.Add(AbilityType.BARBARIAN, new RageAbility(available.rageMultiplier, this, available.rageVFX));
			rageCooldown = new Meter(0, available.rageCooldown, available.rageCooldown);
			rageDuration = new Meter(0, available.rageDuration);
			isRaging = false;
		}
		if (available.hasShield)
		{
			abilities.Add(AbilityType.WIZARD, new ShieldAbility(this, available.shieldPrefab));
			shieldCooldown = new Meter(0, available.shieldCooldown, available.shieldCooldown);
			hasShield = false;
		}
		if (available.hasEntangle)
		{
			abilities.Add(AbilityType.DRUID, new EntangleAbility(available.entangleDuration, available.effectPrefab));
			entangleCooldown = new Meter(0, available.entangleCooldown, available.entangleCooldown);
			entangleDuration = new Meter(0, available.entangleDuration);
		}

		actions = new PlayerInputActions();
		actions.Combat.HealAbility.performed += Heal;
		actions.Combat.RageAbility.performed += Rage;
		actions.Combat.ShieldAbility.performed += Shield;
		actions.Combat.EntangleAbility.performed += Entangle;

	}

	private void OnEnable()
	{
		actions.Combat.HealAbility.Enable();
		actions.Combat.RageAbility.Enable();
		actions.Combat.ShieldAbility.Enable();
		actions.Combat.EntangleAbility.Enable();
	}

	private void OnDisable()
	{
		actions.Combat.HealAbility.Disable();
		actions.Combat.RageAbility.Disable();
		actions.Combat.ShieldAbility.Disable();
		actions.Combat.EntangleAbility.Disable();
	}

	private void Heal(InputAction.CallbackContext obj)
	{
		if (PauseController.IsPaused())
		{
			return;
		}
		if (!available.hasHeal)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.CLERIC, out Ability heal))
		{
			return;
		}
		if (!healCooldown.IsEmpty())
		{
			return;
		}

		heal.Activate();
		healCooldown.FillMeter();
	}

	private void Rage(InputAction.CallbackContext obj)
	{
		if (PauseController.IsPaused())
		{
			return;
		}
		if (!available.hasRage)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.BARBARIAN, out Ability rage))
		{
			return;
		}
		if (!rageCooldown.IsEmpty() || isRaging)
		{
			return;
		}

		// Cooldown is empty.
		// Duration is full.
		rage.Activate();
		rageDuration.FillMeter();
		isRaging = true;
	}

	private void EndRage()
	{
		if (!available.hasRage)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.BARBARIAN, out Ability rage))
		{
			return;
		}
		if (!rageDuration.IsEmpty())
		{
			return;
		}

		// Cooldown is full.
		// Duration is empty.
		rage.Deactivate();
		rageCooldown.FillMeter();
		isRaging = false;
	}

	private void Shield(InputAction.CallbackContext obj)
	{
		if (PauseController.IsPaused())
		{
			return;
		}
		if (!available.hasShield)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.WIZARD, out Ability shield))
		{
			return;
		}
		if (!shieldCooldown.IsEmpty())
		{
			return;
		}

		shield.Activate();
		hasShield = true;
	}

	public void EndShield()
	{
		if (!available.hasShield)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.WIZARD, out Ability shield))
		{
			return;
		}
		if (!shieldCooldown.IsEmpty())
		{
			return;
		}

		shield.Deactivate();
		shieldCooldown.FillMeter();
		hasShield = false;
	}

	private void Entangle(InputAction.CallbackContext obj)
	{
		if (PauseController.IsPaused())
		{
			return;
		}
		if (!available.hasEntangle)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.DRUID, out Ability entangle))
		{
			return;
		}
		if (!entangleCooldown.IsEmpty() || isEntangling)
		{
			return;
		}

		// Cooldown is empty.
		// Duration is full.
		entangle.Activate();
		entangleDuration.FillMeter();
		isEntangling = true;
	}

	private void EndEntangle()
	{
		if (!available.hasEntangle)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.DRUID, out Ability entangle))
		{
			return;
		}
		if (!entangleDuration.IsEmpty())
		{
			return;
		}

		// Cooldown is full.
		// Duration is empty.
		entangle.Deactivate();
		entangleCooldown.FillMeter();
		isEntangling = false;
	}

	private void TickHeal()
	{
		if (!available.hasHeal)
		{
			return;
		}

		healCooldown.EmptyMeter(Time.deltaTime);
	}

	private void TickRage()
	{
		if (!available.hasRage)
		{
			return;
		}

		if (isRaging)
		{
			rageDuration.EmptyMeter(Time.deltaTime);
			if (rageDuration.IsEmpty())
			{
				EndRage();
			}
			else
			{
				return;

			}
		}

		rageCooldown.EmptyMeter(Time.deltaTime);
	}

	private void TickShield()
	{
		if (!available.hasShield)
		{
			return;
		}
		if (hasShield)
		{
			return;
		}

		shieldCooldown.EmptyMeter(Time.deltaTime);
	}

	private void TickEntangle()
	{
		if (!available.hasEntangle)
		{
			return;
		}

		if (isEntangling)
		{
			entangleDuration.EmptyMeter(Time.deltaTime);
			if (entangleDuration.IsEmpty())
			{
				EndEntangle();
			}
			else
			{
				return;

			}
		}

		entangleCooldown.EmptyMeter(Time.deltaTime);
	}

	private void Update()
	{
		TickHeal();
		TickRage();
		TickShield();
		TickEntangle();
	}
}
