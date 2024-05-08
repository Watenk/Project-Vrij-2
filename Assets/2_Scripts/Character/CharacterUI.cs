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
		healthSlider.value = amount;
	}
	
	public void UpdateBoostAmount(int amount)
	{
		boostSlider.value = amount;
	}
}
