using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomCreater : MonoBehaviour
{
    public GameObject venomPrefab;
    public Transform venomPos;
    public float venomSpeed;

    public void CreateVenom()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject venom = ObjectPool.Instance.GetObject(venomPrefab);
            venom.transform.position = venomPos.position;
            venom.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * (i + 1), 2.5f);
        }
    }
}
