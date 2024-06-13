using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AnimationLogic : MonoBehaviour
{
	public Animator playerAnimator;
	public VisualEffect circleEffect;
	public GameObject singEffect;
	public GameObject scratchVFX;

	private bool singing = false;

	private GameObject gameManager;

	private EventManager events;
	// Start is called before the first frame update
	void Start()
	{
		CharacterAttack.OnAttackAnimation += PlayAnimation;
		singEffect.SetActive(false);
		gameManager = GameObject.Find("GameManager");
		if (gameManager.Equals(null)) {
			Debug.Log("Gamemanager not found on animation logic");
		}
		scratchVFX.SetActive(false);
		events = ServiceLocator.Instance.Get<EventManager>();
		events.AddListener<Human>(Event.OnHumanStunned, (human) => StunParticle(human));
	}
	
	private void OnDestroy() {
		events.RemoveListener<Human>(Event.OnHumanStunned, (human) => StunParticle(human));
		CharacterAttack.OnAttackAnimation -= PlayAnimation;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E)) {
			if(!singing){
				singing = true;
				StartCoroutine(SingEffect());
				gameManager.GetComponent<SoundManager>().PlaySound(5);
			}
		}
	}

	public void PlayAnimation(string trigger) {
		playerAnimator.Play(trigger);
		StartCoroutine(ResetAnimation());
		circleEffect.Play();
		scratchVFX.SetActive(true);
	}

	IEnumerator ResetAnimation() {
		yield return new WaitForSeconds(.5f);
		playerAnimator.Play("Default");
		scratchVFX.SetActive(false);
	}

	IEnumerator SingEffect() {
		singEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		singEffect.SetActive(false);
		singing = false;
	}

	public void StunParticle(Human human) {
		human.GameObject.transform.Find("Stun").gameObject.SetActive(true);
		StartCoroutine(StunEffect(human));
	}

	IEnumerator StunEffect(Human human) {
		yield return new WaitForSeconds(1f);
		human.GameObject.transform.Find("Stun").gameObject.SetActive(false);
	}
}
