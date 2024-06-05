using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Image healthSlider;
	[SerializeField]
	private Image boostSlider;
	[SerializeField]
	private TextMeshProUGUI boatKills;
	[SerializeField]
	private GameObject pauseObject;
	public int boatKillsNeeded;

	private bool paused;

	public void Start()
	{
		ServiceLocator.Instance.Get<EventManager>().AddListener(Event.OnPlayerHealth, (int amount) => UpdateHealthAmount(amount));
		UpdateBoatsKilled(0);
		UpPause();
	}
	
	private void OnDestroy() 
	{
		ServiceLocator.Instance.Get<EventManager>().RemoveListener(Event.OnPlayerHealth, (int amount) => UpdateHealthAmount(amount));
	}
	
	public void UpdateHealthAmount(int amount)
	{
		if (healthSlider == null) return;
		healthSlider.fillAmount = amount;
	}
	
	public void UpdateBoostAmount(int amount)
	{
		if (boostSlider == null) return;
		boostSlider.fillAmount = amount;
	}

	public void UpdateBoatsKilled(int amount)
	{
		if (boatKills == null) return;
		boatKills.text = amount + "/" + boatKillsNeeded;
	}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
				UpPause();
			} else {
				Pause();

            }
        }
    }

	public void Pause()
	{
		Cursor.lockState = CursorLockMode.None;
		pauseObject.SetActive(true);
		Time.timeScale = 0;
		paused = !paused;
	}

	public void UpPause()
	{
		Cursor.lockState = CursorLockMode.Locked;
		pauseObject.SetActive(false);
		Time.timeScale = 1;
		paused = !paused;
	}

	public void BackToMenu() {

    }
}
