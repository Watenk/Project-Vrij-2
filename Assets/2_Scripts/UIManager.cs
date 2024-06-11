using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
		BoatsManager.OnBoatSunk += UpdateBoatsKilled;
		ServiceLocator.Instance.Get<EventManager>().AddListener(Event.OnPlayerHealth, (int amount) => UpdateHealthAmount(amount));
		EventManager events = ServiceLocator.Instance.Get<EventManager>();
		events.AddListener<float>(Event.OnBoostChange, (float amount) => UpdateBoostAmount(amount));

		UpdateBoatsKilled(0);
		UnPause();
	}
	
	private void OnDestroy() 
	{
		ServiceLocator.Instance.Get<EventManager>().RemoveListener(Event.OnPlayerHealth, (int amount) => UpdateHealthAmount(amount));
	}

	public float Remap(float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public void UpdateHealthAmount(float amount)
	{
		if (healthSlider == null) return;
		amount = Remap(amount, 0, 10, 0, 1);
		healthSlider.fillAmount = amount;
	}
	
	public void UpdateBoostAmount(float amount)
	{
		if (boostSlider == null) return;
		boostSlider.fillAmount = amount;
	}

	public void UpdateBoatsKilled(int amount)
	{
		Debug.Log(amount);
		if (boatKills == null) return;
		boatKills.text = amount + "/" + boatKillsNeeded;
	}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
				UnPause();
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

	public void UnPause()
	{
		Cursor.lockState = CursorLockMode.Locked;
		pauseObject.SetActive(false);
		Time.timeScale = 1;
		paused = !paused;
	}

	public void BackToMenu() {
		SceneManager.LoadScene("MainMenu");
    }	
}
