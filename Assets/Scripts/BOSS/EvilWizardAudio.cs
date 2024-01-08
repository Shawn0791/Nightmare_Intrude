using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardAudio : MonoBehaviour
{
    public void AudioAttack1()
    {
        SoundService.instance.Play("BOSS_attack1");
    }

    public void AudioAttack2()
    {
        SoundService.instance.Play("BOSS_attack2");
    }

    public void AudioDead()
    {
        SoundService.instance.Play("BOSS_dead");
    }
}
