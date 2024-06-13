using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
	[SerializeField]
	private float cutSceneLenght;
	
	private Timer sceneTimer;
	
	void Start()
	{
		sceneTimer = new Timer(cutSceneLenght);
		sceneTimer.OnTimer += OnCutsceneTimer;
	}

	void Update()
	{
		sceneTimer.Tick(Time.deltaTime);
	}
	
	private void OnCutsceneTimer()
	{
		sceneTimer.OnTimer -= OnCutsceneTimer;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
