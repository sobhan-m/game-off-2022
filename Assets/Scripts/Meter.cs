using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter
{
	public float maxValue { get; private set; }
	public float minValue { get; private set; }
	public float currentValue { get; private set; }

	public Meter(float minValue, float maxValue, float currentValue = 0)
	{
		this.minValue = minValue;
		this.maxValue = maxValue;
		this.currentValue = currentValue;
	}

	public void FillMeter(float val)
	{
		if (val < 0)
		{
			throw new System.ArgumentOutOfRangeException("Meter.FillMeter(): The passed value must be non-negative. Use Meter.DepleteMeter() to reduce.");
		}
		currentValue = Mathf.Clamp(currentValue += val, minValue, maxValue);
	}

	public void DepleteMeter(float val)
	{
		if (val < 0)
		{
			throw new System.ArgumentOutOfRangeException("Meter.DepleteMeter(): The passed value must be non-negative. Use Meter.FillMeter() to increase.");
		}
		currentValue = Mathf.Clamp(currentValue -= val, minValue, maxValue);
	}

	public bool IsFull()
	{
		return currentValue >= maxValue;
	}

	public bool IsEmpty()
	{
		return currentValue <= minValue;
	}

}
