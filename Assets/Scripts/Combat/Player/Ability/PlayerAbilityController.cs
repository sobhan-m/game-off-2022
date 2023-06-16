using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
	[SerializeField] public AvailableAbilities available { get; private set; }
	private List<Ability> activeAbilities;
	private Dictionary<AbilityType, Ability> abilities;
	private PlayerInputActions actions;
	private Meter healCooldown;
	private Meter rageCooldown;
	private Meter rageDuration;
	private bool isRaging;

	private void Awake()
	{
		activeAbilities = new List<Ability>();
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
			rageDuration = new Meter(0, available.rageDuration, available.rageDuration);
			isRaging = false;
		}

		actions = new PlayerInputActions();
		actions.Player.HealAbility.performed += Heal;
		actions.Player.RageAbility.performed += Rage;
	}

	private void OnEnable()
	{
		actions.Player.HealAbility.Enable();
		actions.Player.RageAbility.Enable();
	}

	private void OnDisable()
	{
		actions.Player.HealAbility.Disable();
		actions.Player.RageAbility.Disable();
	}

	private void Heal(InputAction.CallbackContext obj)
	{
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
		if (!available.hasRage)
		{
			return;
		}
		if (!abilities.TryGetValue(AbilityType.BARBARIAN, out Ability rage))
		{
			return;
		}
		if (!rageCooldown.IsEmpty())
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

	private void Update()
	{
		TickHeal();
		TickRage();
	}
}
