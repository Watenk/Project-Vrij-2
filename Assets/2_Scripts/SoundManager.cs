using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioElement> audioElements;

    public AudioSource audioPlayer;

    private void Awake() 
    {
        UnderwaterShader.OnWaterJump += PlaySound;
        CharacterAttack.OnAttackSound += PlaySound;
        Health<IPlayer>.OnKill += PlaySound;
        HumanAttackState.OnAttack += PlaySound;
    }

    public void PlaySound(int id) {
        audioElements.ForEach(delegate (AudioElement element){
            if (id.Equals(element.id)) {
                audioPlayer.clip = element.clip;
                audioPlayer.Play();
            }
        });
    }

    public void StopSound(int id) {
        audioElements.ForEach(delegate (AudioElement element) {
            if (id.Equals(element.id)) {
                audioPlayer.Stop();
            }
        });
    }
}
