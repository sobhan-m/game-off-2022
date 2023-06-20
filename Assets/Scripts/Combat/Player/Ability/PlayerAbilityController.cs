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

	private void Awake()
	{
		abilities = new Dictionary<AbilityType, Ability>();

		if (available.hasHeal)
		{
			Player player = FindObjectOfType<Player>();
			if (!player)
			{
				throw new MissingReferenceException("No players in scene.");
			}

			abilities.Add(AbilityType.CLERIC, new HealAbility(available.healAmount, player));
			healCooldown = new Meter(0, available.healCooldown, available.healCooldown);
		}
		if (available.hasRage)
		{
			abilities.Add(AbilityType.BARBARIAN, new RageAbility(available.rageMultiplier));
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

		actions = new PlayerInputActions();
		actions.Player.HealAbility.performed += Heal;
		actions.Player.RageAbility.performed += Rage;
		actions.Player.ShieldAbility.performed += Shield;
	}

	private void OnEnable()
	{
		actions.Player.HealAbility.Enable();
		actions.Player.RageAbility.Enable();
		actions.Player.ShieldAbility.Enable();
	}

	private void OnDisable()
	{
		actions.Player.HealAbility.Disable();
		actions.Player.RageAbility.Disable();
		actions.Player.ShieldAbility.Disable();
	}

	private void Heal(InputAction.CallbackContext obj)
	{
		Debug.Log("Trying to heal.");
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
		Debug.Log("Trying to rage.");
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
		Debug.Log("Trying to shield.");
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

	private void Update()
	{
		TickHeal();
		TickRage();
		TickShield();
	}
}
