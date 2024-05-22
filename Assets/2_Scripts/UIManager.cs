using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Slider healthSlider;
	[SerializeField]
	private Slider boostSlider;
	
	public void Start()
	{
		EventManager.Instance.AddListener(Event.OnPlayerHealth, (int amount) => UpdateHealthAmount(amount));
	}
	
	private void OnDestroy() 
	{
		EventManager.Instance.RemoveListener(Event.OnPlayerHealth, (int amount) => UpdateHealthAmount(amount));
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
