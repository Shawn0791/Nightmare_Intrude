using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrap : MonoBehaviour
{
    public GameObject[] chips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            CreateChips();
            Destroy(gameObject);
        }
    }

    private void CreateChips()
    {
        for (int i = 0; i < chips.Length; i++)
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            GameObject chip = ObjectPool.Instance.GetObject(chips[i]);
            chip.transform.position = new Vector2(posX, posY - i * 0.33f);
        }
    }
}
