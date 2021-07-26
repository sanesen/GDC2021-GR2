using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip play_idle()
    {
        return audioClips[1];
    }
    
    public AudioClip play_move()
    {
        return audioClips[8];
    }

    public AudioClip play_reload()
    {
        return audioClips[5];
    }

    public AudioClip play_shot()
    {
        return audioClips[6];
    }

}
