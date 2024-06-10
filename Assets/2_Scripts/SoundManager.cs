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
        CharacterAttack.OnAttack += PlaySound;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
