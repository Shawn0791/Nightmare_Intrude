using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    void OnEnable()
    {
        SoundService.instance.Play("water");
    }
}
