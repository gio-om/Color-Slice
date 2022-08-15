using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Manager<AudioController>
{
    public AudioSource AudioSource;

    public AudioClip RightCubeSound;
    public AudioClip WrongCubeSound;

    public void PlayWrongSound()
    {
        AudioSource.PlayOneShot(WrongCubeSound);
    }

    public void PlayRighSound()
    {
        AudioSource.PlayOneShot(RightCubeSound);
    }
}
