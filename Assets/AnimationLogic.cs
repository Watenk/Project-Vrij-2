using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AnimationLogic : MonoBehaviour
{
    public Animator playerAnimator;
    public VisualEffect circleEffect;
    public GameObject singEffect;

    private bool singing = false;
    // Start is called before the first frame update
    void Start()
    {
        CharacterAttack.OnAttackAnimation += PlayAnimation;
        singEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if(!singing){
                StartCoroutine(SingEffect());
            }
        }
    }

    public void PlayAnimation(string trigger) {
        playerAnimator.Play(trigger);
        StartCoroutine(ResetAnimation());
        circleEffect.Play();
    }

    IEnumerator ResetAnimation() {
        yield return new WaitForSeconds(.5f);
        playerAnimator.Play("Default");
    }

    IEnumerator SingEffect() {
        singEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        singEffect.SetActive(false);
    }
}
