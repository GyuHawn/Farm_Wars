using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSkill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}