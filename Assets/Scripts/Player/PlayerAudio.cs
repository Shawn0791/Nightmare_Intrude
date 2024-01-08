using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public void AudioAttack1()
    {
        SoundService.instance.Play("Player_attack1");
    }

    public void AudioAttack2()
    {
        SoundService.instance.Play("Player_attack2");
    }

    public void AudioThrow()
    {
        SoundService.instance.Play("Player_throw");
    }

    public void AudioExecute()
    {
        SoundService.instance.Play("Player_execute");
    }
}
