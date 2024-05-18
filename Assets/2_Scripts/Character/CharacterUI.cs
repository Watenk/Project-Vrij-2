using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : ICharacterUI
{
	// Dependencies
	private Slider healthSlider;
	private Slider boostSlider;
	
	public CharacterUI(Slider healthSlider, Slider boostSlider)
	{
		this.healthSlider = healthSlider;
		this.boostSlider = boostSlider;
	}
	
	public void UpdateHealthAmount(int amount)
	{
		if (healthSlider == null) return;
		healthSlider.value = amount;
	}
	
	public void UpdateBoostAmount(int amount)
	{
		if (boostSlider == null) return;
		boostSlider.value = amount;
	}
}
